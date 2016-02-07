using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Google_Scholar_Crawler
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            // set values for settings
            numPagesTextBox.Text = CrawlerPreferences.NumPages.ToString();
            numRecordsTextBox.Text = CrawlerPreferences.NumMaxRecords.ToString();
        }

        private void applySettingButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(numPagesTextBox.Text))
            {
                try
                {
                    CrawlerPreferences.NumPages = Convert.ToInt32(numPagesTextBox.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Please enter correct number");
                }
                
            }

            if (!String.IsNullOrEmpty(numRecordsTextBox.Text))
            {
                try
                {
                    CrawlerPreferences.NumMaxRecords = Convert.ToInt32(numRecordsTextBox.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Please enter correct number");
                }
            }

            // close the form
            this.Hide();
            this.Close();

        }
    }
}
