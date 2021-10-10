using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithDatabase.WorkWithDB_Decorator
{
    public abstract class CondimentDecorator:Request
    {
        protected Request request;
        public CondimentDecorator(string lineCommand, Request request):base(lineCommand)
        {
            this.request = request;
        }
        
    }
}
