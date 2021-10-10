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
    /// Логика взаимодействия для WindowCreateDatabase.xaml
    /// </summary>
    public partial class WindowCreateDatabase : Window
    {
        UserData userData;
        WindowDatabaseSelection windowDatabaseSelection;
        public WindowCreateDatabase(UserData uD,WindowDatabaseSelection WDS)
        {
            InitializeComponent();
            userData = uD;
            windowDatabaseSelection = WDS;
        }

        private void ButtonAddDatabase_Click(object sender, RoutedEventArgs e)
        {
            Request request = new NonQueryExecuter();
            request = new Create(request, "DATABASE " + TextBoxDatabase.Text);
            request.Execute(request.Connect(userData), request.lineCommand);
            TextBoxDatabase.Clear();
            windowDatabaseSelection.ListBoxTables.Items.Clear();
            windowDatabaseSelection.refreshDatabases();
        }
    }
}
