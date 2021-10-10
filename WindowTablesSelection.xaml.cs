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
using System.Windows.Shapes;
using WorkWithDatabase;
using WorkWithDatabase.WorkWithDB_Decorator;

namespace WpfSQLapp
{
    /// <summary>
    /// Логика взаимодействия для WindowTablesSelection.xaml
    /// </summary>
    public partial class WindowTablesSelection : Window
    {
        UserData userData;
        public WindowTablesSelection(UserData uD)
        {
            userData = uD;
            InitializeComponent();
            refreshTables();
        }
        public void refreshTables()
        {
            Request request = new ReaderExecuter();
            request = new Use(request, userData.usableDatabase);
            request = new Show(request, "TABLES");
            

            List<string> listTables = request.Execute(request.Connect(userData), request.lineCommand);
            listTables.Sort();
            int i = 1;
            foreach (string table in listTables)
            {

                ListBoxItem item = new ListBoxItem();
                item.Content = i.ToString() + ") " + table;
                ListBoxTables.Items.Add(item);
                i++;
            }
        }
        void deleteTable(string remobableTable)
        {

            Request request = new NonQueryExecuter();
            request = new Use(request, userData.usableDatabase);
            request = new Drop(request, "TABLE " + remobableTable);
            request.Execute(request.Connect(userData), request.lineCommand);
            
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            WindowDatabaseSelection wds = new WindowDatabaseSelection(userData);
            wds.Show();
            this.Close();
        }

        private void ButtonOpenTable_Click(object sender, RoutedEventArgs e)
        {
            userData.usableTable = ListBoxTables.SelectedItem.ToString().Split().Last();
            WindowDataSelection wds = new WindowDataSelection(userData);
            wds.Show();
            this.Close();
        }

        private void ButtonCreateTable_Click(object sender, RoutedEventArgs e)
        {
            WindowCreateTable wct = new WindowCreateTable(userData,this);
            wct.Show();
        }

        private void ButtonDeleteTable_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxTables.SelectedItem != null)
            {
                deleteTable(ListBoxTables.SelectedItem.ToString().Split(' ')[2]);
                ListBoxTables.Items.Clear();
                refreshTables();
            }
            

        }
    }
}
