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
using ChatApp.Views;

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
                if (_loginModel.RegisterEmail != value)
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
        private bool _registerErrorVisibility;
        public bool RegisterErrorVisibility
        {
            get { return _registerErrorVisibility; }
            set
            {
                _registerErrorVisibility = value;
                OnPropertyChanged(nameof(RegisterErrorVisibility));
            }
        }
        private string _registerErrorInfo;
        public string RegisterErrorInfo
        {
            get { return _registerErrorInfo; }
            set
            {
                _registerErrorInfo = value;
                OnPropertyChanged(nameof(RegisterErrorInfo));
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

        private string _loggedUserName;
        public string LoggedUserNameField
        {
            get { return _loggedUserName; }
            set
            {
                _loggedUserName = value;
                OnPropertyChanged(nameof(LoggedUserNameField));
                LoginCommand.RaiseCanExecuteChanged();
            }
        }
        public RelayCommand<string> LoginCommand { get; private set; }
        public RelayCommand<string> RegisterCommand { get; private set; }

        public LoginPageViewModel(Models.LoginPageModel loginModel)
        {
            _loginModel = loginModel;
            LoginCommand = new RelayCommand<string>(x => LoginMessage(), x => LoginIsValid());
            RegisterCommand = new RelayCommand<string>(x => RegisterMessage(), x => RegisterIsValid());

        }

        public bool LoginIsValid()
        {
            if (string.IsNullOrEmpty(LoginEmail))
            {
                LoginErrorVisibility = true;
                LoginErrorInfo = "Pole E-mail nie może być puste";
                return false;
            }
            else if (!Regex.IsMatch(LoginEmail, @"\.[a-zA-Z]{2,}$"))
            {
                LoginErrorVisibility = true;
                LoginErrorInfo = "Podano niepoprawny Email. Wymagany format to xx@xx.xx";
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
        public bool RegisterIsValid()
        {
            if (string.IsNullOrEmpty(RegisterEmail))
            {
                RegisterErrorVisibility = true;
                RegisterErrorInfo = "Pole E-mail nie może być puste";
                return false;
            }
            else if (!Regex.IsMatch(RegisterEmail, @"\.[a-zA-Z]{2,}$"))
            {
                RegisterErrorVisibility = true;
                RegisterErrorInfo = "Podano niepoprawny Email. Wymagany format to xx@xx.xx";
                return false;
            }
            if(string.IsNullOrEmpty(RegisterUserName))
            {
                RegisterErrorVisibility = true;
                RegisterErrorInfo = "Pole E-mail nie może być puste";
                return false;
            }
            if (string.IsNullOrEmpty(RegisterPassword))
            {
                RegisterErrorVisibility = true; ;
                RegisterErrorInfo = "Pole Hasło nie może być puste";
                return false;
            }
            else if (RegisterPassword.Length < 8)
            {
                RegisterErrorVisibility = true;
                RegisterErrorInfo = "Hasło musi zawierać conajmniej 8 znaków";
                return false;
            }
            if (RegisterPasswordRepeat != RegisterPassword)
            {
                RegisterErrorVisibility = true;
                RegisterErrorInfo = "Podane hasła są różne";
                return false;
            }
            else
            {
                RegisterErrorVisibility = false;
                return true;
            }

        }

        //public bool LoginIsValid
        //{
        //    get => !string.IsNullOrEmpty(LoginEmail) && !string.IsNullOrEmpty(LoginPassword) && LoginEmail.Contains("@") && Regex.IsMatch(LoginEmail, @"\.[a-zA-Z]{2,}$");
        //}
        //public bool RegisterIsValid
        //{
        //    get => !string.IsNullOrEmpty(RegisterEmail) && !string.IsNullOrEmpty(RegisterUserName) && !string.IsNullOrEmpty(RegisterPassword) && RegisterPassword == RegisterPasswordRepeat && RegisterEmail.Contains("@") && Regex.IsMatch(RegisterEmail, @"\.[a-zA-Z]{2,}$");
        //}

        private void LoginMessage()
        {
            using var context = new ChatDbContext();
            var updateLoginStaus = context.Users
                .Where(x => x.EMail == LoginEmail && x.Password == LoginPassword)
                .ToList();
            if (updateLoginStaus.Any())
            {
                foreach (var user in updateLoginStaus)
                {
                    user.IsLogedIn = true;
                }
                context.SaveChanges();
                var LoggedUserName = context.Users
                    .FirstOrDefault(x => x.EMail == LoginEmail);

                LoggedUserNameField = LoggedUserName.UserName;

                IsLoggedIn = true;
                LoginInfoBarMessage = $"Pomyślnie zalogowano jako {LoggedUserNameField}";
                ShellPage.CurrentLoggedUserLabel.Content = LoggedUserNameField;
            }
            else
            {
                IsLoggedIn = false;
                LoginErrorVisibility = true;
                LoginErrorInfo = "Nie znaleziono użytkownika o takich danych";
            }
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
}
