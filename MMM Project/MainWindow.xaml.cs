﻿using System;
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

                Tablicuj_Wejscie();
                Transmitancja_Wypadkowa();                  // G=(2*s^3+4*s^2+3*s+1)/(s^4+4*s^3+6*s^2+4*s+1) dla parametrów =1
            }
            catch { MessageBox.Show("Niewłaściwe parametry!"); }
            Calkuj_Wejscie();
            Licz_Wyjscie();
            int stop = 0;                                   // miejsce na pułapkę
            Czyszczenie();
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

        private void Calkuj_Wejscie()
        {
            int j = 1;
            calka_we1.Add(0);
            for (double i = 0; i <= tmax; i = i + h)
            {
                calka_we1.Add(calka_we1[j-1]+wejscie[j]*h);
                j++;
            }
            j = 1;
            calka_we2.Add(0);
            for (double i = 0; i <= tmax; i = i + h)
            {
                calka_we2.Add(calka_we2[j-1]+calka_we1[j] * h);
                j++;
            }
            j = 1;
            calka_we3.Add(0);
            for (double i = 0; i <= tmax; i = i + h)
            {
                calka_we3.Add(calka_we3[j-1]+calka_we2[j] * h);
                j++;
            }
            j = 1;
            calka_we4.Add(0);
            for (double i = 0; i <= tmax; i = i + h)
            {
                calka_we4.Add(calka_we4[j-1]+calka_we3[j] * h);
                j++;
            }
        }

        private void Tablicuj_Skok()
        {
            for (double i=0; i<= tmax+h ; i=i+h )
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

        private void Tablicuj_Sinus()
        {
            for (double i = 0; i <= tmax+h; i = i + h)
            {
                wejscie.Add(ampl * Math.Sin(w * i));
            }
        }

        private void Tablicuj_Trojkat()
        {
            ;
        }

        private void Licz_Wyjscie()
        {
            calka_wy1.Add(0);
            calka_wy2.Add(0);
            calka_wy3.Add(0);
            calka_wy4.Add(0);
            wyjscie.Add(0);
            double wynik = 0;
            int j = 1;
            for (double i = 0; i <= tmax; i = i + h)
            {
                calka_wy1.Add(calka_wy1[j - 1] + wyjscie[j-1] * h);
                calka_wy2.Add(calka_wy2[j - 1] + calka_wy1[j] * h);
                calka_wy3.Add(calka_wy3[j - 1] + calka_wy2[j] * h);
                calka_wy4.Add(calka_wy4[j - 1] + calka_wy3[j] * h);
                wynik = c3 / d4 * calka_we1[j] + c2 / d4 * calka_we2[j] + c1 / d4 * calka_we3[j] + c0 / d4 * calka_we4[j] - d3/d4*calka_wy1[j] - d2/d4*calka_wy2[j] - d1/d4*calka_wy3[j] - d0/d4*calka_wy4[j];
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
