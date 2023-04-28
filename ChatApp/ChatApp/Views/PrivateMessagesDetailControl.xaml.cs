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
        var connstr1 = "Server=localhost;Database=ChatDB;Uid=root;Pwd=;";
        var Query = "INSERT INTO chatdb.messages(MessageAuthor,MessageContent,MessageDestination,SentDate) values('" + 1 + "','" + this.MessageField.Text + "','" + ListDetailsMenuItem.UserId + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "');";
        MySqlConnection SendPrivMessageCon = new MySqlConnection(connstr1);
        MessageField.Text = String.Empty;
        MySqlCommand InsertPrivMessageSend = new MySqlCommand(Query, SendPrivMessageCon);
        MySqlDataReader MyReader2;
        SendPrivMessageCon.Open();
        MyReader2 = InsertPrivMessageSend.ExecuteReader();
        SendPrivMessageCon.Close();
    }

    private void MessageReceived(object sender, RoutedEventArgs e)
    {

        InvertedListView.Items.Add(
            new PrivateMessage("Message ", DateTime.Now, HorizontalAlignment.Left)
            );
        var connstr1 = "datasource=localhost;port=3306;username=root;password=";
        var Query = "INSERT INTO chatdb.messages(MessageAuthor,MessageContent,MessageDestination,SentDate) values('" +  + ListDetailsMenuItem.UserId + "','" + this.MessageField.Text + "','" + 1 + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "');";
        MySqlConnection SendPrivMessageCon = new MySqlConnection(connstr1);
        MessageField.Text = String.Empty;
        MySqlCommand InsertPrivMessageSend = new MySqlCommand(Query, SendPrivMessageCon);
        MySqlDataReader MyReader2;
        SendPrivMessageCon.Open();
        MyReader2 = InsertPrivMessageSend.ExecuteReader();
        SendPrivMessageCon.Close();
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
