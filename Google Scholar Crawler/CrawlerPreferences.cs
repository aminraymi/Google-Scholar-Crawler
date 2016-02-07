using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Google_Scholar_Crawler
{
    static class CrawlerPreferences
    {
        // number of inner pages that crawler must crawl. it's 1 by defualt
        private static int _numPages = 1;

        // number of records per CSV
        private static double _numMaxRecords = 10;

        public static int NumPages
        {
            get { return _numPages; }
            set { _numPages = value; }
        }

        public static double NumMaxRecords
        {
            get { return _numMaxRecords; }
            set { _numMaxRecords = value; }
        }
    }
}
