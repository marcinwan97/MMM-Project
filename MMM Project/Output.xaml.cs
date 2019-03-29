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
    /// Logika interakcji dla klasy Output.xaml
    /// </summary>
    public partial class Output : Window
    {
        public PlotModel MyModel { get; private set; }
        OxyPlot.Series.LineSeries PunktyWy;
        OxyPlot.Series.LineSeries PunktyWe;

        public Output()
        {
            ;
        }

        public void SetData(List<double> wejscie, List<double> wyjscie)
        {
            MyModel = new PlotModel { Title = "Odpowiedź układu:" };
            MyModel.DefaultColors = new List<OxyColor>
            {
                OxyColors.Blue,
                OxyColors.Gray,
            };
            DataContext = this;
            PunktyWy = new LineSeries();
            PunktyWy.Title = "Wyjście";
            PunktyWy.MarkerStroke = OxyPlot.OxyColors.Red;
            PunktyWe = new LineSeries();
            PunktyWe.Title = "Wejście";
            
            for (int i=0; i<wyjscie.Count; i++)
            {
                OxyPlot.DataPoint o = new OxyPlot.DataPoint(i, wyjscie[i]);
                PunktyWy.Points.Add(o);
                OxyPlot.DataPoint p = new OxyPlot.DataPoint(i, wejscie[i]);
                PunktyWe.Points.Add(p);
            }

            MyModel.Series.Add(PunktyWy);
            MyModel.Series.Add(PunktyWe);
            InitializeComponent();
        }
    }
}
