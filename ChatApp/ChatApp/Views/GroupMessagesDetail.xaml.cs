// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ChatApp.DBModels;
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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ChatApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GroupMessagesDetail : Page
    {
        public GroupMessagesDetail()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var exampleParameter = e.Parameter as Group;
            //string exampleParameter = e.Parameter as string;
            if (exampleParameter != null)
            {
                var context = new ChatDbContext();
                var groupMembers = context.Groups
                 .Where(g => g.GroupId == exampleParameter.GroupId)
                 .SelectMany(g => g.Users)
                 .Select(u => u.UserName)
                 .ToList(); 

                string members = string.Join("\n", groupMembers);

                GroupName.Text = exampleParameter.GroupName;
                ToolTipService.SetToolTip(GroupName, members);
                MessageField.PlaceholderText = $"Napisz w {exampleParameter.GroupName}";
            }
        }
        private void AddItemToEnd(object sender, RoutedEventArgs e)
        {
            var messageContent = MessageField.Text;

            InvertedListView.Items.Add(
                new GroupMessage(messageContent, DateTime.Now, HorizontalAlignment.Right)
                );

            MessageField.Text = String.Empty;

        }

        private void MessageReceived(object sender, RoutedEventArgs e)
        {

            InvertedListView.Items.Add(
                new GroupMessage("Message ", DateTime.Now, HorizontalAlignment.Left)
                );
            MessageField.Text = String.Empty;
        }
    }
    public class GroupMessage
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
        public GroupMessage(string text, DateTime dateTime, HorizontalAlignment align)
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
