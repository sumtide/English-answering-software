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
using LiveCharts;
using LiveCharts.Wpf;

namespace test
{
    /// <summary>
    /// analysis.xaml 的交互逻辑
    /// </summary>
    public partial class analysis : Page
    {
        public analysis()
        {
            InitializeComponent();
            InitLine();
        }

        private async  void InitLine()
        {
            UserData userData = new UserData();
            LineSeries correct = new LineSeries();
            correct.Values = new ChartValues<int>(await userData.GetCorrect());
            correct.Title = "正确数";
            Charts.Series.Add(correct);
            LineSeries error = new LineSeries();
            error.Values = new ChartValues<int>(await userData.GetError());
            error.Title = "错误数";
            Charts.Series.Add(error);
        }

    }

}
