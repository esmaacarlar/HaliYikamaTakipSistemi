using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HaliYikamaSirketi
{
    public partial class Form1: Form
    {
        List<Musteri> musteriler = new List<Musteri>();
        public Form1()
        {
            InitializeComponent();
        }

        public class Musteri
        {
            public string AdSoyad { get; set; }
            public string Telefon { get; set; }
            public string Adres { get; set; }
            public List<Hali> Halilar { get; set; } = new List<Hali>();

            public override string ToString()
            {
                return $"{AdSoyad}";
            }
        }

        public class Hali
        {
            public int metrekare { get; set; }
            public DateTime AlişTarihi { get; set; }
            public DateTime TeslimTarihi { get; set; }
            public bool TeslimEdildiMi { get; set; }
            public int ucret => metrekare * 20;

            public override string ToString()
            {
                string durum = TeslimEdildiMi ? "Teslim edildi" : "Yıkamada";
                return $"Metrekare: {metrekare}, Ücret: {ucret} TL, Durum: {durum}";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Musteri musteri = new Musteri();
            musteri.AdSoyad = textBox1.Text;
            musteri.Telefon = textBox2.Text;
            musteri.Adres = textBox3.Text;
            musteriler.Add(musteri);
            comboBox1.Items.Add(musteri);
            MessageBox.Show("Müşteri eklendi.");
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is Musteri secilenMusteri)
            {
                Hali hali = new Hali()
                {
                    metrekare = Convert.ToInt32(textBox4.Text),
                    AlişTarihi = dateTimePicker1.Value,
                    TeslimTarihi = dateTimePicker2.Value,
                    TeslimEdildiMi = false
                };
                secilenMusteri.Halilar.Add(hali);
                ListeGuncelle();
                MessageBox.Show("Halı eklendi.");
                textBox4.Text = "";
            }
        }

        private void ListeGuncelle()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            foreach (var musteri in musteriler)
            {
                foreach (var hali in musteri.Halilar)
                {
                    string bilgi = $"{musteri} - {hali}";
                    if (hali.TeslimEdildiMi)
                        listBox2.Items.Add(bilgi);
                    else
                        listBox1.Items.Add(bilgi);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (var musteri in musteriler)
            {
                foreach (var hali in musteri.Halilar)
                {
                    string bilgi = $"{musteri} - {hali}";
                    if (listBox1.SelectedItem != null && bilgi == listBox1.SelectedItem.ToString())
                    {
                        hali.TeslimEdildiMi = true;
                        ListeGuncelle();
                        MessageBox.Show("Halı teslim edildi olarak güncellendi.");
                    }
                }
            }
        }
    }
}