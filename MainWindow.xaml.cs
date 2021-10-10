using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorkWithDatabase.WorkWithDB_Decorator;
namespace WpfSQLapp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string[] userData = new string[6];

        public MainWindow()
        {
            InitializeComponent();
        }


        private void xtxUserEntry2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (TextBoxPassword.Password.Length == 0)
            {
                WatermarkTextBlock.Visibility = Visibility.Visible;
            }
            else
                WatermarkTextBlock.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            
            string[] serverPort = TextBoxServer.Text.Split(':');
            string login = TextBoxLogin.Text;
            string password = TextBoxPassword.Password;

            // 0 - serverIp 1 - serverPort 2 - login 3 - password
            UserData userData = new UserData();
            userData.ip = serverPort[0];
            userData.port = serverPort[1];
            userData.login = login;
            userData.password = password;

            Request request = new NonQueryExecuter();
            MySqlCommand cmd =  request.Connect(userData);
            
            
            // подключение к дб
            
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                    WindowDatabaseSelection wds = new WindowDatabaseSelection(userData);
                    wds.Show();                    
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка подключения ! \nНеправильно введены данные.");
                }           
                        
        }
    }
    public struct UserData
    {
        public string ip;
        public string port;
        public string login;
        public string password;
        public string usableDatabase;
        public string usableTable;
    }
}
