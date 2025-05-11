using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using TriggerSystemDemo.Commands;
using TriggerSystemDemo.Models;

namespace TriggerSystemDemo.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly UserModel _userModel;
        private string _username;
        private string _password;
        private string _email;
        private bool _isSubscribed;
        private string _fullName;
        private DateTime? _birthDate;

        // 用于触发器演示的属性
        private bool _isFormValid;
        private bool _isUsernameValid;
        private bool _isPasswordValid;
        private bool _isEmailValid;
        private bool _isMouseOverLoginButton;
        private int _animationState;
        private string _usernameValidationMessage;
        private string _passwordValidationMessage;
        private string _emailValidationMessage;

        public MainViewModel()
        {
            _userModel = new UserModel();
            _birthDate = DateTime.Now.AddYears(-20);
            SubmitCommand = new RelayCommand(ExecuteSubmit, CanExecuteSubmit);
            ResetCommand = new RelayCommand(ExecuteReset);
            AnimateCommand = new RelayCommand(ExecuteAnimate);
        }

        #region 属性

        public string Username
        {
            get => _username;
            set 
            {
                if (SetProperty(ref _username, value))
                {
                    _userModel.Username = value;
                    ValidateUsername();
                    ValidateForm();
                }
            }
        }

        public string Password
        {
            get => _password;
            set 
            {
                if (SetProperty(ref _password, value))
                {
                    _userModel.Password = value;
                    ValidatePassword();
                    ValidateForm();
                }
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (SetProperty(ref _email, value))
                {
                    _userModel.Email = value;
                    ValidateEmail();
                    ValidateForm();
                }
            }
        }

        public bool IsSubscribed
        {
            get => _isSubscribed;
            set
            {
                if (SetProperty(ref _isSubscribed, value))
                {
                    _userModel.IsSubscribed = value;
                }
            }
        }

        public string FullName
        {
            get => _fullName;
            set
            {
                if (SetProperty(ref _fullName, value))
                {
                    _userModel.FullName = value;
                }
            }
        }

        public DateTime? BirthDate
        {
            get => _birthDate;
            set
            {
                if (SetProperty(ref _birthDate, value))
                {
                    _userModel.BirthDate = value;
                }
            }
        }

        // 验证属性
        public bool IsFormValid
        {
            get => _isFormValid;
            private set => SetProperty(ref _isFormValid, value);
        }

        public bool IsUsernameValid
        {
            get => _isUsernameValid;
            private set => SetProperty(ref _isUsernameValid, value);
        }

        public bool IsPasswordValid
        {
            get => _isPasswordValid;
            private set => SetProperty(ref _isPasswordValid, value);
        }

        public bool IsEmailValid
        {
            get => _isEmailValid;
            private set => SetProperty(ref _isEmailValid, value);
        }

        public bool IsMouseOverLoginButton
        {
            get => _isMouseOverLoginButton;
            set => SetProperty(ref _isMouseOverLoginButton, value);
        }

        public int AnimationState
        {
            get => _animationState;
            set => SetProperty(ref _animationState, value);
        }

        public string UsernameValidationMessage
        {
            get => _usernameValidationMessage;
            private set => SetProperty(ref _usernameValidationMessage, value);
        }

        public string PasswordValidationMessage
        {
            get => _passwordValidationMessage;
            private set => SetProperty(ref _passwordValidationMessage, value);
        }

        public string EmailValidationMessage
        {
            get => _emailValidationMessage;
            private set => SetProperty(ref _emailValidationMessage, value);
        }

        #endregion

        #region 命令
        
        public RelayCommand SubmitCommand { get; }
        public RelayCommand ResetCommand { get; }
        public RelayCommand AnimateCommand { get; }

        private void ExecuteSubmit(object parameter)
        {
            if (IsFormValid)
            {
                MessageBox.Show($"表单提交成功！\n用户名: {Username}\n订阅状态: {(IsSubscribed ? "已订阅" : "未订阅")}", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool CanExecuteSubmit(object parameter)
        {
            return IsFormValid;
        }

        private void ExecuteReset(object parameter)
        {
            Username = string.Empty;
            Password = string.Empty;
            Email = string.Empty;
            IsSubscribed = false;
            FullName = string.Empty;
            BirthDate = DateTime.Now.AddYears(-20);
            AnimationState = 0;
        }

        private void ExecuteAnimate(object parameter)
        {
            AnimationState = (AnimationState + 1) % 3;
        }

        #endregion

        #region 验证方法

        private void ValidateForm()
        {
            IsFormValid = IsUsernameValid && IsPasswordValid && (string.IsNullOrEmpty(Email) || IsEmailValid);
        }

        private void ValidateUsername()
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(_userModel) { MemberName = nameof(_userModel.Username) };
            
            IsUsernameValid = Validator.TryValidateProperty(_userModel.Username, context, results);
            UsernameValidationMessage = results.FirstOrDefault()?.ErrorMessage ?? string.Empty;
        }

        private void ValidatePassword()
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(_userModel) { MemberName = nameof(_userModel.Password) };
            
            IsPasswordValid = Validator.TryValidateProperty(_userModel.Password, context, results);
            PasswordValidationMessage = results.FirstOrDefault()?.ErrorMessage ?? string.Empty;
        }

        private void ValidateEmail()
        {
            if (string.IsNullOrEmpty(Email))
            {
                IsEmailValid = true;
                EmailValidationMessage = string.Empty;
                return;
            }
            
            var results = new List<ValidationResult>();
            var context = new ValidationContext(_userModel) { MemberName = nameof(_userModel.Email) };
            
            IsEmailValid = Validator.TryValidateProperty(_userModel.Email, context, results);
            EmailValidationMessage = results.FirstOrDefault()?.ErrorMessage ?? string.Empty;
        }

        #endregion
    }
} 