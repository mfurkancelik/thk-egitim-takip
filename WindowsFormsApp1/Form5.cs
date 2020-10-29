using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Services;

namespace WindowsFormsApp1
{
    public partial class Form5 : Form
    {
        private PersonService _personService;
        private TakeService _takeService;

         void Listele()
        {
            dataGridView1.DataSource = _personService.GetAll().Select(x => new
            {
                x.Id,
                x.FirstName,
                x.LastName                
            }).ToList();
        }

        void Listele(string param)
        {
            dataGridView1.DataSource = _personService.GetAll(
               x => x.FirstName.Contains(param) ||
                    x.LastName.Contains(param) ||
                    x.Id.ToString().Contains(param)
                    
                    )
                    .Select(x => new
                    {
                        x.Id,
                        x.FirstName,
                        x.LastName,
                        
                    }).ToList();

        }
        void DersListele()
        {
            Guid id = (Guid)dataGridView1.SelectedRows[0].Cells[0].Value;

            dataGridView2.DataSource = _takeService.GetAll().Where(x=>x.PersonId==id).Select(x => new
            {
                x.Person.FirstName,
                x.Person.LastName,
                x.Lesson.LessonName               

            }).ToList();
        }

        public Form5()
        {
            InitializeComponent();
            _personService = new PersonService();
            _takeService = new TakeService();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void excelyazdir_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

            excel.Visible = true;
            object Missing = Type.Missing;
            Workbook workbook = excel.Workbooks.Add(Missing);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
            int baslamakolonu = 1;
            int baslamasatiri = 1;
            for (int j = 0; j < dataGridView2.Columns.Count; j++)
            {
                Range myRange = (Range)sheet1.Cells[baslamasatiri, baslamakolonu + j];
                myRange.Value2 = dataGridView2.Columns[j].HeaderText;
            }
            baslamasatiri++;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView2.Columns.Count; j++)
                {
                    Range myRange = (Range)sheet1.Cells[baslamasatiri + i, baslamasatiri + j-1];
                    myRange.Value2 = dataGridView2[j, i].Value == null ? "" : dataGridView2[j, i].Value;
                    myRange.Select();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DersListele();
        }

        private void txtkisiara_TextChanged(object sender, EventArgs e)
        {
            Listele(txtkisiara.Text);
        }

       
    }
}
