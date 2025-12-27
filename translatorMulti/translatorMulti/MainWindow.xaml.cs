using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace MultiLangTranslator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            cmbLang.SelectedIndex = 0;
        }

        private void Translate_Click(object sender, RoutedEventArgs e)
        {
            string text = txtInput.Text;
            if (string.IsNullOrWhiteSpace(text)) return;

            string from = "tr";
            string to = "en";

            if (cmbLang.SelectedIndex == 1)
            {
                from = "en";
                to = "tr";
            }
            else if (cmbLang.SelectedIndex == 2)
            {
                from = "tr";
                to = "de";
            }

            txtOutput.Text = Translate(text, from, to);
        }

        string Translate(string text, string from, string to)
        {
            string url =
                $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={from}&tl={to}&dt=t&q={Uri.EscapeUriString(text)}";

            using (WebClient wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                string result = wc.DownloadString(url);

                // JSON içinden çeviriyi çekiyoruz
                Match match = Regex.Match(result, "\\[\\[\\[\"(.*?)\"");
                return match.Success ? match.Groups[1].Value : "Çeviri başarısız";
            }
        }
    }
}
