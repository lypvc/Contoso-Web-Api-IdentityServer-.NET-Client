using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcClientB.Entities
{
    public class Student
    {

        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }
        public string FullName
       {
           get
           {
               return LastName + ", " + FirstMidName;            }
       }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
