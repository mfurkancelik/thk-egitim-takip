using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    public class Lesson : BaseEntity
    {
        public string LessonName { get; set; }
        public string LessonNo { get; set; }

        public virtual ICollection<Take> Takes { get; set; }
    }
}
