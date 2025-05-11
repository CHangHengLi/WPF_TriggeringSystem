using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Input;

namespace TriggerSystemDemo.Views
{
    public partial class ExtendedExamplesView : UserControl
    {
        // 保存按钮原始背景色，用于恢复
        private Brush _originalBackground;
        // 引用动画Storyboard
        private Storyboard _buttonAnimation;

        public ExtendedExamplesView()
        {
            InitializeComponent();
            
            // 在页面完全加载后初始化背景和动画
            this.Loaded += (s, e) =>
            {
                if (priorityButton != null)
                {
                    _originalBackground = priorityButton.Background;
                }
                
                // 获取定义在XAML中的Storyboard资源
                if (this.Resources != null)
                {
                    _buttonAnimation = this.FindResource("buttonAnimation") as Storyboard;
                }
            };
        }

        /// <summary>
        /// 移除按钮的本地背景设置，让样式触发器生效
        /// </summary>
        private void RemoveLocalBackground_Click(object sender, RoutedEventArgs e)
        {
            // 清除本地设置的背景色
            if (priorityButton != null)
            {
                // 彻底清除背景属性及其可能的动画效果
                priorityButton.ClearValue(Button.BackgroundProperty);
                
                // 额外确保清除背景动画残留
                Storyboard clearStoryboard = new Storyboard();
                ColorAnimation clearAnimation = new ColorAnimation();
                clearAnimation.To = null;  // 设置为null会清除动画值
                clearAnimation.Duration = TimeSpan.Zero;
                Storyboard.SetTarget(clearAnimation, priorityButton);
                Storyboard.SetTargetProperty(clearAnimation, new PropertyPath("(Button.Background).(SolidColorBrush.Color)"));
                clearStoryboard.Children.Add(clearAnimation);
                clearStoryboard.Begin();
                
                MessageBox.Show(
                    "已移除本地背景值和清除动画残留效果。现在鼠标悬停时会显示绿色(样式触发器)，否则显示蓝色(样式Setter)。",
                    "触发器优先级演示", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// 恢复按钮的本地背景设置
        /// </summary>
        private void RestoreLocalBackground_Click(object sender, RoutedEventArgs e)
        {
            // 恢复本地背景色
            if (priorityButton != null && _originalBackground != null)
            {
                priorityButton.Background = _originalBackground;
                
                MessageBox.Show(
                    "已恢复本地背景值(LightGray)。由于本地值优先级高于样式触发器和样式Setter，鼠标悬停不会改变颜色。",
                    "触发器优先级演示", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// 开始动画
        /// </summary>
        private void StartAnimation_Click(object sender, RoutedEventArgs e)
        {
            Storyboard animation = this.Resources["buttonAnimation"] as Storyboard;
            if (animation != null)
            {
                animation.Begin();
            }
        }
        
        /// <summary>
        /// 暂停动画
        /// </summary>
        private void PauseAnimation_Click(object sender, RoutedEventArgs e)
        {
            Storyboard animation = this.Resources["buttonAnimation"] as Storyboard;
            if (animation != null)
            {
                animation.Pause();
            }
        }
        
        /// <summary>
        /// 恢复动画
        /// </summary>
        private void ResumeAnimation_Click(object sender, RoutedEventArgs e)
        {
            Storyboard animation = this.Resources["buttonAnimation"] as Storyboard;
            if (animation != null)
            {
                animation.Resume();
            }
        }
        
        /// <summary>
        /// 停止动画
        /// </summary>
        private void StopAnimation_Click(object sender, RoutedEventArgs e)
        {
            Storyboard animation = this.Resources["buttonAnimation"] as Storyboard;
            if (animation != null)
            {
                animation.Stop();
            }
        }

        /// <summary>
        /// 使用C#代码创建带触发器的按钮
        /// </summary>
        private void CreateButtonWithTrigger_Click(object sender, RoutedEventArgs e)
        {
            if (codeTriggerPanel == null) return;
            
            // 清空面板(除了第一个按钮)
            while (codeTriggerPanel.Children.Count > 1)
            {
                codeTriggerPanel.Children.RemoveAt(1);
            }
            
            // 添加说明文本
            TextBlock description = new TextBlock
            {
                Text = "以下是通过C#代码动态创建的按钮：",
                Margin = new Thickness(0, 10, 0, 10)
            };
            codeTriggerPanel.Children.Add(description);

            // 1. 创建一个按钮样式
            Style buttonStyle = new Style(typeof(Button));

            // 2. 添加基本样式设置
            buttonStyle.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.Blue));
            buttonStyle.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.White));
            buttonStyle.Setters.Add(new Setter(Button.FontWeightProperty, FontWeights.Bold));
            buttonStyle.Setters.Add(new Setter(Button.PaddingProperty, new Thickness(10, 5, 10, 5)));
            
            // 3. 创建鼠标悬停触发器
            Trigger mouseOverTrigger = new Trigger();
            mouseOverTrigger.Property = Button.IsMouseOverProperty;
            mouseOverTrigger.Value = true;
            mouseOverTrigger.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.DarkBlue));
            mouseOverTrigger.Setters.Add(new Setter(Button.CursorProperty, System.Windows.Input.Cursors.Hand));

            // 4. 创建鼠标按下触发器
            Trigger mousePressedTrigger = new Trigger();
            mousePressedTrigger.Property = Button.IsPressedProperty;
            mousePressedTrigger.Value = true;
            mousePressedTrigger.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.Navy));
            
