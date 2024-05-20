using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab1
{
    internal class TableUnit
    {
        public int Id { get;  private set; }
        public string Name { get; private set; }
        public string Description { get; set; }

        public TableUnit(int id, string name, string desciption)
        {
            if (id > 0) { Id = id; } else { throw new Exception("Id can not be negative or 0"); }
            if (name != null) { Name = name; } else { throw new Exception("Name can not be NULL"); }
            if (desciption != null) { Description = desciption; } else { throw new Exception("Description can not be NULL"); }

        }
    }
}
