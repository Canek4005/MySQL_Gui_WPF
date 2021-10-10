using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfSQLapp;

namespace WorkWithDatabase.WorkWithDB_Decorator
{
    public abstract class Request
    {
        public string lineCommand ;
        //protected MySqlConnection connection;
        UserData userData;
        
        public Request(string lineCommand)
        {
            this.lineCommand = lineCommand;
        }
        public MySqlCommand Connect(UserData userData)
        {
            this.userData = userData;
            MySqlCommand cmd = new MySqlCommand();
            
            String connString = "Server=" + userData.ip
                + ";port=" + int.Parse(userData.port) + ";User Id=" + userData.login + ";password=" + userData.password;
            Console.WriteLine("Формирую данныe на подключение...");
            MySqlConnection connection = new MySqlConnection(connString);
            try
            {
            connection.Open();
            cmd.Connection = connection;

            }catch(Exception ex)
            {
                
                MessageBox.Show("Ошибка подключения ! \nНеправильно введены данные. \n" + ex.Message);
            }
            return cmd;
            //cmd.CommandText = commandText;
        }
        public abstract List<string> Execute(MySqlCommand command,String lineCommand);
        
        
        


    }
}
