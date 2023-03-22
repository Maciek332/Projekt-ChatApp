using ChatApp.Core.Models;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace ChatApp.Views;

public sealed partial class PrivateMessagesDetailControl : UserControl
{
    public User? ListDetailsMenuItem
    {
        get => GetValue(ListDetailsMenuItemProperty) as User;
        set => SetValue(ListDetailsMenuItemProperty, value);
    }

    public static readonly DependencyProperty ListDetailsMenuItemProperty = DependencyProperty.Register("ListDetailsMenuItem", typeof(User), typeof(PrivateMessagesDetailControl), new PropertyMetadata(null, OnListDetailsMenuItemPropertyChanged));

    public PrivateMessagesDetailControl()
    {
        InitializeComponent();
    }

    private static void OnListDetailsMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is PrivateMessagesDetailControl control)
        {
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }
    private void AddItemToEnd(object sender, RoutedEventArgs e)
    {
        var messageContent = MessageField.Text;
        MessageField.Text = String.Empty;
        InvertedListView.Items.Add(
            new PrivateMessage(messageContent, DateTime.Now, HorizontalAlignment.Right)
            );
    }

    private void MessageReceived(object sender, RoutedEventArgs e)
    {

        InvertedListView.Items.Add(
            new PrivateMessage("Message ", DateTime.Now, HorizontalAlignment.Left)
            );
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
