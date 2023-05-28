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

        private bool _isRegistering;
        public bool IsRegistering
        {
            get { return _isRegistering; }
            set
            {
                _isRegistering = value;
                OnPropertyChanged(nameof(IsRegistering));
            }
        }

        private bool _isLogging;
        public bool IsLogging
        {
            get { return _isLogging; }
            set
            {
                _isLogging = value;
                OnPropertyChanged(nameof(IsLogging));
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
            LoginCommand = new RelayCommand<string>(x => LoginMessage(), x => LoginIsValid);
            RegisterCommand = new RelayCommand<string>(x => RegisterMessage(), x => RegisterIsValid);
            TryingLogin = true;
            TryingRegister = true;
            LoginErrorVisibility = false;

        }
        public bool LoginValidation()
        {

            if (!Regex.IsMatch(LoginEmail, @"\.[a-zA-Z]{2,}$"))
            {
                LoginErrorVisibility = true;
                LoginErrorInfo = "Podano niepoprawny format E-mail";
                return false;
            }
            if (string.IsNullOrEmpty(LoginPassword))
            {
                LoginErrorVisibility = true;
                LoginErrorInfo = "Pole Hasło nie może być puste";
                return false;
            }
            if (LoginPassword.Length < 8)
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

        public bool LoginIsValid
        {
            get => !string.IsNullOrEmpty(LoginEmail)&& !string.IsNullOrEmpty(LoginPassword);
        }
        public bool RegisterIsValid
        {
            get => !string.IsNullOrEmpty(RegisterEmail) && !string.IsNullOrEmpty(RegisterUserName) && !string.IsNullOrEmpty(RegisterPassword) && !string.IsNullOrEmpty(RegisterPasswordRepeat);
        }
        
        public bool RegisterValidation()
        {
            if (!Regex.IsMatch(RegisterEmail, @"\.[a-zA-Z]{2,}$"))
            {
                RegisterErrorVisibility = true;
                RegisterErrorInfo = "Podano niepoprawny Email";
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
            if (RegisterPassword.Length < 8)
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
        
        private bool _tryingRegister;
        public bool TryingRegister
        {
            get { return _tryingRegister; }
            set
            {
                _tryingRegister = value;
                OnPropertyChanged(nameof(TryingRegister));
            }
        }
        private bool _tryingLogin;
        public bool TryingLogin
        {
            get { return _tryingLogin; }
            set
            {
                _tryingLogin = value;
                OnPropertyChanged(nameof(TryingLogin));
            }
        }
        private bool LoginStatushelp;
        private string LoginStatusmessageHelp;
        public static User LoggedUser;

        private async void LoginMessage()
        {
            //LoginErrorVisibility = false;
            
            var loginValidated = LoginValidation();
            if (loginValidated) 
            {
                IsLogging = true;
                TryingLogin = false;
                await UpdateLoggedUserAsync();
                IsLoggedIn = LoginStatushelp;
                if (IsLoggedIn != false)
                {
                    LoggedUserNameField = LoggedUser.UserName;
                }

                if (IsLoggedIn)
                {
                    LoginInfoBarMessage = LoginStatusmessageHelp;
                    ShellPage.CurrentLoggedUserLabel.Content = LoggedUserNameField;
                    ShellPage.PrivateMessagesLabel.IsEnabled = true;
                    ShellPage.GroupMessagesLabel.IsEnabled = true;
                    ShellPage.LogoutLabel.IsEnabled = true;
                    TryingLogin = false;
                }

                else
                {
                    LoginErrorVisibility = true;
                    LoginErrorInfo = LoginStatusmessageHelp;
                    TryingLogin = true;
                    
                }
                IsLogging = false;

            }
        }
            

        private async Task UpdateLoggedUserAsync()
        {
            var result = Task.Run(() =>
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

                    LoginStatushelp = true;
                    LoggedUser = LoggedUserName;
                    LoginStatusmessageHelp = $"Pomyślnie zalogowano jako {LoggedUserName.UserName}";
                }

                else
                {
                    LoginStatushelp = false;
                    LoginStatusmessageHelp = "Nie znaleziono użytkownika o takich danych";
                }
            });
            await result;
        }
        private bool RegisterStatushelp;
        private string RegisterStatusmessageHelp;
        private async void RegisterMessage()
        {
            
            var registerValidated = RegisterValidation();
            if (registerValidated)
            {
                TryingRegister = false;
                IsRegistering = true;
                await RegisterTask();
                IsRegistering = false;
                IsRegisteredIn = RegisterStatushelp;
                RegisterInfoBarMessage = RegisterStatusmessageHelp;
                if (!IsRegisteredIn)
                {
                    RegisterErrorInfo = RegisterStatusmessageHelp;
                    RegisterErrorVisibility = true;
                }
            }
            else 
            {
                RegisterStatusmessageHelp = RegisterErrorInfo;
                RegisterErrorVisibility = true;
            }
            TryingRegister = true;
        }

        private async Task RegisterTask()
        {
            var result = Task.Run(() =>
            {
                using var context = new ChatDbContext();
                var checkCanRegister = context.Users
                    .FirstOrDefault(e => e.EMail == RegisterEmail);
                if ( checkCanRegister == null)
                {
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
                    RegisterStatusmessageHelp = $"Pomyślnie zarejestrowano użytkownika o danych:\nE-mail: {RegisterEmail}\nLogin: {RegisterUserName}.\n Możesz teraz się zalogować";
                    RegisterStatushelp = true;
                }
                else
                {
                    RegisterStatusmessageHelp = $"Podany email jest już w użyciu";
                    RegisterStatushelp = false;

                }
                
            });
            await result;
        
        }
    }
}
