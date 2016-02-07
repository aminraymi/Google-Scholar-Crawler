using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google_Scholar_Crawler.util;
using System.Collections;


namespace Google_Scholar_Crawler
{
    public partial class Crawler : Form
    {
        // search items
        private List<string> _searchItems;

        // current item index + 1 for using in progress bar
        private double _currentItemNum;

        // total search items count
        private double _totalItemsNum;

        // total number of records
        private double _recordCounter;

        // store records
        private StringBuilder _records;

        public Crawler()
        {
            InitializeComponent();
        }
        private void Crawler_Load(object sender, EventArgs e)
        {
            FormInitialization();

        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            new SettingsForm().Show();
        }

        private void aboutButton_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }
        private void selectFileButton_Click(object sender, EventArgs e)
        {
            // open dialog to select input file
            DialogResult result = openTextFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                inputFileTextBox.Text = openTextFileDialog.FileName;
            }
        }

        private void selectFolderButton_Click(object sender, EventArgs e)
        {
            // open dialog to select output folder
            DialogResult result = outputFolderBrowser.ShowDialog();
            if (result == DialogResult.OK)
            {
                outputFolderTextBox.Text = outputFolderBrowser.SelectedPath;
            }
        }

        private async void startButton_Click(object sender, EventArgs e)
        {
            statusLabel.Text = "";
            // validate form inputs
            if (!FormValidator())
            {
                return;
            }

            // get search items from input file
            _searchItems = GetSearchItems(openTextFileDialog.FileName);
            if (_searchItems == null)
            {
                statusLabel.Text = "File not found";
                return;
            }

            // start crawling first item
            _currentItemNum = 1;

            // total number of items
            _totalItemsNum = _searchItems.Count;

            _recordCounter = 0;

            _records = new StringBuilder("PDF's URL").Append("\t").Append("BibTeX").Append(Environment.NewLine);

            foreach (string item in _searchItems)
            {
                // calculate progress
                double progress = (double)(_currentItemNum / _totalItemsNum) * 100;

                // set progress value from 0 - 100
                progressBar1.Value = Convert.ToInt32(progress);

                for (int i = 0; i < CrawlerPreferences.NumPages; i++)
                {
                    // set starting page for pagination
                    int pageIndex = i * 10;
                    WebClient client = new WebClient();

                    // add url query string
                    client.QueryString.Add("hl", "en");
                    client.QueryString.Add("as_sdt", "0,5");
                    client.QueryString.Add("q", item);
                    client.QueryString.Add("start", pageIndex.ToString());

                    string htmlCode = await client.DownloadStringTaskAsync("https://scholar.google.com/scholar");

                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(htmlCode);

                    foreach (HtmlNode div in doc.DocumentNode.SelectNodes("//div[@class='gs_r']"))
                    {
                        // select span nodes that contains [PDF] for current div
                        HtmlNodeCollection pdfNodes = div.SelectNodes(".//span[.='[PDF]']");

                        // whether div node has pdf URL or not
                        if (pdfNodes == null)
                        {
                            // go to next div
                            continue;
                        }

                        HtmlNode citeNode = div.SelectSingleNode(".//a[.='Cite']");

                        // get citation id from 'Cite' link
                        string citeOnclickValue = citeNode.Attributes["onclick"].Value;
                        string[] splitedCiteOnclickValue = citeOnclickValue.Split('\'');
                        string citationId = splitedCiteOnclickValue[1];

                        // get citation popup page
                        WebClient citationClient = new WebClient();

                        // create citation page URL
                        StringBuilder citationUrl = new StringBuilder("https://scholar.google.com/scholar?q=info:")
                                            .Append(citationId)
                                            .Append(":scholar.google.com/&output=cite&scirp=4&hl=en");

                        // get citation page content
                        string citationPageContent = await citationClient.DownloadStringTaskAsync(citationUrl.ToString());

                        // load citation page content into citeDoc html document
                        HtmlAgilityPack.HtmlDocument citeDoc = new HtmlAgilityPack.HtmlDocument();
                        citeDoc.LoadHtml(citationPageContent);

                        // get BibTeX link
                        HtmlNode bibTexLink = citeDoc.DocumentNode.SelectSingleNode("//a[.='BibTeX']");

                        // get BibTeX URL
                        StringBuilder bibTexUrl = new StringBuilder("https://scholar.google.com")
                                            .Append(bibTexLink.Attributes["href"].Value);

                        // clean the URL (removing amp; from BibTeX URL)
                        string bibTexUrlTrimmed = bibTexUrl.Replace("amp;", String.Empty).ToString();

                        // get BibTeX content
                        WebClient bibTexClient = new WebClient();
                        string bibTexContent = await bibTexClient.DownloadStringTaskAsync(bibTexUrlTrimmed);

                        // remov \n from BibTeX content
                        string bibTexContentTrimmed = bibTexContent.Replace("\n", String.Empty);

                        // get PDF URL link node
                        HtmlNode pdfLinkNode = div.SelectSingleNode(".//div[contains(@class,'gs_md_wp gs_ttss')]").Descendants("a").FirstOrDefault();

                        // get PDF URL from pdf link node
                        string pdfUrl = pdfLinkNode.Attributes["href"].Value;

                        // clean the PDF URL (removing amp; from PDF URL)
                        string pdfUrlTrimmed = pdfUrl.Replace("amp;", String.Empty);

                        _recordCounter++;

                        // add record to _records
                        _records.Append(pdfUrlTrimmed).Append("\t").Append(bibTexContentTrimmed).Append(Environment.NewLine);

                        // save records to file
                        if (_recordCounter % CrawlerPreferences.NumMaxRecords == 0)
                        {
                            // save records into csv file
                            this.SaveRecords(_records);
                            _records = new StringBuilder("PDF's URL").Append("\t").Append("BibTeX").Append(Environment.NewLine);
                        }

                    }
                }
                _currentItemNum++;


            }

            MessageBox.Show("crawling completed");

        }

        private void FormInitialization()
        {
            // set open file dialog file format
            openTextFileDialog.Title = "Open Text File";
            openTextFileDialog.Filter = "TXT files|*.txt";
        }

        private bool FormValidator()
        {
            // check input file
            if (String.IsNullOrEmpty(inputFileTextBox.Text))
            {
                statusLabel.Text = "Please select input file";
                return false;
            }

            // check output folder
            if (String.IsNullOrEmpty(outputFolderTextBox.Text))
            {
                statusLabel.Text = "Please select output folder";
                return false;
            }
            // check internet connection
            if (!ConnectionHelper.CheckConnectivity())
            {
                statusLabel.Text = "No internet access";
                return false;
            }

            return true;
        }

        private List<string> GetSearchItems(string inputFilePath)
        {
            try
            {
                // get input file lines
                string[] inputLines = File.ReadAllLines(inputFilePath);

                List<string> searchItems = new List<string>(inputLines);

                // seek lines to reach search items
                searchItems.RemoveRange(0, 4);

                return searchItems;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void SaveRecords(StringBuilder records)
        {
            string csvnumberString = "";
            int csvNumber = Convert.ToInt32(_recordCounter / CrawlerPreferences.NumMaxRecords);
            if (csvNumber < 10)
            {
                csvnumberString = "000" + csvNumber.ToString();
            }
            else if (csvNumber < 100)
            {
                csvnumberString = "00" + csvNumber.ToString();
            }
            else if (csvNumber < 1000)
            {
                csvnumberString = "0" + csvNumber.ToString();
            }
            else
            {
                csvnumberString = csvNumber.ToString();
            }
            DateTime today = DateTime.Now;

            // format date to yyyy-mm-dd e.g. 2016-02-08
            string date = String.Format("{0:yyyy-MM-dd}", today);
            StringBuilder csvFileName = new StringBuilder();
            csvFileName.Append(date).Append("_").Append(csvnumberString).Append(".csv");
            string path = outputFolderBrowser.SelectedPath + "/" + csvFileName;
            // save to output
            File.WriteAllText(path, records.ToString());  
        }
    }
}
