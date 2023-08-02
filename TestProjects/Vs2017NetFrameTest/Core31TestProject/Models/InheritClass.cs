using Core31TestProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core31TestProject.Models
{
    public class InheritClass : InheritInterfaces<Contact>
    {
        public string To { get; set; }
        public Contact Contact { get; set; }
    }

    public class Contact : IContactor
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }
}
