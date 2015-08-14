using System;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace counter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void chooseBttn_Click(object sender, EventArgs e)
        {
            fileDialog.ShowDialog();
            fileBox.Text = fileDialog.FileName;
            string content = "";
            content = Program.ExtractTextFromPdf(fileDialog.FileName);
            var numtype = new Regex(@"\(\d{4,6}\/2015\)\s+([\d]). mell", RegexOptions.Multiline);
            var eur = new Regex(@"\(\d{4,6}\/2015\)\s+EUR", RegexOptions.Multiline);
            var dict = new SortedDictionary<string, int>();
            dict.Add("total", 0);
            foreach (Match item in numtype.Matches(content))
            {
                string key = item.Groups[1].Value;
                if (!dict.ContainsKey(key)) dict.Add(key, 0);
                dict[key]++;
                dict["total"]++;
            }
            dict.Add("eur", 0);
            foreach (Match item in eur.Matches(content))
            {
                dict["eur"]++;
                dict["total"]++;
            }
            int total = 0;
            foreach (KeyValuePair<string, int> item in dict)
            {
                dataView.Rows.Add(new string[] { item.Key, item.Value.ToString() });
            }
        }
    }
}
