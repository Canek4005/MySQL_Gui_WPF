using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithDatabase.WorkWithDB_Decorator
{
    public class Create:CondimentDecorator
    {
        public Create(Request request, string usableObject):base((request.lineCommand.Length == 0 ? "" : request.lineCommand + ";") + " CREATE " + usableObject, request)
        {

        }
        public override List<string> Execute(MySqlCommand command, string lineCommand)
        {
            
            return request.Execute(command, lineCommand);
        }
    }
}
