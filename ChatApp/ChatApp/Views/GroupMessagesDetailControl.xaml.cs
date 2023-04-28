using ChatApp.Core.Models;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using WinUIEx.Messaging;

namespace ChatApp.Views;

public sealed partial class GroupMessagesDetailControl : UserControl
{
    public Users? ListDetailsMenuItem
    {
        get => GetValue(ListDetailsMenuItemProperty) as Users;
        set => SetValue(ListDetailsMenuItemProperty, value);
    }

    public static readonly DependencyProperty ListDetailsMenuItemProperty = DependencyProperty.Register("ListDetailsMenuItem", typeof(Users), typeof(GroupMessagesDetailControl), new PropertyMetadata(null, OnListDetailsMenuItemPropertyChanged));

    public GroupMessagesDetailControl()
    {
        InitializeComponent();
    }
    private void AddItemToEnd(object sender, RoutedEventArgs e)
    {
        var messageContent = GroupMessageField.Text;
        GroupMessageField.Text = String.Empty;
        InvertedListView.Items.Add(
            new GroupMessage(messageContent, DateTime.Now, HorizontalAlignment.Right)
            );
    }

    private void MessageReceived(object sender, RoutedEventArgs e)
    {

        InvertedListView.Items.Add(
            new GroupMessage("Message ", DateTime.Now, HorizontalAlignment.Left)
            );
    }
    private static void OnListDetailsMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is GroupMessagesDetailControl control)
        {
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
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
