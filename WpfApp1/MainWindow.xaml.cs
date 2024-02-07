using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {

        private string filePath = "";
        public MainWindow()
        {
            InitializeComponent();
            statusText.Visibility = Visibility.Visible; // Eller Visibility.Collapsed
        }

        private void mnuNew_Click(object sender, RoutedEventArgs e)
        {
            TxtInput.Text = "";
        }

        private void mnuOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Tekstfiler (*.txt)|*.txt|Alle filer (*.*)|*.*"; // Sætter filter for filtyper
            openFileDialog.DefaultExt = ".txt"; // Standard filudvidelse

            if (openFileDialog.ShowDialog() == true)
            {
                // Læser filen fra den valgte sti og viser indholdet i TxtInput
                TxtInput.Text = File.ReadAllText(openFileDialog.FileName);
                MessageBox.Show("Text recieved!");
            }
            else
            {
                MessageBox.Show("No file chosen.");
            }
        }

        private void mnuSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Tekstfiler (*.txt)|*.txt|Alle filer (*.*)|*.*"; // Sætter filter for filtyper
            saveFileDialog.DefaultExt = ".txt"; // Standard filudvidelse
            saveFileDialog.FileName = "Dokument"; // Standard filnavn

            if (saveFileDialog.ShowDialog() == true)
            {
                // Gemmer filen til den valgte sti
                File.WriteAllText(saveFileDialog.FileName, TxtInput.Text);
                MessageBox.Show("Text saved.");
            }
        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Dumango Editor", "Do you want to save before exiting?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    // Gem ændringerne
                    File.WriteAllText(filePath, TxtInput.Text);
                    MessageBox.Show("Changes saved.");
                    this.Close();
                    break;
                case MessageBoxResult.No:
                    // Lukker uden at gemme
                    this.Close();
                    break;
                case MessageBoxResult.Cancel:
                    // Gør intet (lader brugeren gå tilbage til applikationen)
                    break;

            }
        }

        private void mnuZoomIn_Click(object sender, RoutedEventArgs e)
        {
            double sizeIncrease = 2.0;

            if (TxtInput != null)
            {
                TxtInput.FontSize += sizeIncrease;
            }
        }

        private void mnuZoomOut_Click(object sender, RoutedEventArgs e)
        {
            double sizeDecrease = 2.0;

            if (TxtInput != null)
            {
                TxtInput.FontSize -= sizeDecrease;
            }
        }

        private void mnuRestoreZoom_Click(object sender, RoutedEventArgs e)
        {

            double defaultSize = 12.0;

           if (TxtInput != null)
            {
                TxtInput.FontSize = defaultSize;
            }
        }

        private void mnuStatusBar_Click(object sender, RoutedEventArgs e)
        {
            // Skifter statusbaren mellem synlig og skjult
            if (statusText.Visibility == Visibility.Visible)
            {
                statusText.Visibility = Visibility.Collapsed; // Skjuler statusbaren
            }
            else
            {
                statusText.Visibility = Visibility.Visible; // Viser statusbaren
            }
        }

        private void TxtInput_SelectionChanged(object sender, RoutedEventArgs e)
        {
            int row = TxtInput.GetLineIndexFromCharacterIndex(TxtInput.CaretIndex) + 1;
            int col = TxtInput.CaretIndex - TxtInput.GetCharacterIndexFromLineIndex(TxtInput.GetLineIndexFromCharacterIndex(TxtInput.CaretIndex)) + 1;
            int chr = TxtInput.Text.Length;
            statusText.Text = $"Line: {row}, Column: {col}, Charachters: {chr}";
        }

        private void mnuWordWrap_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void NewCommand_Executed(object sender, RoutedEventArgs e)
        {
            TxtInput.Text = "";
        }
    }
}