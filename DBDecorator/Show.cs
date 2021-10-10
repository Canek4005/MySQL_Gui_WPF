using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithDatabase.WorkWithDB_Decorator
{
    public class Show:CondimentDecorator
    {
        public Show(Request request, String usableObject) : base((request.lineCommand.Length==0 ? "":request.lineCommand+";")+" SHOW " + usableObject, request)
        {

        }
        public override List<string> Execute(MySqlCommand command, string lineCommand)
        {
            
            return request.Execute(command, lineCommand); ;
        }
    }
}
