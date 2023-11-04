using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using static System.Net.Mime.MediaTypeNames;

namespace HW_04._11._23
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class TextAnalizer
    {
        private string _text;
        public TextAnalizer(string text)
        {
            _text = text;
        }

        public int CountSentences()
        {
            string[] sentences = _text.Split(new string[] { ". ", ", ", "? ", "! " }, StringSplitOptions.RemoveEmptyEntries);
            return sentences.Length;
        }

        public int CountCharacters()
        {
            return _text.Length;
        }

        public int CountWords()
        {
            string[] words = _text.Split(new char[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }

        public int CountQuestionSentences()
        {
            string[] questions = _text.Split(new string[] { "? " }, StringSplitOptions.RemoveEmptyEntries);
            return questions.Length - 1;
        }

        public int CountExclamations()
        {
            string[] exclamations = _text.Split(new string[] { "! " }, StringSplitOptions.RemoveEmptyEntries);
            return exclamations.Length - 1;
        }

    }
    public partial class MainWindow : Window
    {
        TextAnalizer text;
        int countSenteces = 0;
        int countCharacters = 0;
        int countWords = 0;
        int countQuestions = 0;
        int countExclamation = 0;
        List<CheckBox> selectedCheckBoxes;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UserTextChanged(object sender, TextChangedEventArgs e)
        {
            text = new TextAnalizer(UserText.Text);
        }

        private async void TextFromFile_Click(object sender, RoutedEventArgs e)
        {
            string path;
            try
            {
                OpenFileDialog d = new OpenFileDialog();
                d.Filter = "Text files (*.txt)|*.txt";
                if (d.ShowDialog() == true)
                {
                    path = d.FileName;
                }
                else
                {
                    MessageBox.Show("Скасовано вибір файлу. Використовується файл за замовчуванням.", "Попередження", MessageBoxButton.OK, MessageBoxImage.Warning);
                    path = "text.txt";
                }

                string fileText = await LoadTextFromFileAsync(path);
                if (!string.IsNullOrEmpty(fileText))
                {
                    text = new TextAnalizer(fileText);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error occured:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<string> LoadTextFromFileAsync(string path)
        {
            try
            {
                string fileText = await Task.Run(() => File.ReadAllText(path));
                return fileText;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error occured:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return string.Empty;
            }
        }
        private IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
        private List<CheckBox> FindSelectedCheckBoxes(DependencyObject depObj)
        {
            List<CheckBox> selectedCheckBoxes = new List<CheckBox>();

            if (depObj != null)
            {
                foreach (CheckBox checkBox in FindVisualChildren<CheckBox>(depObj))
                {
                    if (checkBox.IsChecked == true && checkBox.Name != "AllTypes")
                    {
                        selectedCheckBoxes.Add(checkBox);
                    }
                }
            }

            return selectedCheckBoxes;
        }
        private void CheckBoxChecked(object sender, RoutedEventArgs e)
        {
            if (AllTypes.IsChecked == true)
            {
                
                    Sentences.IsChecked = true;
                    Characters.IsChecked = true;
                    Words.IsChecked = true;
                    Questions.IsChecked = true;
                    Exclamations.IsChecked = true;
            }
            selectedCheckBoxes = FindSelectedCheckBoxes(this);
        }
           private void ShowRecord_Click(object sender, RoutedEventArgs e)
            {
            //StringBuilder result = new StringBuilder();

            //if (selectedCheckBoxes.Count > 0)
            //{
            //    Parallel.ForEach(selectedCheckBoxes, chBox =>
            //    {
            //        switch (chBox.Name)
            //        {
            //            case "Sentences":
            //                result.AppendLine($"Кількість речень: {text.CountSentences()}");
            //                break;
            //            case "Characters":
            //                result.AppendLine($"Кількість символів: {text.CountCharacters()}");
            //                break;
            //            case "Words":
            //                result.AppendLine($"Кількість слів: {text.CountWords()}");
            //                break;
            //            case "Questions":
            //                result.AppendLine($"Кількість питальних речень: {text.CountQuestionSentences()}");
            //                break;
            //            case "Exclamations":
            //                result.AppendLine($"Кількість окличних речень: {text.CountExclamations()}");
            //                break;
            //        }
            //    });
            //}
            //else
            //{
            //    MessageBox.Show("Оберіть хоча б один пункт для аналізу.", "Попередження", MessageBoxButton.OK, MessageBoxImage.Warning);
            //}

            //Dispatcher.Invoke(()=>ResultTextBlock.Text = result.ToString());
            StringBuilder result = new StringBuilder();

            if (selectedCheckBoxes != null && selectedCheckBoxes.Count > 0)
            {
                foreach (var chBox in selectedCheckBoxes)
                {
                    if (chBox.IsChecked==true)
                    {
                        switch (chBox.Name)
                        {
                            case "Sentences":
                                result.AppendLine($"Кількість речень: {text.CountSentences()}");
                                break;
                            case "Characters":
                                result.AppendLine($"Кількість символів: {text.CountCharacters()}");
                                break;
                            case "Words":
                                result.AppendLine($"Кількість слів: {text.CountWords()}");
                                break;
                            case "Questions":
                                result.AppendLine($"Кількість питальних речень: {text.CountQuestionSentences()}");
                                break;
                            case "Exclamations":
                                result.AppendLine($"Кількість окличних речень: {text.CountExclamations()}");
                                break;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Оберіть хоча б один пункт для аналізу.", "Попередження", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            ResultTextBlock.Text = result.ToString();
        }

        private async Task SaveRecordToFileAsync(string result)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text files (*.txt)|*.txt";
                if (saveFileDialog.ShowDialog() == true)
                {
                    string filePath = saveFileDialog.FileName;
                    await Task.Run(() => File.WriteAllText(filePath, result));
                    MessageBox.Show("Результат успешно сохранен в файл.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения файла:\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void SaveRecordToFile(object sender, RoutedEventArgs e)
        {
            
            await SaveRecordToFileAsync(ResultTextBlock.Text);
        }
    }
    
}
