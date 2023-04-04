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
            if (this.RegEmail.Text != "" || this.RegPassword.Password != "" || this.RegUsername.Text != "")
            {
                if (this.RegEmail.Text.Contains("@"))
                {

                    using var context = new ChatDbContext();
                    var RegisterUser = new Users
                    {
                        EMail = this.RegEmail.Text,
                        UserName = this.RegUsername.Text,
                        Password = this.RegPassword.Password,
                        IsLogedIn = false,
                        RegisterDate = DateTime.Now
                    };
                    context.Users.Add(RegisterUser);
                    context.SaveChanges();

                    RegisterResult = "Pomyœlnie zarejestrowano z danymi:" + "\nE-mail: " + this.RegEmail.Text + "\n Login: " + this.RegUsername.Text + "\n Has³o: " + this.RegPassword.Password;
                    this.RegisterSuccess.Message = RegisterResult;
                    this.RegisterFail.IsOpen = false;
                    this.RegisterSuccess.IsOpen = true;

                }
                else
                {
                    RegisterResult = "WprowadŸ poprawy E-mail";
                    this.RegisterFail.Message = RegisterResult;
                    this.RegisterSuccess.IsOpen = false;
                    this.RegisterFail.IsOpen = true;
                }

            }
            else
            {
                RegisterResult = "Pola nie mog¹ byæ puste";
                this.RegisterFail.Message = RegisterResult;
                this.RegisterSuccess.IsOpen = false;
                this.RegisterFail.IsOpen = true;
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
                        LoggedUserNameVar = LoggedUserName.UserName;
                        

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
                    this.LoginFail.Message = "WprowadŸ poprawy E-mail";
                    this.LoginSuccess.IsOpen = false;
                    this.LoginFail.IsOpen = true;
                }
            }
            else
            {
                this.LoginFail.Message = "Pola nie mog¹ byæ puste";
                this.LoginSuccess.IsOpen = false;
                this.LoginFail.IsOpen = true;
            }
        }
        public string LoggedUserNameVar;

    }
}