            // 5. 创建禁用状态触发器
            Trigger disabledTrigger = new Trigger();
            disabledTrigger.Property = Button.IsEnabledProperty;
            disabledTrigger.Value = false;
            disabledTrigger.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.Gray));
            disabledTrigger.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.LightGray));

            // 6. 将触发器添加到样式
            buttonStyle.Triggers.Add(mouseOverTrigger);
            buttonStyle.Triggers.Add(mousePressedTrigger);
            buttonStyle.Triggers.Add(disabledTrigger);

            // 7. 创建启用状态按钮并应用样式
            Button myButton1 = new Button();
            myButton1.Content = "代码创建的按钮";
            myButton1.Width = 150;
            myButton1.Height = 30;
            myButton1.Margin = new Thickness(0, 0, 0, 10);
            myButton1.Style = buttonStyle;

            // 8. 创建禁用状态按钮并应用相同样式
            Button myButton2 = new Button();
            myButton2.Content = "代码创建的禁用按钮";
            myButton2.Width = 150;
            myButton2.Height = 30;
            myButton2.IsEnabled = false;
            myButton2.Style = buttonStyle;

            // 9. 添加按钮到面板
            codeTriggerPanel.Children.Add(myButton1);
            codeTriggerPanel.Children.Add(myButton2);
            
            // 10. 添加事件触发器演示
            TextBlock eventTriggerText = new TextBlock
            {
                Text = "使用代码添加事件触发器（点击按钮会变宽）：",
                Margin = new Thickness(0, 20, 0, 10)
            };
            codeTriggerPanel.Children.Add(eventTriggerText);
            
            // 11. 创建带事件触发器的按钮
            Button eventButton = new Button();
            eventButton.Content = "点击我触发动画";
            eventButton.Width = 150;
            eventButton.Height = 30;
            
            // 12. 创建Storyboard
            Storyboard storyboard = new Storyboard();
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 150;
            animation.To = 250;
            animation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            animation.AutoReverse = true;
            Storyboard.SetTargetProperty(animation, new PropertyPath(Button.WidthProperty));
            storyboard.Children.Add(animation);
            
            // 13. 添加事件处理程序来启动动画
            eventButton.Click += (s, args) => storyboard.Begin(eventButton);
            
            // 14. 添加到面板
            codeTriggerPanel.Children.Add(eventButton);
        }
    }
} 