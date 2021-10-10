using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithDatabase.WorkWithDB_Decorator
{
    public class Use:CondimentDecorator
    {
        
        //string usableObject;
        public Use(Request request, string usableObject):base((request.lineCommand.Length == 0 ? "" : request.lineCommand + ";") + " USE " + usableObject, request)
        {
            
            //this.usableObject = usableObject;
            
        }
        
        public override List<string> Execute(MySqlCommand command,String lineCommand)
        {
            
            return request.Execute(command, lineCommand); 
        }
    }
}
