using System;
using System.Windows;
using System.Windows.Controls;

namespace TriggerSystemDemo.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            
            // 对于WPF中的PasswordBox，需要特殊处理数据绑定
            // 因为PasswordBox的Password属性是不可绑定的（出于安全考虑）
            passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModels.MainViewModel viewModel)
            {
                viewModel.Password = passwordBox.Password;
            }
        }
    }
} 