using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportOfDayWork
{
    public class User
    {
        public string FullName { get; }
        public uint Id { get; }
        public uint BindingId { get; }

        public User(string fullName, uint id, uint bindingId)
        {
            FullName = fullName;
            Id = id;
            BindingId = bindingId;
        }
    }
}
