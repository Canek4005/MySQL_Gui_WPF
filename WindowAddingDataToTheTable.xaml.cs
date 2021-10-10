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
using WorkWithDatabase.WorkWithDB_Decorator;

namespace WpfSQLapp
{
    /// <summary>
    /// Логика взаимодействия для WindowAddingDataToTheTable.xaml
    /// </summary>
    
    public partial class WindowAddingDataToTheTable : Window
    {
        UserData userData;
        WindowDataSelection windowDataSelection;
        List<DataContext> dataContexts = new List<DataContext>();
        public WindowAddingDataToTheTable(UserData ud,WindowDataSelection wds)
        {
            windowDataSelection = wds;
            userData = ud;
            InitializeComponent();
            FormingCollumnsOfTable();
        }
        void FormingCollumnsOfTable()
        {
            
            
            Request request = new ReaderExecuter();
            request = new Use(request, userData.usableDatabase);
            request = new ShowColumns(request,userData.usableTable);
            List<string> collumns= request.Execute(request.Connect(userData),request.lineCommand);
            
            
            int last = 0;
            for (int i = 0; i < collumns.Count; i++)
            {
                if(collumns[i]=="")
                {
                    DataContext data;
                    int switcher = i - last-1;
                    switch (switcher){
                        case 3:
                            data = new DataContext() {Name = collumns[last], Type = collumns[last + 1], Attribute1 = collumns[last + 2], Attribute2 = collumns[last + 3] };
                            last = i+1;
                            dataContexts.Add(data);
                            break;
                        case 2:
                            data = new DataContext() {Name = collumns[last], Type = collumns[last + 1], Attribute1 = collumns[last + 2], Attribute2 = "" };
                            last = i+1;
                            dataContexts.Add(data);
                            break;
                        case 1:
                            data = new DataContext() {Name = collumns[last], Type = collumns[last + 1], Attribute1 = " ", Attribute2 = " " };
                            last = i+1;
                            dataContexts.Add(data);
                            break;
                        
                    }
                    
                }            
                

                
            }
            dataGrid.ItemsSource = dataContexts;
        }
        public struct DataContext
        {
            private string name;
            private string type;
            private string attribute1;
            private string attribute2;
            private string val;
            public string Name
            {
                get
                {
                    return name;
                }
                set
                {
                    name = value;
                }
            }
            public string Type
            {
                get
                {
                    return type; 
                }
                set
                {
                    type = value;
                }
            }
            public string Attribute1
            {
                get
                {
                    return attribute1;
                }
                set
                {
                    attribute1 = value;
                }
            }
            public string Attribute2
            {
                get
                {
                    return attribute2;
                }
                set
                {
                    attribute2 = value;
                }
            }
            public string YourValue
            {
                get
                {
                    return val;
                }
                set
                {
                    val = value;
                }
            }


        }

    }
}
