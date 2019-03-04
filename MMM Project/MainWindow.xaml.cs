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

namespace MMM_Project
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double a0, a1, b0, b1, b2;          // parametry transmitancj
        double kp, Ti, Td, Kd;              // nastawy regulatora PID
        double ampl, t, w; 
        public MainWindow()
        {
            

            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                a0 = double.Parse(a0Box.Text);
                a1 = double.Parse(a1Box.Text);
                b0 = double.Parse(b0Box.Text);
                b1 = double.Parse(b1Box.Text);
                b2 = double.Parse(b2Box.Text);
                kp = double.Parse(kpBox.Text);
                Ti = double.Parse(TiBox.Text);
                Td = double.Parse(TdBox.Text);
                Kd = double.Parse(KdBox.Text);
                ampl = double.Parse(amplBox.Text);
                t = double.Parse(tBox.Text);
                w = double.Parse(wBox.Text);
            }
            catch { MessageBox.Show("Niewłaściwe parametry!"); }

        }


    }
}
