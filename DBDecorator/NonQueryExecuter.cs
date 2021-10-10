using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WorkWithDatabase.WorkWithDB_Decorator
{
    class NonQueryExecuter:Request
    {
        
        public NonQueryExecuter():base("")
        {
            
            
            
        }
        
        public override List<string> Execute(MySqlCommand command,String lineCommand)
        {
            Console.WriteLine("Применяю команду: " + lineCommand);
            command.CommandText = lineCommand;
            try
            {
                int count = command.ExecuteNonQuery();
                Console.WriteLine($"Выполненно, поврежденных строк: {0}", count);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine(ex.ToString());
                
            }
            finally
            {
                
            }
            command.Connection.Close();
            command.Connection.Dispose();

            return null;
        }

    }
}
