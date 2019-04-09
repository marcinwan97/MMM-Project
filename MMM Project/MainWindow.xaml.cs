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
        double c0, c1, c2, c3, d0, d1, d2, d3, d4;  // parametry transmitancji wypadkowej
        bool czy_wypadkowa = false;
        List<double> wejscie = new List<double>();
        List<double> calka_we1 = new List<double>();
        List<double> calka_we2 = new List<double>();
        List<double> calka_we3 = new List<double>();
        List<double> calka_we4 = new List<double>();
        List<double> calka_wy1 = new List<double>();
        List<double> calka_wy2 = new List<double>();
        List<double> calka_wy3 = new List<double>();
        List<double> calka_wy4 = new List<double>();
        List<double> wyjscie = new List<double>();
        public MainWindow()
        {
            

            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                h = NaLiczbe(hBox.Text);
                tmax = NaLiczbe(tmaxBox.Text);
                a0 = NaLiczbe(a0Box.Text);
                a1 = NaLiczbe(a1Box.Text);
                b0 = NaLiczbe(b0Box.Text);
                b1 = NaLiczbe(b1Box.Text);
                b2 = NaLiczbe(b2Box.Text);
                kp = NaLiczbe(kpBox.Text);
                Ti = NaLiczbe(TiBox.Text);
                Td = NaLiczbe(TdBox.Text);
                Kd = NaLiczbe(KdBox.Text);
                if (czy_wypadkowa)
                {
                    c0 = NaLiczbe(c0Box.Text);
                    c1 = NaLiczbe(c1Box.Text);
                    c2 = NaLiczbe(c2Box.Text);
                    c3 = NaLiczbe(c3Box.Text);
                    d0 = NaLiczbe(d0Box.Text);
                    d1 = NaLiczbe(d1Box.Text);
                    d2 = NaLiczbe(d2Box.Text);
                    d3 = NaLiczbe(d3Box.Text);
                    d4 = NaLiczbe(d4Box.Text);
                }
                else TransmitancjaWypadkowa();                               // G=(2*s^3+4*s^2+3*s+1)/(s^4+4*s^3+6*s^2+4*s+1) dla parametrów =1
                ampl = NaLiczbe(amplBox.Text);
                t = NaLiczbe(tBox.Text);
                w = NaLiczbe(wBox.Text); ;
                TablicujWejscie();
                CalkujWejscie();
                LiczWyjscie();
                Output output = new Output();
                output.SetData(wejscie, wyjscie);
                output.Show();
            }
            catch { MessageBox.Show("Niewłaściwe parametry!"); } 

            //int stop = 0;                                   // miejsce na pułapkę
            Czyszczenie();
        }

        private void Wypadkowa_Click(object sender, RoutedEventArgs e)
        {
            czy_wypadkowa = true;
            PIDGo.Opacity = 1; PIDGo.IsEnabled = true; Panel.SetZIndex(PIDGo, 0); Panel.SetZIndex(Wypadkowa, -1);

            Gpid.Opacity = 0;
            Go.Opacity = 0;
            NastawyPID.Opacity = 0;
            ParametryO.Opacity = 0;
            a1Box.Opacity = 0; a1Box.IsEnabled = false; a1Label.Opacity = 0; Panel.SetZIndex(a1Box, -1);
            a0Box.Opacity = 0; a0Box.IsEnabled = false; a0Label.Opacity = 0; Panel.SetZIndex(a0Box, -1);
            b2Box.Opacity = 0; b2Box.IsEnabled = false; b2Label.Opacity = 0; Panel.SetZIndex(b2Box, -1);
            b1Box.Opacity = 0; b1Box.IsEnabled = false; b1Label.Opacity = 0; Panel.SetZIndex(b1Box, -1);
            b0Box.Opacity = 0; b0Box.IsEnabled = false; b0Label.Opacity = 0; Panel.SetZIndex(b0Box, -1);
            kpBox.Opacity = 0; kpBox.IsEnabled = false; kpLabel.Opacity = 0; Panel.SetZIndex(kpBox, -1);
            TiBox.Opacity = 0; TiBox.IsEnabled = false; TiLabel.Opacity = 0; Panel.SetZIndex(TiBox, -1);
            TdBox.Opacity = 0; TdBox.IsEnabled = false; TdLabel.Opacity = 0; Panel.SetZIndex(TdBox, -1);
            KdBox.Opacity = 0; KdBox.IsEnabled = false; KdLabel.Opacity = 0; Panel.SetZIndex(KdBox, -1);

            Gwyp.Opacity = 1;
            ParametryWyp.Opacity = 1;
            c3Box.Opacity = 1; c3Box.IsEnabled = true; c3Label.Opacity = 1; Panel.SetZIndex(c3Box, 0);
            c2Box.Opacity = 1; c2Box.IsEnabled = true; c2Label.Opacity = 1; Panel.SetZIndex(c2Box, 0);
            c1Box.Opacity = 1; c1Box.IsEnabled = true; c1Label.Opacity = 1; Panel.SetZIndex(c1Box, 0);
            c0Box.Opacity = 1; c0Box.IsEnabled = true; c0Label.Opacity = 1; Panel.SetZIndex(c0Box, 0);
            d4Box.Opacity = 1; d4Box.IsEnabled = true; d4Label.Opacity = 1; Panel.SetZIndex(d4Box, 0);
            d3Box.Opacity = 1; d3Box.IsEnabled = true; d3Label.Opacity = 1; Panel.SetZIndex(d3Box, 0);
            d2Box.Opacity = 1; d2Box.IsEnabled = true; d2Label.Opacity = 1; Panel.SetZIndex(d2Box, 0);
            d1Box.Opacity = 1; d1Box.IsEnabled = true; d1Label.Opacity = 1; Panel.SetZIndex(d1Box, 0);
            d0Box.Opacity = 1; d0Box.IsEnabled = true; d0Label.Opacity = 1; Panel.SetZIndex(d0Box, 0);
        }

        private void PIDGo_Click(object sender, RoutedEventArgs e)
        {
            czy_wypadkowa = false;
            Wypadkowa.Opacity = 1; Wypadkowa.IsEnabled = true; Panel.SetZIndex(Wypadkowa, 0); Panel.SetZIndex(PIDGo, -1);

            Gpid.Opacity = 1;
            Go.Opacity = 1;
            NastawyPID.Opacity = 1;
            ParametryO.Opacity = 1;
            a1Box.Opacity = 1; a1Box.IsEnabled = true; a1Label.Opacity = 1; Panel.SetZIndex(a1Box, 0);
            a0Box.Opacity = 1; a0Box.IsEnabled = true; a0Label.Opacity = 1; Panel.SetZIndex(a0Box, 0);
            b2Box.Opacity = 1; b2Box.IsEnabled = true; b2Label.Opacity = 1; Panel.SetZIndex(b2Box, 0);
            b1Box.Opacity = 1; b1Box.IsEnabled = true; b1Label.Opacity = 1; Panel.SetZIndex(b1Box, 0);
            b0Box.Opacity = 1; b0Box.IsEnabled = true; b0Label.Opacity = 1; Panel.SetZIndex(b0Box, 0);
            kpBox.Opacity = 1; kpBox.IsEnabled = true; kpLabel.Opacity = 1; Panel.SetZIndex(kpBox, 0);
            TiBox.Opacity = 1; TiBox.IsEnabled = true; TiLabel.Opacity = 1; Panel.SetZIndex(TiBox, 0);
            TdBox.Opacity = 1; TdBox.IsEnabled = true; TdLabel.Opacity = 1; Panel.SetZIndex(TdBox, 0);
            KdBox.Opacity = 1; KdBox.IsEnabled = true; KdLabel.Opacity = 1; Panel.SetZIndex(KdBox, 0);

            Gwyp.Opacity = 0;
            ParametryWyp.Opacity = 0;
            c3Box.Opacity = 0; c3Box.IsEnabled = false; c3Label.Opacity = 0; Panel.SetZIndex(c3Box, -1);
            c2Box.Opacity = 0; c2Box.IsEnabled = false; c2Label.Opacity = 0; Panel.SetZIndex(c2Box, -1);
            c1Box.Opacity = 0; c1Box.IsEnabled = false; c1Label.Opacity = 0; Panel.SetZIndex(c1Box, -1);
            c0Box.Opacity = 0; c0Box.IsEnabled = false; c0Label.Opacity = 0; Panel.SetZIndex(c0Box, -1);
            d4Box.Opacity = 0; d4Box.IsEnabled = false; d4Label.Opacity = 0; Panel.SetZIndex(d4Box, -1);
            d3Box.Opacity = 0; d3Box.IsEnabled = false; d3Label.Opacity = 0; Panel.SetZIndex(d3Box, -1);
            d2Box.Opacity = 0; d2Box.IsEnabled = false; d2Label.Opacity = 0; Panel.SetZIndex(d2Box, -1);
            d1Box.Opacity = 0; d1Box.IsEnabled = false; d1Label.Opacity = 0; Panel.SetZIndex(d1Box, -1);
            d0Box.Opacity = 0; d0Box.IsEnabled = false; d0Label.Opacity = 0; Panel.SetZIndex(d0Box, -1);
        }

        private double NaLiczbe(string wpisane)
        {
            return double.Parse(wpisane.Replace('.', ','));
        }

        private void TransmitancjaWypadkowa()
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

        private void TablicujWejscie()
        {
            if (Rskok.IsChecked == true) TablicujSkok();
            else if (Rsin.IsChecked == true) TablicujSinus();
            else TablicujTrojkat();
        }

        private void CalkujWejscie()
        {
            int j = 1;
            calka_we1.Add(0);
            for (double i = 0; i < tmax-h; i = i + h)
            {
                calka_we1.Add(calka_we1[j-1]+wejscie[j]*h);
                j++;
            }
            j = 1;
            calka_we2.Add(0);
            for (double i = 0; i < tmax-h; i = i + h)
            {
                calka_we2.Add(calka_we2[j-1]+calka_we1[j] * h);
                j++;
            }
            j = 1;
            calka_we3.Add(0);
            for (double i = 0; i < tmax-h; i = i + h)
            {
                calka_we3.Add(calka_we3[j-1]+calka_we2[j] * h);
                j++;
            }
            j = 1;
            calka_we4.Add(0);
            for (double i = 0; i < tmax-h; i = i + h)
            {
                calka_we4.Add(calka_we4[j-1]+calka_we3[j] * h);
                j++;
            }
        }

        private void TablicujSkok()
        {
            for (double i=0; i< tmax ; i=i+h )
            {
                if (i < t)
                {
                    wejscie.Add(ampl);
                }
                else
                {
                    wejscie.Add(0);
                }
                
            }
        }

        private void TablicujSinus()
        {
            for (double i = 0; i < tmax; i = i + h)
            {
                wejscie.Add(ampl * Math.Sin(w * i));
            }
        }

        private void TablicujTrojkat()
        {
            double a = 0;
            double szczyt = 0;
            for(double j=0; j<=tmax; j=j+t)
            { 
            for (double i = a; i < t; i = i + h)
            {
                if (i > (t / 2))
                {                    
                    wejscie.Add(ampl*(szczyt*2/t));
                    szczyt = szczyt - h;
                }
                else
                {
                    wejscie.Add(ampl*(i*2/t));
                        szczyt = i;
                }

            }
                a = h;
            }
        }

        private void LiczWyjscie()
        {
            int rzad = 4;
            if (d4 == 0) rzad = 3;
            if (d3 == 0 && d4 == 0) rzad = 2;
            if (d2 == 0 && d3 == 0 && d4 == 0) rzad = 1;
            if (d1 == 0 && d2 == 0 && d3 == 0 && d4 == 0) rzad = 0;
            calka_wy1.Add(0);
            calka_wy2.Add(0);
            calka_wy3.Add(0);
            calka_wy4.Add(0);
            wyjscie.Add(0);
            double wynik = 0;
            int j = 1;
            for (double i = 0; i < tmax-h; i = i + h)
            {
                calka_wy1.Add(calka_wy1[j - 1] + wyjscie[j-1] * h);
                calka_wy2.Add(calka_wy2[j - 1] + calka_wy1[j] * h);
                calka_wy3.Add(calka_wy3[j - 1] + calka_wy2[j] * h);
                calka_wy4.Add(calka_wy4[j - 1] + calka_wy3[j] * h);
                switch(rzad)
                {
                    case 4: wynik = c3 / d4 * calka_we1[j] + c2 / d4 * calka_we2[j] + c1 / d4 * calka_we3[j] + c0 / d4 * calka_we4[j] - d3 / d4 * calka_wy1[j] - d2 / d4 * calka_wy2[j] - d1 / d4 * calka_wy3[j] - d0 / d4 * calka_wy4[j];
                        break;
                    case 3: wynik = c2 / d3 * calka_we1[j] + c1 / d3 * calka_we2[j] + c0 / d3 * calka_we3[j] - d2 / d3 * calka_wy1[j] - d1 / d3 * calka_wy2[j] - d0 / d3 * calka_wy3[j];
                        break;
                    case 2: wynik = c1 / d2 * calka_we1[j] + c0 / d2 * calka_we2[j] - d1 / d2 * calka_wy1[j] - d0 / d2 * calka_wy2[j];
                        break;
                    case 1: wynik = c0 / d1 * calka_we1[j] - d0 / d1 * calka_wy1[j];
                        break;
                    default: wynik = 5;
                        break;
                }
                wyjscie.Add(wynik);
                j++;
            }
        }

        private void Czyszczenie()
        {
            wejscie.Clear();
            wyjscie.Clear();
            calka_we1.Clear();
            calka_we2.Clear();
            calka_we3.Clear();
            calka_we4.Clear();
            calka_wy1.Clear();
            calka_wy2.Clear();
            calka_wy3.Clear();
            calka_wy4.Clear();
        }
    }
}
