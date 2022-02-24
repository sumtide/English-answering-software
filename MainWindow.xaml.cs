using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Threading;

namespace test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        /// <summary>
        /// 当选择标签不同时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ribbon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var table = Ribbon.SelectedItem as RibbonTab;
            //if (table.Header.ToString() == "答题界面")
            //{
            //    PageContral.Navigate(new Uri("QuestionView.xaml", UriKind.Relative));
            //}
            //if (table.Header.ToString() == "图表界面")
            //{
            //    while (QuestionView.correct != 0 || QuestionView.error != 0)
            //    {
            //        result show = new result();
            //        show.ShowDialog();
            //        UserData userData = new UserData();
            //        userData.SaveData(QuestionView.correct,QuestionView.error);
            //        QuestionView.correct = 0;
            //        QuestionView.error = 0;
            //    }
            //    PageContral.Navigate(new Uri("analysis.xaml", UriKind.Relative));
            //}
        }


        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            system_time.Text = DateTime.Now.ToString("HH:mm:ss yyyy-MM-dd");
        }
        /// <summary>
        /// Ribbon加载时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ribbon_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(64);
            timer.Tick += Timer_Tick;
            timer.Start();
        }



        private void Quit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ReStart(object sender,RoutedEventArgs e)
        {
            question.Visibility = Visibility.Visible;
            list.Visibility = Visibility.Hidden;
            PageContral.Navigate(new Uri("QuestionView.xaml",UriKind.Relative));
        }
        private void Select(object sender, RoutedEventArgs e)
        {
            Loading loading = new Loading();
            loading.Visibility = Visibility.Visible;
            this.Close();
        }
    }
}
