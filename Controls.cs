using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWork
{
    internal interface Controls
    {

        public abstract void add();
        public abstract void delete(int employeeID);
        public abstract void update(int employeeID);
        public abstract void search(int employeeID);
        public abstract void display();
    }
}
