using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using UnscrambleWords.Algorithm;
using UnscrambleWords.Data;
using UnscrambleWords.FileWorker;

namespace UnscrambleWords
{
    public partial class MainWindow : Window
    {
        private static readonly FileReader _fileReader = new FileReader();
        private static readonly WordMatcher _wordMatcher = new WordMatcher();

        public static string option;
        public static string filename;
        public static bool changeTheText;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExecuteScrambledWordsManualEntryScenario()
        {
            string[] scrambledWords = option.Split(',');
            DisplayMatchedUnscrambledWords(scrambledWords);
        }

        private void ExecuteScrambledWordsInFileScenario()
        {
            try
            {
                string[] scrambledWords = _fileReader.Read(filename);
                DisplayMatchedUnscrambledWords(scrambledWords);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Constants.ErrorScrambledWordsCannotBeLoaded + ex.Message);
            }
        }

        public void DisplayMatchedUnscrambledWords(string[] scrambledWords)
        {
            string[] wordList = _fileReader.Read(Constants.WordListFileName);

            List<MatchedWord> matchedWords = _wordMatcher.Match(scrambledWords, wordList);

            if (matchedWords.Any())
            {
                MatchFoundText.Text = Constants.MatchFound;
                foreach (var matchedWord in matchedWords)
                {
                    ResultMatchText.Text += matchedWord.Word + ", ";
                }
                if (InputRadioBtn.IsChecked == true)
                {
                    InputMatchText.Text = InputText.Text.ToUpper();
                }
                else if (FileRadioBtn.IsChecked == true)
                {
                    InputMatchText.Text = filename;
                }
            }
            else
            {
                MessageBox.Show(Constants.MatchNotFound);
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                option = InputText.Text ?? string.Empty;
                ResultMatchText.Text = string.Empty;
                InputMatchText.Text = string.Empty;
                MatchFoundText.Text = string.Empty;

                if (InputRadioBtn.IsChecked == true)
                {
                    ExecuteScrambledWordsManualEntryScenario();
                }
                else if (FileRadioBtn.IsChecked == true)
                {
                    ExecuteScrambledWordsInFileScenario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Constants.ErrorProgramWillBeTerminated + ex.Message);
            }
        }

        private void BrowseFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = Constants.FileExt;
            ofd.Filter = Constants.TxtFilter;
            if (ofd.ShowDialog() == true)
            {
                filename = ofd.FileName;
                FileDestinationText.Text = Constants.FileDestination + filename;
            }
        }
    }
}
