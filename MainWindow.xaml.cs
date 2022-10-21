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

        List<string> messagesToClass = new List<string>{ "Glöm inte att övning ger färdighet!", "Öppna boken på sida 257." };

        string colorStyling;

        List<string> techniques = File.ReadAllLines("techniques.txt").ToList();
        public string[] colors { get; set; }
        public string selectedColor = "";

        public MainWindow()
        {
            InitializeComponent();

            colors = new string[] { "Red", "Blue", "Green", "Yellow", "Purple" };
            DataContext = this;

        }

        private void GenerateWebsite_Click(object sender, RoutedEventArgs e)
        {
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
            if (EnterMessages.Text.Length.Equals(0) || EnterTechniques.Text.Length.Equals(0) || ComboBoxColor.SelectedItem == null)
            {
                GenerateMANWebsite.IsEnabled = false;
            }
            else
            {
                GenerateMANWebsite.IsEnabled = true;
            }

            Selection.Text = ComboBoxColor.SelectedItem.ToString();
        }

        private void GenerateMANWebsite_Click(object sender, RoutedEventArgs e)
        {
            string manColorStyling = ComboBoxColor.SelectedItem.ToString();
            List<string> manTechniques = EnterTechniques.Text.Split(Environment.NewLine).ToList();
            List<string> manMessagesToClass = EnterMessages.Text.Split(Environment.NewLine).ToList();

            WebsiteGeneratorWPF.StyledWebsiteGenerator managerWebsite = new WebsiteGeneratorWPF.StyledWebsiteGenerator("ITHS Dist 2022", manColorStyling, manMessagesToClass, manTechniques);

            List<string> website1 = managerWebsite.PrintPage();

            string websiteString = "";

            foreach (string s in website1)
            {
                websiteString += s;
            }

            WebsiteOutput.Text = websiteString;
            OutoutForDev.Text = websiteString;

        }

        private void EnterMessages_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (EnterMessages.Text.Length.Equals(0) || EnterTechniques.Text.Length.Equals(0) || ComboBoxColor.SelectedItem == null)
            {
                GenerateMANWebsite.IsEnabled = false;
            }
            else
            {
                GenerateMANWebsite.IsEnabled = true;
            }
        }

        private void EnterTechniques_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (EnterMessages.Text.Length.Equals(0) || EnterTechniques.Text.Length.Equals(0) || ComboBoxColor.SelectedItem == null)
            {
                GenerateMANWebsite.IsEnabled = false;
            }
            else
            {
                GenerateMANWebsite.IsEnabled = true;
            }
        }
    }
}
