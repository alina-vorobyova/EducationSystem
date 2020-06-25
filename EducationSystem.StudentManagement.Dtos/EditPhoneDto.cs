using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.StudentManagement.Dtos
{
    public class EditPhoneDto 
    {
        public string OldNumber { get; set; }
        public string NewNumber { get; set; }
        public string OldType { get; set; }
        public string NewType { get; set; }
    }
}
