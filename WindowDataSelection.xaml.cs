using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;
using WorkWithDatabase.WorkWithDB_Decorator;

namespace WpfSQLapp
{
    /// <summary>
    /// Логика взаимодействия для WindowDataSelection.xaml
    /// </summary>
    
    public partial class WindowDataSelection : Window
    {
        UserData userData;

        //public DataFromTheTable dataFromTheTable = new DataFromTheTable();
        public WindowDataSelection(UserData uD)
        {
            userData = uD;
            InitializeComponent();
            TextPeaceNameOfTable.Text = userData.usableTable;
            
            loadDataFromMySql();
            
        }
        void loadDataFromMySql()
        {
           
            Request request = new NonQueryExecuter();
            request = new Use(request,userData.usableDatabase);
            request = new SelectAll(request, userData.usableTable);
            
            
            MySqlCommand cmd =request.Connect(userData);
            cmd.CommandText = request.lineCommand;
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            cmd.Connection.Close();
            MainDataGrid.DataContext = dt;
            
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            WindowTablesSelection wts = new WindowTablesSelection(userData);
            wts.Show();
            this.Close();
        }

        private void ButtonAddData_Click(object sender, RoutedEventArgs e)
        {
            WindowAddingDataToTheTable wadttt = new WindowAddingDataToTheTable(userData,this);
            wadttt.Show();

        }
    }
    public class DataFromTheTable
    {
        public string NameOftable
        {
            get
            {
                return nameOfTable;
            }
            set
            {
                nameOfTable = value;
            }
        }
        private string nameOfTable;
    }
}
