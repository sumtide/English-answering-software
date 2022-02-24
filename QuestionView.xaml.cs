using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace test
{
    /// <summary>
    /// QuestionView.xaml 的交互逻辑
    /// </summary>
    public partial class QuestionView : Page
    {
        #region 单词字段
        /// <summary>
        /// 单词标记标记集合
        /// </summary>
        private List<int> mark;
        /// <summary>
        /// 单词集合
        /// </summary>
        private List<Word> words;
        /// <summary>
        /// 选项集合
        /// </summary>
        private List<Word> options;
        /// <summary>
        /// 随机数生成类
        /// </summary>
        private Random random;
        /// <summary>
        /// 中英文切换
        /// </summary>
        private int shift = 0;
        /// <summary>
        /// 按钮集合
        /// </summary>
        private List<Button> buttons;
        /// <summary>
        /// 正确选项id
        /// </summary>
        private int temp;
        /// <summary>
        /// 记录正确回答次数
        /// </summary>
        static public int correct;
        /// <summary>
        /// 记录错误回答次数
        /// </summary>
        static public int error;
        /// <summary>
        /// 进度条数值
        /// </summary>
        private double  ProgressBarValue;
        /// <summary>
        /// 模式选择
        /// </summary>
        static public string Mode= "四级必备词汇";
        #endregion

        #region 属性
        public QuestionView()
        {
            InitializeComponent();
            random = new Random();
            words = new List<Word>();
            InitButton();
            SetOptions();

            pbStatus.Value = 0;
            ProgressBarValue = 0;

        }

        #endregion

        # region 方法
        /// <summary>
        /// 初始化button
        /// </summary>
        private void InitButton()
        {
            buttons = new List<Button>(4);
            buttons.Add(SelectOne);
            buttons.Add(SelectTwo);
            buttons.Add(SelectThree);
            buttons.Add(SelectFour);
        }
        /// <summary>
        /// 初始化当前单词容器
        /// </summary>
        private void InitWord()
        {
            words = new List<Word>();
            mark = new List<int>();
            int[] moderange=new int[2];
            moderange = MathTools.GetRange(Mode);
            foreach (var e in MathTools.RandomExtract(random,moderange[0], moderange[1],10))
            {
                Word word = new Word(e);
                words.Add(word);
                mark.Add(word.wrong);
            }
        }
        /// <summary>
        /// 获取单词容器内标记次数最多的四个单词
        /// </summary>
        private void GetOptions()
        {
            if (words.Count == 0)
                InitWord();
            else
                InitButtonEvent();

            options = new List<Word>(4);
            options.AddRange(MathTools.ControllerRandomExtract(random, words, mark, 4));
            if (options[0].right>options[0].wrong*2)
            {
                InitWord();
                options = new List<Word>(4);
                options.AddRange(MathTools.ControllerRandomExtract(random, words, mark, 4));
            }

        }
        /// <summary>
        /// 根据内容获取对应单词
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private Word GetWordID(string content)
        {
            if (shift % 2 == 0)
            {
                foreach (var e in words)
                {
                    if (e.english == content)
                        return e;
                }
            }
            else
            {
                foreach (var e in words)
                {
                    if (e.chinese == content)
                        return e;
                }
            }
            return null;

        }
        /// <summary>
        /// 设置正确选项内容
        /// </summary>
        /// <param name="button"></param>
        /// <param name="word"></param>
        private void SetRight(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            button.CommandParameter = "RIGHT";
            DisableButton();
            Word word = GetWordID(button.Content.ToString());
            word.DecreaseMark();
            SetPrompt(1);
        }
        /// <summary>
        /// 设置错误选项内容
        /// </summary>
        /// <param name="word"></param>
        private void SetWrong(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            button.CommandParameter = "WRONG";     //设置显示command标签，以触发相应触发器
            buttons[temp].CommandParameter = "RIGHT"; //设置显示正确答案
            DisableButton();                       //设置button不点击
            Word word = GetWordID(button.Content.ToString());   //获取绑定在当前button上的类
            word.AddMark();                
            Word question = options[temp];
            question.AddMark();
            SetPrompt(2);
        }
        /// <summary>
        /// 设置选项和问题
        /// </summary>
        /// <param name="options"></param>
        private void SetOptions()
        {
            Next.IsEnabled = false;
            GetOptions();
            temp = random.Next(0, 4);
            if (shift % 2 == 0)    //问题为中文则选项为英文
            {
                 Question.Text = options[temp].chinese;
                for (int i = 0; i < 4; i++)
                {
                    buttons[i].Content = options[i].english;

                    if (i == temp)
                    {
                        buttons[i].Click += SetRight;
                        continue;
                    }
                    buttons[i].Click += SetWrong;
                }
            }
            else                    //问题为英文则选项为中文
            {
                Question.Text = options[temp].english;
                for (int i = 0; i < 4; i++)
                {
                    buttons[i].Content = options[i].chinese;

                    if (i == temp)
                    {
                        buttons[i].Click += SetRight;
                        continue;
                    }
                    buttons[i].Click += SetWrong;
                }
            }
        }
        /// <summary>
        /// 初始化button事件
        /// </summary>
        private void InitButtonEvent()
        {
            for (int i = 0; i < 4; i++)
            {
                if (i == temp)
                    buttons[i].Click -= SetRight;

                else
                     buttons[i].Click -= SetWrong;

                buttons[i].CommandParameter = null;
                buttons[i].IsEnabled = true;
            }
            SetPrompt(3);
        }
        /// <summary>
        /// 禁用button
        /// </summary>
        private void DisableButton()
        {
            foreach (var e in buttons)
            {
                e.IsEnabled = false;
            }
            pbStatus.Value = SetProgressBar();
            Next.IsEnabled = true;
        }
        /// <summary>
        /// 下一题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (ProgressBarValue >=95)
            {
                ChangePage();
            }
            shift++;   
            SetOptions();
        }
        /// <summary>
        /// 设置问题反馈
        /// </summary>
        private void SetPrompt(int mode)
        {
            switch (mode)
            {
                case 1:
                    correct++;
                    Prompt.Text = "correct";
                    break;
                case 2:
                    error++;
                    Prompt.Text = "error";
                    break;
                case 3:
                    Prompt.Text = null;
                    break;
                default:
                    break;
            }
        }

        #region 进度条控制
        private double SetProgressBar()
        {
            GetProgressBarValue();
            return Set(ProgressBarValue);
        }
        private  double Set(double value)
        {
            value += 5;
            return value;
        }
        private double GetProgressBarValue()
        {
            ProgressBarValue = pbStatus.Value;
            return pbStatus.Value;
        }
        #endregion

        /// <summary>
        /// 改变界面
        /// </summary>
        private void ChangePage()
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.question.Visibility = Visibility.Hidden;
            mainWindow.list.Visibility = Visibility.Visible;

            while (correct != 0 || error != 0)
            {
                result show = new result();
                show.ShowDialog();
                UserData userData = new UserData();
                userData.SaveData(correct,error);
                correct = 0;
                error = 0;
            }
              mainWindow.PageContral.Navigate(new Uri("analysis.xaml", UriKind.Relative));
        }

        #endregion

        #region 星星私有成员变量
        /// <summary>
        /// 星星PathData
        /// </summary>
        private Geometry _pathDataStar = PathGeometry.Parse("M16.001007,0L20.944,10.533997 32,12.223022 23.998993,20.421997 25.889008,32 16.001007,26.533997 6.1109924,32 8,20.421997 0,12.223022 11.057007,10.533997z");

        /// <summary>
        /// 星星个数
        /// </summary>
        private int _starCount = 50;

        /// <summary>
        /// 星星最小尺寸
        /// </summary>
        private static int _starSizeMin = 5;

        /// <summary>
        /// 星星最大尺寸
        /// </summary>
        private static int _starSizeMax = 10;

        /// <summary>
        /// 星星运动的最大速度
        /// </summary>
        private int _starVMax = 20;

        /// <summary>
        /// 星星转动的最小速度
        /// </summary>
        private int _starRVMin = 90;

        /// <summary>
        /// 星星转动的最大速度
        /// </summary>
        private int _starRVMax = 360;

        /// <summary>
        /// 倍率 (乘以星星长度)
        /// </summary>
        private int _lineRate = 3;

        /// <summary>
        /// 随机数
        /// </summary>
        private Random _random = new Random();

        /// <summary>
        /// 星星数组
        /// </summary>
        private StarInfo[] _stars;

        #endregion

        #region  星空事件

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //注册帧动画
            CompositionTarget.Rendering += CompositionTarget_Rendering;
            InitStar();
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            CompositionTarget.Rendering -= CompositionTarget_Rendering;
        }

        /// <summary>
        /// 帧渲染事件
        /// </summary>
        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            StarRoamAnimation();
            AddOrRemoveStarLine();
            MoveStarLine();
        }
        #endregion

        #region 星星背景
        /// <summary>
        /// 初始化星星
        /// </summary>
        private void InitStar()
        {
            //清空星星容器
            _stars = new StarInfo[_starCount];
            cvs_starContainer.Children.Clear();
            grid_lineContainer.Children.Clear();
            //生成星星
            for (int i = 0; i < _starCount; i++)
            {
                double size = _random.Next(_starSizeMin, _starSizeMax + 1);//星星尺寸
                StarInfo starInfo = new StarInfo()
                {
                    X = _random.Next(0, (int)cvs_starContainer.ActualWidth),
                    XV = (double)_random.Next(-_starVMax, _starVMax) / 60,
                    XT = _random.Next(6, 301),//帧
                    Y = _random.Next(0, (int)cvs_starContainer.ActualHeight),
                    YV = (double)_random.Next(-_starVMax, _starVMax) / 60,
                    YT = _random.Next(6, 301),//帧
                    StarLines = new Dictionary<StarInfo, Line>()
                };
                Path star = new Path()
                {
                    Data = _pathDataStar,
                    Width = size,
                    Height = size,
                    Stretch = Stretch.Fill,
                    Fill = GetRandomColorBursh(),
                    RenderTransformOrigin = new Point(0.5, 0.5),
                    RenderTransform = new RotateTransform() { Angle = 0 }
                };
                Canvas.SetLeft(star, starInfo.X);
                Canvas.SetTop(star, starInfo.Y);
                starInfo.StarRef = star;
                //设置星星旋转动画
                SetStarRotateAnimation(star);
                //添加到容器
                _stars[i] = starInfo;
                cvs_starContainer.Children.Add(star);
            }
        }

        /// <summary>
        /// 获取随机颜色画刷(偏亮)
        /// </summary>
        /// <returns>SolidColorBrush</returns>
        private SolidColorBrush GetRandomColorBursh()
        {
            byte r = (byte)_random.Next(128, 256);
            byte g = (byte)_random.Next(128, 256);
            byte b = (byte)_random.Next(128, 256);
            return new SolidColorBrush(Color.FromRgb(r, g, b));
        }

        /// <summary>
        /// 设置星星旋转动画
        /// </summary>
        /// <param name="star"></param>
        private void SetStarRotateAnimation(Path star)
        {
            double v = _random.Next(_starRVMin, _starRVMax + 1);//速度
            double a = _random.Next(0, 360 * 5);//角度
            double t = a / v;//时间
            Duration dur = new Duration(new TimeSpan(0, 0, 0, 0, (int)(t * 1000)));

            Storyboard sb = new Storyboard()
            {
                Duration = dur
            };
            //动画完成事件 再次设置此动画
            sb.Completed += (S, E) =>
            {
                SetStarRotateAnimation(star);
            };

            DoubleAnimation da = new DoubleAnimation()
            {
                To = a,
                Duration = dur
            };
            Storyboard.SetTarget(da, star);
            Storyboard.SetTargetProperty(da, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));
            sb.Children.Add(da);
            sb.Begin(this);
        }

        /// <summary>
        /// 星星漫游动画
        /// </summary>
        private void StarRoamAnimation()
        {
            if (_stars == null)
                return;

            foreach (StarInfo starInfo in _stars)
            {
                //X轴运动
                if (starInfo.XT > 0)
                {
                    //运动时间大于0,继续运动
                    if (starInfo.X >= cvs_starContainer.ActualWidth || starInfo.X <= 0)
                    {
                        //碰到边缘,速度取反向
                        starInfo.XV = -starInfo.XV;
                    }
                    //位移加,时间减
                    starInfo.X += starInfo.XV;
                    starInfo.XT--;
                    Canvas.SetLeft(starInfo.StarRef, starInfo.X);
                }
                else
                {
                    //运动时间小于0,重新设置速度和时间
                    starInfo.XV = (double)_random.Next(-_starVMax, _starVMax) / 60;
                    starInfo.XT = _random.Next(100, 1001);
                }
                //Y轴运动
                if (starInfo.YT > 0)
                {
                    //运动时间大于0,继续运动
                    if (starInfo.Y >= cvs_starContainer.ActualHeight || starInfo.Y <= 0)
                    {
                        //碰到边缘,速度取反向
                        starInfo.YV = -starInfo.YV;
                    }
                    //位移加,时间减
                    starInfo.Y += starInfo.YV;
                    starInfo.YT--;
                    Canvas.SetTop(starInfo.StarRef, starInfo.Y);
                }
                else
                {
                    //运动时间小于0,重新设置速度和时间
                    starInfo.YV = (double)_random.Next(-_starVMax, _starVMax) / 60;
                    starInfo.YT = _random.Next(100, 1001);
                }
            }
        }

        /// <summary>
        /// 添加或者移除星星之间的连线
        /// </summary>
        private void AddOrRemoveStarLine()
        {
            //没有星星 直接返回
            if (_stars == null)
                return;

            //生成星星间的连线
            for (int i = 0; i < _starCount - 1; i++)
            {
                for (int j = i + 1; j < _starCount; j++)
                {
                    StarInfo star1 = _stars[i];
                    double x1 = star1.X + star1.StarRef.Width / 2;
                    double y1 = star1.Y + star1.StarRef.Height / 2;
                    StarInfo star2 = _stars[j];
                    double x2 = star2.X + star2.StarRef.Width / 2;
                    double y2 = star2.Y + star2.StarRef.Height / 2;
                    double s = Math.Sqrt((y2 - y1) * (y2 - y1) + (x2 - x1) * (x2 - x1));//两个星星间的距离
                    double threshold = star1.StarRef.Width * _lineRate + star2.StarRef.Width * _lineRate;
                    if (s <= threshold)
                    {
                        if (!star1.StarLines.ContainsKey(star2))
                        {
                            Line line = new Line()
                            {
                                X1 = x1,
                                Y1 = y1,
                                X2 = x2,
                                Y2 = y2,
                                Stroke = GetStarLineBrush(star1.StarRef, star2.StarRef)
                            };
                            star1.StarLines.Add(star2, line);
                            grid_lineContainer.Children.Add(line);
                        }
                    }
                    else
                    {
                        if (star1.StarLines.ContainsKey(star2))
                        {
                            grid_lineContainer.Children.Remove(star1.StarLines[star2]);
                            star1.StarLines.Remove(star2);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 移动星星之间的连线
        /// </summary>
        private void MoveStarLine()
        {
            //没有星星 直接返回
            if (_stars == null)
                return;

            foreach (var star in _stars)
            {
                foreach (var starLine in star.StarLines)
                {
                    Line line = starLine.Value;
                    line.X1 = star.X + star.StarRef.Width / 2;
                    line.Y1 = star.Y + star.StarRef.Height / 2;
                    line.X2 = starLine.Key.X + starLine.Key.StarRef.Width / 2;
                    line.Y2 = starLine.Key.Y + starLine.Key.StarRef.Height / 2;
                }
            }
        }

        /// <summary>
        /// 获取星星连线颜色画刷
        /// </summary>
        /// <param name="star0">起始星星</param>
        /// <param name="star1">终点星星</param>
        /// <returns>LinearGradientBrush</returns>
        private LinearGradientBrush GetStarLineBrush(Path star0, Path star1)
        {
            return new LinearGradientBrush()
            {
                GradientStops = new GradientStopCollection()
                {
                    new GradientStop() { Offset=0,Color=(star0.Fill as SolidColorBrush).Color},
                    new GradientStop() { Offset=1,Color=(star1.Fill as SolidColorBrush).Color}
                }
            };
        }


        #endregion

    }
}
