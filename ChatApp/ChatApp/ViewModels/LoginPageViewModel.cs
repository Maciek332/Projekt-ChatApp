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
        private string _infoBarMessage;
        public string InfoBarMessage
        {
            get { return _infoBarMessage; }
            set
            {
                _infoBarMessage = value;
                OnPropertyChanged(nameof(InfoBarMessage));
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
        public RelayCommand<string> LoginCommand { get; private set; }

    public LoginPageViewModel(Models.LoginPageModel loginModel)
    {
        _loginModel = loginModel;
        LoginCommand = new RelayCommand<string>(x => DisplayMessage(), x => this.IsValid);
    }

    public bool IsValid
    {
        get => !string.IsNullOrEmpty(LoginEmail) && !string.IsNullOrEmpty(LoginPassword) && LoginEmail.Contains("@") && Regex.IsMatch(LoginEmail, @"\.[a-zA-Z]{2,}$");
    }

    private void DisplayMessage()
    {

            IsLoggedIn = true;
            InfoBarMessage = $"Pomyślnie zalogowano przy pomocy maila: {LoginEmail}";
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
