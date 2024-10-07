using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnerClass
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        Leave leave = new Leave();

        public Employee()
        {
            Console.WriteLine("Creating Object");
            Id = 1;
            Name = "Johnny";
            leave.SickLeave = 10;
            leave.CasualLeave = 20;
        }

        public bool AvailStickLeave(int day)
        {
            if (leave.SickLeave >= day) {
                return true;
            }
            return false;
        }
        class Leave
        {
            public int SickLeave {  get; set; }
            public int CasualLeave { get; set; }   
        }
    }
}
