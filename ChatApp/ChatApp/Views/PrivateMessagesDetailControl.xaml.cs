using ChatApp.Core.Models;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MySql.Data.MySqlClient;

namespace ChatApp.Views;

public sealed partial class PrivateMessagesDetailControl : UserControl
{
    public Users? ListDetailsMenuItem
    {
        get => GetValue(ListDetailsMenuItemProperty) as Users;
        set => SetValue(ListDetailsMenuItemProperty, value);
    }

    public static readonly DependencyProperty ListDetailsMenuItemProperty = DependencyProperty.Register("ListDetailsMenuItem", typeof(Users), typeof(PrivateMessagesDetailControl), new PropertyMetadata(null, OnListDetailsMenuItemPropertyChanged));

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
        
        InvertedListView.Items.Add(
            new PrivateMessage(messageContent, DateTime.Now, HorizontalAlignment.Right)
            );

        using var context = new ChatDbContext();
        
        var LoggedUserId = context.Users
                        .FirstOrDefault(x => x.IsLogedIn == true);
        var NewMessage = new Messages
        {
            SentDate = DateTime.Now,
            MessageAuthor = LoggedUserId.UserId,
            MessageDestination =ListDetailsMenuItem.UserId,
            MessageContent = messageContent,
        };
        context.Messages.Add(NewMessage);
        context.SaveChanges();
        MessageField.Text = String.Empty;

    }

    private void MessageReceived(object sender, RoutedEventArgs e)
    {

        InvertedListView.Items.Add(
            new PrivateMessage("Message ", DateTime.Now, HorizontalAlignment.Left)
            );
        var messageContent = MessageField.Text;

        using var context = new ChatDbContext();

        var LoggedUserId = context.Users
                        .FirstOrDefault(x => x.IsLogedIn == true);
        var NewMessage = new Messages
        {
            SentDate = DateTime.Now,
            MessageDestination = LoggedUserId.UserId,
            MessageAuthor= ListDetailsMenuItem.UserId,
            MessageContent = messageContent,
        };
        context.Messages.Add(NewMessage);
        context.SaveChanges();
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
