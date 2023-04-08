using ChatApp.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using ChatApp.Views;
using System.Security.Cryptography.X509Certificates;
using Windows.Storage;
using System.Diagnostics.Eventing.Reader;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Windows.UI.Core;

namespace ChatApp.Views
{

    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            string RegisterResult;
            if (RegEmail.Text != "" || RegPassword.Password != "" || RegUsername.Text != "" || RegPasswordRepeat.Password !="")
            {
                if (RegPassword.Password == RegPasswordRepeat.Password)
                {
                    if (RegEmail.Text.Contains("@"))
                    {

                            using var context = new ChatDbContext();
                            var RegisterUser = new User
                            {
                                EMail = RegEmail.Text,
                                UserName = RegUsername.Text,
                                Password = RegPassword.Password,
                                IsLogedIn = false,
                                RegisterDate = DateTime.Now
                            };
                            context.Users.Add(RegisterUser);
                            context.SaveChanges();

                            RegisterResult = $"Pomyœlnie zarejestrowano z danymi: \nE-mail: {RegEmail.Text}\n Login: {RegUsername.Text}\n Has³o: {RegPassword.Password}";
                        RegisterSuccess.Message = RegisterResult;
                        RegisterFail.IsOpen = false;
                        RegisterSuccess.IsOpen = true;

                        

                        

                    }

                    else
                    {
                        RegisterResult = "WprowadŸ poprawny E-mail";
                        RegisterFail.Message = RegisterResult;
                        RegisterSuccess.IsOpen = false;
                        RegisterFail.IsOpen = true;
                    }
                }
                else
                {
                    RegisterResult = "Has³a s¹ ró¿ne";
                    RegisterFail.Message = RegisterResult;
                    RegisterSuccess.IsOpen = false;
                    RegisterFail.IsOpen = true;
                }


            }
            
            else
            {
                RegisterResult = "Pola nie mog¹ byæ puste";
                RegisterFail.Message = RegisterResult;
                RegisterSuccess.IsOpen = false;
                RegisterFail.IsOpen = true;
            }
            


        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            if (LoginEmail.Text != "" || LoginPassword.Password != "")
            {
                if (LoginEmail.Text.Contains("@"))
                {

                    using var context = new ChatDbContext();
                    var updateLoginStaus = context.Users
                        .Where(x => x.EMail == LoginEmail.Text && x.Password == LoginPassword.Password)
                        .ToList();

                    if (updateLoginStaus.Any())
                    {
                        foreach (var user in updateLoginStaus)
                        {
                            user.IsLogedIn = true;
                        }

                        context.SaveChanges();

                        var LoggedUserName = context.Users
                            .FirstOrDefault(x => x.EMail == LoginEmail.Text);
                        LoginSuccess.Message = string.Format("Poprawnie zalogowano jako {0}", LoggedUserName.UserName);
                        LoginSuccess.IsOpen = true;
                        ShellPage.CurrentLoggedUserLabel.Content = LoggedUserName.UserName;
                        ShellPage.PrivateMessageNavigationLabel.IsEnabled = true;
                        ShellPage.GroupMessageNavigationLabel.IsEnabled = true;



                    }
                    else
                    {
                        LoginFail.Message = "Nie znaleziono u¿ytkownika o podanych danych. Spróbuj ponownie";
                        LoginSuccess.IsOpen = false;
                        LoginFail.IsOpen = true;
                    }

                }
                else
                {
                    LoginFail.Message = "WprowadŸ poprawny E-mail";
                    LoginSuccess.IsOpen = false;
                    LoginFail.IsOpen = true;
                }
            }
            else
            {
                LoginFail.Message = "Pola nie mog¹ byæ puste";
                LoginSuccess.IsOpen = false;
                LoginFail.IsOpen = true;
            }
        }
        public string LoggedUserNameVar;

    }
}
