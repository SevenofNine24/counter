using System;
using System.Text;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace counter
{
    class Program
    {
        static Form1 form;
        [STAThread]
        static void Main(string[] args)
        {
            form = new Form1();
            Application.EnableVisualStyles();
            Application.Run(form);
        }
        public static string ExtractTextFromPdf(string path)
        {
            using (PdfReader reader = new PdfReader(path))
            {
                StringBuilder text = new StringBuilder();
                for (int i = 1; i <= reader.NumberOfPages; i++)
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                return text.ToString();
            }
        }
    }
}
