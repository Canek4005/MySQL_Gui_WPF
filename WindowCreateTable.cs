using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WorkWithDatabase.WorkWithDB_Decorator;

namespace WpfSQLapp
{
    
    public partial class WindowCreateTable : Window
    {
        UserData userData;
        WindowTablesSelection WindowTablesSelection;
        
        public ObservableCollection<TemplateItem> templateItems = new ObservableCollection<TemplateItem>();
        public WindowCreateTable(UserData uD , WindowTablesSelection WTS)
        {
            userData = uD;
            WindowTablesSelection = WTS;

            InitializeComponent();
            
            // Создание первого шаблона
            templateItems.Add(new TemplateItem() { });
            // Связь TabControl с источником данных
            TabControlParams.ItemsSource = templateItems;
            
            
                        
                        
        }
        // Добавления вкладки в TabControl
        private void ButtonAddChildren_Click(object sender, RoutedEventArgs e)
        {
            templateItems.Add(new TemplateItem() { });
            TabControlParams.ItemsSource = templateItems;
            
        }
        // Удаление вкладки в Tab Control
        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            if (templateItems.Count>1)
            {
                templateItems.RemoveAt(templateItems.Count - 1);

            }

        }
        // Создание таблицы на основе полученных данных
        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            string content = "(";
            for(int i =0;i< templateItems.Count() ; i++)
            {
                string NameOfCollumn = templateItems[i].TextBox_Name+" ";
                string TypeOfColumn = templateItems[i].TextboxTypeField + " ";
                string FirstAttributeOfCollumn = templateItems[i].TextboxAttribute1Field + " ";
                string SecondAttributeOfCollumn = templateItems[i].TextboxAttribute2Field;
                
                content += NameOfCollumn+TypeOfColumn+FirstAttributeOfCollumn+SecondAttributeOfCollumn + ",\n";
            }
            content = content.Substring(0, content.Length - 2);
            content += ");";

            Request request = new NonQueryExecuter();
            request = new Use(request, userData.usableDatabase);
            request = new Create(request, "TABLE " + TextBoxNameOfTable.Text + " " + content);
            request.Execute(request.Connect(userData), request.lineCommand);
            MessageBox.Show(request.lineCommand);
            WindowTablesSelection.ListBoxTables.Items.Clear();
            WindowTablesSelection.refreshTables();

            this.Close();
            //MessageBox.Show(request.lineCommand);

             
        }

    }



    public class TemplateItem:INotifyPropertyChanged
    {
        
        private string textBox_Name;
        string[] textboxType = new string[] {"INT", "FLOAT", "DOUBLE", "DECIMAL", "CHAR(*)", "VARCHAR(*)", "BOOL" };
        string[] textboxAttribute1 = new string[] { "PRIMARY KEY", "NULL", "NOT NULL", "DEFAULT *", "CHECK (*)" , "AUTO_INCREMENT","UNIQUE" };
        string[] textboxAttribute2 = new string[] { "PRIMARY KEY", "NULL", "NOT NULL", "DEFAULT *", "CHECK (*)", "AUTO_INCREMENT", "UNIQUE" };
        string textboxTypeField;
        string textboxAttribute1Field;
        string textboxAttribute2Field;

        public TemplateItem()
        {
            this.Items = new ObservableCollection<TemplateItem>();
            
        }
        public string TextBox_Name
        {
            get { return textBox_Name; }
            set { textBox_Name = value;
                onPropertyChanged("TextBox_Name");
            }
        }              
        public string[] TextboxType
        {
            get
            {
                return textboxType;
            }
            set { }                     
                          
        }
        public string[] TextboxAttribute1
        {
            get
            {
                return textboxAttribute1;
            }
            set { }
        }
        public string[] TextboxAttribute2
        {
            get
            {
                return textboxAttribute2;
            }
            set { }
        }

        public string TextboxTypeField
        {
            get { return textboxTypeField; }
            set 
            { 
                textboxTypeField = value;
                onPropertyChanged("TextboxTypeField");
            }
        }
        public string TextboxAttribute1Field
        {
            get { return textboxAttribute1Field; }
            set 
            { 
                textboxAttribute1Field = value;
                onPropertyChanged("TextboxAttribute1Field");
            }
        }
        public string TextboxAttribute2Field
        {
            get { return textboxAttribute2Field; }
            set 
            { 
                textboxAttribute2Field = value;
                onPropertyChanged("TextboxAttribute2Field");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void onPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        

        

        public ObservableCollection<TemplateItem> Items { get; set; }
    }
}
