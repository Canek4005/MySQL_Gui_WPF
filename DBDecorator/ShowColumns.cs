using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithDatabase.WorkWithDB_Decorator
{
    public class ShowColumns : CondimentDecorator
    {
        public ShowColumns(Request request, String usableObject) : base((request.lineCommand.Length == 0 ? "" : request.lineCommand + ";") + " SHOW COLUMNS FROM " + usableObject, request)
        {

        }
        public override List<string> Execute(MySqlCommand command, string lineCommand)
        {

            return request.Execute(command, lineCommand); ;
        }
    }
}
