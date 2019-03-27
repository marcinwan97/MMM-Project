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
using System.Windows.Shapes;
using OxyPlot;
using OxyPlot.Series;

namespace MMM_Project
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class Output : Window
    {
        private MainViewModel viewModel;
        public PlotModel MyModel { get; private set; }
        //public IList<OxyPlot.DataPoint> Points { get; private set; }
        OxyPlot.Series.LineSeries punkty;

        public Output()
        {
            //InitializeComponent();
        }

        public void SetData(List<double> wejscie, List<double> wyjscie)
        {
            viewModel = new MainViewModel();
            DataContext = viewModel;
            punkty = new LineSeries();
            // viewModel.MyModel.Series.Add(new FunctionSeries(Math.Sin, 0, 10, 0.1, "Wyjście"));
           // this.Points = new List<OxyPlot.DataPoint>();
            for(int i=0; i<wyjscie.Count; i++)
            {
                OxyPlot.DataPoint o = new OxyPlot.DataPoint(i, wyjscie[i]);
                punkty.Points.Add(o);
            }
            viewModel.MyModel.Series.Add(punkty);
            InitializeComponent();
        }
    }
}
