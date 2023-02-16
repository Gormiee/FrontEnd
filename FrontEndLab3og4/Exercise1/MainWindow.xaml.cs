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

namespace Exercise1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Sailboat boat = new Sailboat();
            boat.Name = NameBox.Text;
            boat.Length = double.Parse(LenghtBox.Text);
            Result.Text = boat.Hullspeed().ToString().Substring(0, 4);
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("The name of the boat is " + NameBox.Text);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control) && e.Key == Key.L)
            {
                LenghtBox.MinWidth = LenghtBox.MinWidth + 10;
                NameBox.MinWidth = NameBox.MinWidth + 10;
                FontSize = FontSize + 2;
            }
            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control) && e.Key == Key.S)
            {

                if (FontSize > 2)
                {
                    LenghtBox.MinWidth = LenghtBox.MinWidth - 10;
                    NameBox.MinWidth = NameBox.MinWidth - 10;
                    FontSize = FontSize - 2;
                }
                    
                
                //MessageBox.Show("Right Ctrl + S");
            }
        }


        private void NameBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (NameBox.Text == "Name")
            {
                NameBox.Text = "";
            }
        }

        private void NameBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (NameBox.Text == "")
            {
                NameBox.Text = "Name";
            }
        }
    }
    
}
