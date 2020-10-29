using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace WindowsFormsApp1
{
    using Models;
    using Services;
    public partial class Form2 : Form
    {
        private PersonService _personService;

        public Form2()
        {
            InitializeComponent();
            _personService = new PersonService();
        }
        #region Listeleme Kodları
        void Listele()
        {
            dataGridView1.DataSource = _personService.GetAll().Select(x => new
            {
                x.Id,
                x.FirstName,
                x.LastName,
                x.Department
            }).ToList();

        }

        void Listele(string param)
        {
            dataGridView1.DataSource = _personService.GetAll(
                x => x.FirstName.Contains(param) ||
                     x.LastName.Contains(param) ||
                     x.Department.Contains(param)
                     )
                     .Select(x => new
                     {
                         x.Id,
                         x.FirstName,
                         x.LastName,
                         x.Department
                     }).ToList();                

                
        }
        #endregion
        

        private void Form2_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAd.Text.Trim()) && string.IsNullOrEmpty(txtSoyad.Text.Trim()) && string.IsNullOrEmpty(txtDepartman.Text.Trim()))
            {
                MessageBox.Show("Boş Geçilemez.Lütfen Tüm İstenilenleri Doldurunuz...", "Uyarı", MessageBoxButtons.OK);
                txtAd.Text = txtSoyad.Text = txtDepartman.Text = "";
            }
            else
            {
                Person person = new Person();
                person.FirstName = txtAd.Text;
                person.LastName = txtSoyad.Text;
                person.Department = txtDepartman.Text;

                _personService.Add(person);
                bool result = _personService.Save() > 0;
                if (result)
                    txtAd.Text = txtSoyad.Text = txtDepartman.Text = "";
                Listele();
                MessageBox.Show(result ? "Kayıt Eklendi" : "İşlem Hatası");
            }
           
            

        }

        private void txtArama_TextChanged(object sender, EventArgs e)
        {
            Listele(txtArama.Text);
        }

        private void tsmSil_Click(object sender, EventArgs e)
        {
            Guid id = (Guid)dataGridView1.SelectedRows[0].Cells[0].Value;
            _personService.Delete(id);
            _personService.Save();
            Listele();
        }
        private Person _updated;
        private void tsmDuzenle_Click(object sender, EventArgs e)
        {
            Guid id = (Guid)dataGridView1.SelectedRows[0].Cells[0].Value;
            _updated = _personService.GetById(id);

            txtAd.Text = _updated.FirstName;
            txtSoyad.Text = _updated.LastName;
            txtDepartman.Text = _updated.Department;
        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            _updated.FirstName = txtAd.Text;
            _updated.LastName = txtSoyad.Text;
            _updated.Department = txtDepartman.Text;

            _personService.Update(_updated);
            _personService.Save();
            Listele();
            txtAd.Text = txtSoyad.Text = txtDepartman.Text = "";
        }
    }
}
