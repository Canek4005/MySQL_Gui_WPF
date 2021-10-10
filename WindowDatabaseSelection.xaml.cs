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
    /// Логика взаимодействия для WindowDatabaseSelection.xaml
    /// </summary>
    public partial class WindowDatabaseSelection : Window
    {
        UserData userData;
        public WindowDatabaseSelection(UserData uD)
        {
            userData = uD;
            InitializeComponent();
            refreshDatabases();
            
            
        }
        
        public void refreshDatabases()
        {
            Request request = new ReaderExecuter();
            request = new Show(request, "DATABASES");


            List<string> listDatabases = request.Execute(request.Connect(userData), request.lineCommand);
            listDatabases.Sort();
            int i = 1;
            foreach(string table in listDatabases)
            {

                ListBoxItem item = new ListBoxItem();
                item.Content = i.ToString()+") "+table;
                ListBoxTables.Items.Add(item);
                i++;
            }
        }
        void deleteDatabase(string nameTable)
        {
            Request request = new NonQueryExecuter();
            request = new Drop(request, "DATABASE "+nameTable);

            request.Execute(request.Connect(userData), request.lineCommand);
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WindowCreateDatabase wcd = new WindowCreateDatabase(userData,this);
            wcd.Show();
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (ListBoxTables.SelectedItems.Count != 0)
            {
                string[] database = ListBoxTables.SelectedItem.ToString().Split();
                userData.usableDatabase = database.Last();
                WindowTablesSelection wwwd = new WindowTablesSelection(userData);
                wwwd.Show();
                this.Close();
            }
        }

        private void ButtonDeleteDatabase_Click_1(object sender, RoutedEventArgs e)
        {
            if (ListBoxTables.SelectedItem != null)
            {
                deleteDatabase(ListBoxTables.SelectedItem.ToString().Split(' ')[2]);
                ListBoxTables.Items.Clear();
                refreshDatabases();
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            
        }

        
    }
}
