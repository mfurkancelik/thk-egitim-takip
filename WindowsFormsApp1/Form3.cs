using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Services;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        private PersonService _personService;
        private LessonService _lessonService;
        private TakeService _takeService;
        void Listele()
        {
            dataGridView1.DataSource = _personService.GetAll().Select(x => new
            {
                Id = x.Id,
                Ad = x.FirstName,
                Soyad = x.LastName,
                Departman = x.Department
            }).ToList();

         

        }

        void Listele(string param)
        {
            dataGridView1.DataSource = _personService.GetAll(
              x => x.Id.ToString().Contains(param) ||
                   x.FirstName.Contains(param) ||
                   x.LastName.Contains(param) ||
                   x.Department.Contains(param)
                   )
                   .Select(x => new
                   {
                       Id = x.Id,
                       Ad = x.FirstName,
                       Soyad = x.LastName,
                       Departman = x.Department

                   }).ToList();

        }

        

        void DersListele2()
        {
            dataGridView3.DataSource = _lessonService.GetAll().Select(x => new
            {
                Id = x.Id,
                Ders = x.LessonName
            }).ToList();

        }

        void DersListele2(string param)
        {
            dataGridView3.DataSource = _lessonService.GetAll(
            x => x.Id.ToString().Contains(param) ||
                 x.LessonName.Contains(param)
                 
                 )
                 .Select(x => new
                 {
                     Id = x.Id,
                     Ders = x.LessonName
                 }).ToList();

        }
        void DersListele()
        {
            dataGridView2.DataSource = _takeService.GetAll().Select(x => new
            {
                Id = x.Id,
                Ad= x.Person.FirstName,
                Soyad = x.Person.LastName,
                Ders= x.Lesson.LessonName,
                Kalangün=x.Time

            }).ToList();
        }

        void DersListele(string param)
        {

            dataGridView2.DataSource = _takeService.GetAll(
      x => x.Person.FirstName.Contains(param) ||
           x.Person.LastName.Contains(param) ||
           x.Lesson.LessonName.Contains(param) ||
           x.Time.ToString().Contains(param) 

           )
           .Select(x => new
           {
               x.Id,
               Ad = x.Person.FirstName,
               Soyad = x.Person.LastName,
               Ders = x.Lesson.LessonName,
               Kalangün = x.Time
           }).ToList();

        }

        public Form3()
        {
            InitializeComponent();
            _personService = new PersonService();
            _lessonService = new LessonService();
            _takeService = new TakeService();
        }

        private void ekle3_Click(object sender, EventArgs e)
        {
            Take take = new Take();
            Guid personid = (Guid)dataGridView1.SelectedRows[0].Cells[0].Value;
            Guid lessonid = (Guid)dataGridView3.SelectedRows[0].Cells[0].Value;
            take.PersonId = personid;
            take.LessonId = lessonid;

            take.TakeTime = dateTimePicker1.Value;
            take.FinishTime = dateTimePicker2.Value;
            TimeSpan kalan =dateTimePicker2.Value - dateTimePicker1.Value;
            int kalangün = Convert.ToInt32(kalan.TotalDays);
            
            take.Time = Convert.ToString(kalangün);
            _takeService.Add(take);
            bool result = _takeService.Save() > 0;
            if (result)
            {
                dateTimePicker1.Value = DateTime.Now;
                dateTimePicker2.Value = DateTime.Now;
            }           
           
            try
            {
                DersListele();
            }
            catch (Exception)
            {
                MessageBox.Show("Eklenmiştir fakat Programı Açıp Kapattıktan sonra Gözükecektir.!!");
               

            }
            




        }

        private void sil3_Click(object sender, EventArgs e)
        {
            Guid id = (Guid)dataGridView2.SelectedRows[0].Cells[0].Value;
            _takeService.Delete(id);
            _takeService.Save();
            DersListele();
        }


        

        private void Form3_Load(object sender, EventArgs e)
        {
            Listele();
            DersListele();
            DersListele2();
        }

       

        private void txtdersarama_TextChanged(object sender, EventArgs e)
        {
            DersListele2(txtdersarama.Text);
        }

        private void txtpersonelarama_TextChanged(object sender, EventArgs e)
        {
            Listele(txtpersonelarama.Text);
        }

        private void txtalanarama_TextChanged(object sender, EventArgs e)
        {
            DersListele(txtalanarama.Text);
        }
    }
}
