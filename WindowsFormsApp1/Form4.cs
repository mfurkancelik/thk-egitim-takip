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
    public partial class Form4 : Form
    {
        private LessonService _lessonService;
        public Form4()
        {
            InitializeComponent();
            _lessonService = new LessonService();
        }

        void Listele()
        {
            dataGridView1.DataSource = _lessonService.GetAll().Select(x => new
            {
               x.Id,
               x.LessonName,
               x.LessonNo
            }).ToList();

        }

        void Listele(string param)
        {
            dataGridView1.DataSource = _lessonService.GetAll(
               x => x.LessonName.Contains(param) ||
                    x.LessonNo.Contains(param) ||
                    x.Id.ToString().Contains(param)
                    )
                    .Select(x => new
                    {
                        x.Id,
                        x.LessonName,
                        x.LessonNo
                        
                    }).ToList();

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAd.Text.Trim()) && string.IsNullOrEmpty(txtNo.Text.Trim()))
            {
                MessageBox.Show("Boş Geçilemez.Lütfen Tüm İstenilenleri Doldurunuz...","Uyarı",MessageBoxButtons.OK);
                txtAd.Text = txtNo.Text = "";
            }
            else
            {
                Lesson lesson = new Lesson();
                lesson.LessonName = txtAd.Text;
                lesson.LessonNo = txtNo.Text;

                _lessonService.Add(lesson);
                bool result = _lessonService.Save() > 0;
                if (result)
                    txtAd.Text = txtNo.Text = "";
                Listele();
                MessageBox.Show(result ? "Kayıt Eklendi" : "İşlem Hatası");
            }
           
        }

        private void tsmSil_Click(object sender, EventArgs e)
        {
            Guid id = (Guid)dataGridView1.SelectedRows[0].Cells[0].Value;
            _lessonService.Delete(id);
            _lessonService.Save();
            Listele();
            
        }
        private Lesson _updated;
        private void tsmDuzenle_Click(object sender, EventArgs e)
        {
            Guid id = (Guid)dataGridView1.SelectedRows[0].Cells[0].Value;
            _updated =_lessonService.GetById(id);

            txtAd.Text = _updated.LessonName;
            txtNo.Text = _updated.LessonNo;
            
        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            _updated.LessonName = txtAd.Text;
            _updated.LessonNo = txtNo.Text;

            _lessonService.Update(_updated);
            _lessonService.Save();
            Listele();
            txtAd.Text = txtNo.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Listele(textBox1.Text);
        }
    }
}
