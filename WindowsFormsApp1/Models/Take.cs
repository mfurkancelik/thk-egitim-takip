using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    public class Take : BaseEntity
    {
        public Guid PersonId { get; set; }

        public virtual Person Person { get; set; }

        public Guid LessonId { get; set; }

        public virtual Lesson Lesson { get; set; }

        public DateTime TakeTime { get; set; }

        public DateTime FinishTime { get; set; }
              
        public string Time { get; set; }
    }
}
