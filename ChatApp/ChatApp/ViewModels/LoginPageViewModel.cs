using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml.Controls;
using System.Windows.Input;
using ChatApp.Commands;
using System.Text.RegularExpressions;
using ChatApp.DBModels;

namespace ChatApp.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
{
    private readonly Models.LoginPageModel _loginModel;

    public string LoginEmail
    {
        get { return _loginModel.LoginEmail; }
        set
        {
            if (_loginModel.LoginEmail != value)
            {
                _loginModel.LoginEmail = value;
                OnPropertyChanged(nameof(LoginEmail));
                LoginCommand.RaiseCanExecuteChanged();
                }
        }
    }

    public string LoginPassword
    {
        get { return _loginModel.LoginPassword; }
        set
        {
            if (_loginModel.LoginPassword != value)
            {
                _loginModel.LoginPassword = value;
                    OnPropertyChanged(nameof(LoginPassword));
                    LoginCommand.RaiseCanExecuteChanged();
                }
        }
    }

    public string RegisterEmail
    {
        get { return _loginModel.RegisterEmail; }
            set
            {
                if(_loginModel.RegisterEmail != value)
                {
                    _loginModel.RegisterEmail = value;
                    OnPropertyChanged(nameof(RegisterEmail));
                    RegisterCommand.RaiseCanExecuteChanged();
                }
            }
    }
        public string RegisterUserName
        {
            get { return _loginModel.RegisterUserName; }
            set
            {
                if (_loginModel.RegisterUserName != value)
                {
                    _loginModel.RegisterUserName = value;
                    OnPropertyChanged(nameof(RegisterUserName));
                    RegisterCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public string RegisterPassword
        {
            get { return _loginModel.RegisterPassword; }
            set
            {
                if (_loginModel.RegisterPassword != value)
                {
                    _loginModel.RegisterPassword = value;
                    OnPropertyChanged(nameof(RegisterPassword));
                    RegisterCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public string RegisterPasswordRepeat
        {
            get { return _loginModel.RegisterPasswordRepeat; }
            set
            {
                if (_loginModel.RegisterPasswordRepeat != value)
                {
                    _loginModel.RegisterPasswordRepeat = value;
                    OnPropertyChanged(nameof(RegisterPasswordRepeat));
                    RegisterCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string _logininfoBarMessage;
    public string LoginInfoBarMessage
    {
        get { return _logininfoBarMessage; }
        set
        {
                _logininfoBarMessage = value;
            OnPropertyChanged(nameof(LoginInfoBarMessage));
        }
    }
        private string _registerinfoBarMessage;
        public string RegisterInfoBarMessage
        {
            get { return _registerinfoBarMessage; }
            set
            {
                _registerinfoBarMessage = value;
                OnPropertyChanged(nameof(RegisterInfoBarMessage));
            }
        }
        private string _loginErrorInfo;
        public string LoginErrorInfo
        {
            get { return _loginErrorInfo; }
            set
            {
                _loginErrorInfo = value;
                OnPropertyChanged(nameof(LoginErrorInfo));
            }
        }
        private bool _loginErrorVisibility;
        public bool LoginErrorVisibility
        {
            get { return _loginErrorVisibility; }
            set
            {
                _loginErrorVisibility = value;
                OnPropertyChanged(nameof(LoginErrorVisibility));
            }
        }
    private bool _isLoggedIn;
    public bool IsLoggedIn
    {
        get { return _isLoggedIn; }
        set
        {
            _isLoggedIn = value;
            OnPropertyChanged(nameof(IsLoggedIn));
        }
    }
        private bool _isRegisteredIn;
        public bool IsRegisteredIn
        {
            get { return _isRegisteredIn; }
            set
            {
                _isRegisteredIn = value;
                OnPropertyChanged(nameof(IsRegisteredIn));
            }
        }
    public RelayCommand<string> LoginCommand { get; private set; }
    public RelayCommand<string> RegisterCommand { get; private set; }

        public LoginPageViewModel(Models.LoginPageModel loginModel)
    {
        _loginModel = loginModel;
            LoginCommand = new RelayCommand<string>(x => LoginMessage(), x => this.LoginIsValid());
            RegisterCommand = new RelayCommand<string>(x => RegisterMessage(), x => this.RegisterIsValid);

        }

    public bool LoginIsValid()
        {
            if (string.IsNullOrEmpty(LoginEmail))
            {
                LoginErrorVisibility = true;
                LoginErrorInfo = "Pole E-mail nie może być puste";
                return false;
            }
            if (string.IsNullOrEmpty(LoginPassword))
            {
                LoginErrorVisibility = true;
                LoginErrorInfo = "Pole Hasło nie może być puste";
                return false;
            }
            else if (LoginPassword.Length < 8)
            {
                LoginErrorVisibility = true;
                LoginErrorInfo = "Hasło musi zawierać conajmniej 8 znaków";
                return false;
            }
            else
            {
                LoginErrorVisibility = false;
                return true;
            }

}
        
        //public bool LoginIsValid
        //{
        //    get => !string.IsNullOrEmpty(LoginEmail) && !string.IsNullOrEmpty(LoginPassword) && LoginEmail.Contains("@") && Regex.IsMatch(LoginEmail, @"\.[a-zA-Z]{2,}$");
        //}
        public bool RegisterIsValid
        {
            get => !string.IsNullOrEmpty(RegisterEmail) && !string.IsNullOrEmpty(RegisterUserName) && !string.IsNullOrEmpty(RegisterPassword) && RegisterPassword==RegisterPasswordRepeat && RegisterEmail.Contains("@") && Regex.IsMatch(RegisterEmail, @"\.[a-zA-Z]{2,}$");
        }

        private void LoginMessage()
    {
            IsLoggedIn = true;
            LoginInfoBarMessage = $"Pomyślnie zalogowano przy pomocy maila: {LoginEmail}";
        }
        private void RegisterMessage()
        {
            using var context = new ChatDbContext();
            var RegisterUser = new User
            {
                EMail = RegisterEmail,
                UserName = RegisterUserName,
                Password = RegisterPassword,
                IsLogedIn = false,
                RegisterDate = DateTime.Now
            };
            context.Users.Add(RegisterUser);
            context.SaveChanges();
            IsRegisteredIn = true;
            RegisterInfoBarMessage = $"Pomyślnie zarejestrowano użytkownika o danych:\nE-mail: {RegisterEmail}\nLogin: {RegisterUserName}\nHasło: {RegisterPassword}";
        }
    }
    //public class LoginPageViewModel : BaseViewModel
    //{
    //    private readonly Models.LoginPageModel _loginModel;

    //    public string LoginEmail
    //    {
    //        get { return _loginModel.LoginEmail; }
    //        set
    //        {
    //            if (_loginModel.LoginEmail != value)
    //            {
    //                _loginModel.LoginEmail = value;
    //                OnPropertyChanged();
    //                OnPropertyChanged(nameof(LoginEmail));
    //            }
    //        }
    //    }

    //    public string LoginPassword
    //    {
    //        get {return _loginModel.LoginPassword; } 
    //        set 
    //        {
    //            if ( _loginModel.LoginPassword != value)
    //            { 
    //                _loginModel.LoginPassword = value;
    //                OnPropertyChanged();
    //                OnPropertyChanged(nameof(LoginPassword));
    //            }
    //        }
    //    }
    //    public RelayCommand<string> LoginCommand { get; private set; }
    //    public LoginPageViewModel(Models.LoginPageModel loginModel)
    //    {
    //        _loginModel = loginModel;
    //        LoginCommand = new RelayCommand<string>(x => DisplayMessage(), x => this.IsValid);
    //    }
    //    public bool IsValid { get => !string.IsNullOrEmpty(LoginEmail); }

    //    private void DisplayMessage()
    //    {
    //        var loginInfobar = new InfoBar
    //        {
    //            Message = $"Pomyślnie zalogowano przy pomocy maila: {LoginEmail}",
    //            IsOpen = true
    //        };
    //    }

    //}
}
