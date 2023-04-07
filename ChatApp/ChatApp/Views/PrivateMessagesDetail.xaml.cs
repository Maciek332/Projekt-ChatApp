// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

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
using Microsoft.VisualBasic;
using Windows.System;
using ChatApp.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ChatApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PrivateMessagesDetail : Page
    {
        public PrivateMessagesDetail()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string exampleParameter = e.Parameter as string;
            if (exampleParameter != null)
            {
                UserName.Text = exampleParameter;
                MessageField.PlaceholderText = $"Napisz do {exampleParameter}";
            }
        }
        private void AddItemToEnd(object sender, RoutedEventArgs e)
        {
            var messageContent = MessageField.Text;

            InvertedListView.Items.Add(
                new PrivateMessage(messageContent, DateTime.Now, HorizontalAlignment.Right)
                );

            MessageField.Text = String.Empty;

        }

        private void MessageReceived(object sender, RoutedEventArgs e)
        {

            InvertedListView.Items.Add(
                new PrivateMessage("Message ", DateTime.Now, HorizontalAlignment.Left)
                );
            MessageField.Text = String.Empty;
        }
    }
    public class PrivateMessage
    {
        public string MsgText
        {
            get; private set;
        }
        public DateTime MsgDateTime
        {
            get; private set;
        }
        public HorizontalAlignment MsgAlignment
        {
            get; set;
        }
        public PrivateMessage(string text, DateTime dateTime, HorizontalAlignment align)
        {
            MsgText = text;
            MsgDateTime = dateTime;
            MsgAlignment = align;
        }

        public override string ToString()
        {
            return MsgDateTime.ToString() + " " + MsgText;
        }
    }
}
