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
        double a0, a1, b0, b1, b2;          // parametry transmitancji
        double kp, Ti, Td, Kd;              // nastawy regulatora PID
        double h, tmax;                     // krok i czas symulacji
        double ampl, t, w;                  // parametry wejscia
        List<double> input;           // stablicowane wejscie
        double c0, c1, c2, c3, d0, d1, d2, d3, d4;  // parametry transmitancji wypadkowej
        public MainWindow()
        {
            

            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                h = double.Parse(hBox.Text);
                tmax = double.Parse(tmaxBox.Text);
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
                Transmitancja_Wypadkowa();
                Tablicuj_Wejscie();
            }
            catch { MessageBox.Show("Niewłaściwe parametry!"); }
        }

        private void Transmitancja_Wypadkowa()
        {
            c3 = kp * a1 * Td * Ti / Kd + kp * a1 * Td * Ti;
            c2 = kp * Ti * a1 + kp * Td * a1 / Kd + kp * Td * Ti * a0 / Kd + kp * Td * Ti * a0;
            c1 = kp * a1 + kp * Ti * a0 + kp * Td * a0 / Kd;
            c0 = kp * a0;
            d4 = Td * Ti * b2 / Kd;
            d3 = Ti * b2 + Td * Ti * b1 / Kd + kp * a1 * Td * Ti / Kd + kp * a1 * Td * Ti;
            d2 = Ti * b1 + Td * Ti * b0 / Kd + kp * Ti * a1 + kp * Td * a1 / Kd + kp * Td * Ti * a0 / Kd + kp * Td * Ti * a0;
            d1 = Ti * b0 + kp * a1 + kp * Ti * a0 + kp * Td * a0 / Kd;
            d0 = kp * a0;
        }

        private void Tablicuj_Wejscie()
        {
            if (Rskok.IsChecked == true) Tablicuj_Skok();
            else if (Rsin.IsChecked == true) Tablicuj_Sinus();
            else Tablicuj_Trojkat();
        }

        private void Tablicuj_Skok()
        {
            ;
        }

        private void Tablicuj_Sinus()
        {
            ;
        }

        private void Tablicuj_Trojkat()
        {
            ;
        }

    }
}
