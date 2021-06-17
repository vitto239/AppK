using System;
using System.Collections.Generic;
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

namespace Appk
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void InitListBox(string[] z,ListBoxes listBoxes , FillModes fillMode)
        {
            var workListbox = (listBoxes == ListBoxes.Main) ? mainListBox : selectListbox;
            switch (fillMode)
            {
                case FillModes.Init:
                    var x = z.Length; workListbox.Items.Clear();
                    for (int i = 0; i < x; i++)
                    {
                        workListbox.Items.Add(z[i]);
                    }
                    break;
                case FillModes.Fill:
                    break;
                default:
                    break;
            }
        }
        private void InitListBox(System.Collections.IList z, ListBoxes selected, FillModes init)
        {
            var arrLenght = z.Count;
            var arr = new string[arrLenght];
            for (int i = 0; i < arrLenght ; i++)
            {
                arr[i] = z[i].ToString();
            }
            InitListBox(arr, selected, init);
        }
        public enum FillModes
        {
            Init, Fill
        }
        public enum ListBoxes
        {
            Main, Selected
        }

        engine AppEngine = new engine();
        
        public MainWindow()
        {
            InitializeComponent();

            textBoxSearchTB.TextChanged += TextBoxSearchTB_TextChanged;
            textBoxSearchTB.GotFocus += TextBoxSearchTB_GotFocus;
            mainListBox.SelectionChanged += MainListBox_SelectionChanged;
            ButtonNext.Click += NextButton_Click;
            ButtonTestSmtp.Tag = false;
            ButtonNext.Tag = false;

            InitListBox(AppEngine.GetNames(),ListBoxes.Main , FillModes.Init);
        }
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var z = mainListBox.SelectedItems;
            selectListbox.Items.Clear();
            InitListBox(z, ListBoxes.Selected, FillModes.Init);
            if ((bool)ButtonNext.Tag == false)
            {
                ButtonNext.Tag = true;
                Task.Run(() => Blinking(ButtonNext));
            }
        }
        private void Blinking(Button sendButton)
        {            
                sendButton.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    for (int i = 0; i < 10; i++)
                    {
                        sendButton.Background = Brushes.GreenYellow;
                        System.Threading.Thread.Sleep(500);
                        sendButton.Background = Brushes.Green;
                        System.Threading.Thread.Sleep(500);
                    }
                        
                    

                },System.Windows.Threading.DispatcherPriority.Background);
        }
        private void TextBoxSearchTB_GotFocus(object sender, RoutedEventArgs e)
        {
           if (textBoxSearchTB.Text == "nazwa uslugi") { textBoxSearchTB.Text = ""; }
        }
        private void TextBoxSearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBoxSearchTB.Text.Length < 2) { InitListBox(AppEngine.GetNames(), ListBoxes.Main, FillModes.Init); return; }
            mainListBox.Items.Clear();
            InitListBox(AppEngine.SearchIn(textBoxSearchTB.Text),ListBoxes.Main, FillModes.Init);
        }        
    }
}
