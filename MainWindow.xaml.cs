using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Console;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string choice = "";
        string fileName = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateWebsite_Click(object sender, RoutedEventArgs e)
        {
            string[] messagesToClass = { "Glöm inte att övning ger färdighet!", "Öppna boken på sida 257." };

            string colorStyling;

            string[] techniques = File.ReadAllLines("techniques.txt");

            if (File.Exists("color-styling"))
            {
                colorStyling = File.ReadAllText("color-styling");
            }
            else
            {
                File.WriteAllText("color-styling", "blue");
                colorStyling = "blue";
            }

            WebsiteGeneratorWPF.StyledWebsiteGenerator styledWebsite = new WebsiteGeneratorWPF.StyledWebsiteGenerator("Klass A", colorStyling, messagesToClass, techniques);

            List<string> website1 = styledWebsite.PrintPage();

            string websiteString = "";

            foreach (string s in website1)
            {
                websiteString += s;
            }

            WebsiteOutput.Text = websiteString;
            OutoutForDev.Text = websiteString;

            choice = "codeBehind";
        }

        private void LocalHTML_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog oFD = new Microsoft.Win32.OpenFileDialog();
            oFD.FileName = "";
            oFD.DefaultExt = ".html";
            oFD.Filter = "All files (*.*)|*.*";

            Nullable<bool> result = oFD.ShowDialog();

            if (result == true)
            {
                WebsiteOutput.Text = File.ReadAllText(oFD.FileName);
                OutoutForDev.Text = File.ReadAllText(oFD.FileName);
            }

            choice = "ownFile";
            fileName = oFD.FileName;
        }

        private void SaveForDev_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog sFD = new Microsoft.Win32.SaveFileDialog();
            sFD.FileName = "";
            sFD.DefaultExt = ".html";
            sFD.Filter = "Webpage, HTML Only (.html)|*.html";

            if (choice.Equals("ownFile"))
            {
                sFD.FileName = fileName;
            }

            Nullable<bool> result = sFD.ShowDialog();

            if (result == true)
            {
                File.WriteAllText(sFD.FileName, OutoutForDev.Text);
            }
        }

        private void ComboBoxColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Selection.Text = ComboBoxColor.Text;
        }
    }
}
