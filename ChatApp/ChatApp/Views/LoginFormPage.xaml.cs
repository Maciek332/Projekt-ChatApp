using ChatApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Controls;
using CommunityToolkit.Common;

namespace ChatApp.Views;

public sealed partial class LoginFormPage : Page
{
    public LoginFormViewModel ViewModel
    {
        get;
    }

    public LoginFormPage()
    {
        ViewModel = App.GetService<LoginFormViewModel>();
        InitializeComponent();
    }

    private void RegisterClick(object sender, RoutedEventArgs e)
    {
        string RegisterResult;
        if ( this.RegEmail.Text !=""  || this.RegPassword.Password != "" || this.RegUsername.Text != "")
        {
            if (this.RegEmail.Text.IsEmail())
            {
                RegisterResult = "Pomyślnie zarejestrowano z danymi:" + "\nE-mail: " + this.RegEmail.Text + "\n Login: " + this.RegUsername.Text + "\n Hasło: " + this.RegPassword.Password;
                this.RegisterSuccess.Message = RegisterResult;
                this.RegisterFali.IsOpen = false;
                this.RegisterSuccess.IsOpen = true;


            }
            else
            {
                RegisterResult = "Wprowadź poprawy E-mail";
                this.RegisterFali.Message = RegisterResult;
                this.RegisterSuccess.IsOpen = false;
                this.RegisterFali.IsOpen = true;
            }
            
        }
        else
        {
            RegisterResult = "Pola nie mogą być puste";
            this.RegisterFali.Message = RegisterResult;
            this.RegisterSuccess.IsOpen = false;
            this.RegisterFali.IsOpen = true;
        }


    }
}
