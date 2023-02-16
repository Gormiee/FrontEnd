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

namespace Exercise2Grid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            m_boat = new Sailboat();
        }

        private Sailboat m_boat;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            m_boat.Length = Convert.ToDouble(Length.Text);
            Speed.Text = Math.Round(1.34 * Math.Sqrt(m_boat.Length), 2).ToString();
            MessageBox.Show("The name of the boat is " + m_boat.Name);

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (m_boat != null) 
                m_boat.Name = NameTextBox.Text;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control) && e.Key == Key.L)
            {
                Length.MinWidth = Length.MinWidth + 10;
                NameTextBox.MinWidth = NameTextBox.MinWidth + 10;
                FontSize = FontSize + 2;
            }
            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control) && e.Key == Key.S)
            {

                if (FontSize > 2)
                {
                    Length.MinWidth = Length.MinWidth - 10;
                    NameTextBox.MinWidth = NameTextBox.MinWidth - 10;
                    FontSize = FontSize - 2;
                }


                //MessageBox.Show("Right Ctrl + S");
            }
        }
    }
}
