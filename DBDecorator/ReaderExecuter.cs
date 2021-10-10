using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithDatabase.WorkWithDB_Decorator
{
    class ReaderExecuter:Request
    {
        public ReaderExecuter() : base("")
        {

        }
        public override List<string> Execute(MySqlCommand command, string lineCommand)
        {
            Console.WriteLine("Применяю команду: " + lineCommand);
            command.CommandText = lineCommand;

            List<string> listTables = new List<string>();
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                               
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int count = reader.FieldCount;
                        for (int i = 0; i < count; i++)
                        {
                            //Console.Write(reader.GetType() != null ? reader.GetString(i) : "" + " ") ;
                            if (!reader.IsDBNull(i))
                                listTables.Add(reader.GetString(i));
                            
                        }

                        Console.WriteLine();
                    }
                }

            }
            command.Connection.Close();
            command.Connection.Dispose();

            //listTables.Sort();
            return listTables;          

            
            
        }
    }
}
