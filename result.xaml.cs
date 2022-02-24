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
using LiveCharts;
using LiveCharts.Wpf;

namespace test
{
    /// <summary>
    /// result.xaml 的交互逻辑
    /// </summary>
    public partial class result : Window
    {
        public result()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitPie();
        }
        private void InitPie()
        {
            List<string> name = new List<string> { "正确率","错误率" };
            List<int> ratio = new List<int> { QuestionView.correct, QuestionView.error };
            for (int i = 0; i < name.Count; i++)
            {
                PieSeries series = new PieSeries();
                series.DataLabels = true;
                series.Title = name[i];
                series.Values = new ChartValues<int>();
                series.Values.Add(ratio[i]);
                PieResult.Series.Add(series);
            }

        }
    }
}
