using Entity_framework.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Entity_framework
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        #region Panel Islemleri
        private void BtnMenu_Click(object sender, EventArgs e)
        {
            if (PanelMenu.Height == 160)
            {
                TimerPanelClose.Start();
            }
            else
            {

                TimerPanelOpen.Start();
            }
        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void TimerPanelClose_Tick(object sender, EventArgs e)
        {
            PanelMenu.Height = PanelMenu.Height - 10;
            if (PanelMenu.Height == 80)
            {
                TimerPanelClose.Stop();
            }

            
        }

        private void TimerPanelOpen_Tick(object sender, EventArgs e)
        {
            PanelMenu.Height = PanelMenu.Height + 10;
            if (PanelMenu.Height == 160)
            {
                TimerPanelOpen.Stop();
            }
        }
        #endregion

        #region Listeleme
        private void BtnOgrListele_Click(object sender, EventArgs e)
        {
            
            DbSinavOgrenciEntities DbOgrenciListe = new DbSinavOgrenciEntities();//Entity Framework tarafından oluşturulmuş ve veritabanıyla etkileşime geçmek için kullanılan bir DbContext sınıfıdır. 
            dataGridView1.DataSource = DbOgrenciListe.Ogrenci.ToList();//Ogrenci tablosundan verileri yükler.
            dataGridView1.Columns[3].Visible = false;//dataGridView1 kontrolündeki belirli sütunları gizlemek içindir.
            dataGridView1.Columns[4].Visible = false;// Gizlemenin farklı yollarından bir tanesidir
        }

        private void BtnDersListele_Click(object sender, EventArgs e)
        {
            // bu kod bloğunda DbContext sınıfı kullanarak dataGridView1 aracında DERSLER tablosunu göstermek içindir
            DbSinavOgrenciEntities DbdersListele = new DbSinavOgrenciEntities();
            dataGridView1.DataSource = DbdersListele.DERSLER.ToList();

        }

        private void BtnNotListele_Click(object sender, EventArgs e)
        {
            DbSinavOgrenciEntities DbNotListele = new DbSinavOgrenciEntities();
            var Query = from item in DbNotListele.NOTLAR
                        select new {
                            item.NOTID,
                            item.OGR,
                            item.DERS,
                            item.SINAV1,
                            item.SINAV2,
                            item.SINAV3,
                            item.ORTALAMA,
                            item.DURUM};// Burada notlar tablosundan veri almak istedğimiz sütunlardan veri çekmek içindir

            dataGridView1.DataSource = Query.ToList();
            //dataGridView1 de tablo listelemyi sağlamaktadır
        }
        #endregion

        #region tablo Islemleri
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                DbSinavOgrenciEntities Ogrencikaydet = new DbSinavOgrenciEntities();
                Ogrenci Ogr = new Ogrenci();//Bu satır, Ogrenci sınıfından yeni bir nesne oluşturur
                NOTLAR Notlar = new NOTLAR();
                Ogr.AD = TextAd.Text;
                Ogr.SOYAD = TextSoyad.Text;//Bu satırlar, yeni oluşturulan Ogrenci nesnesinin AD ve SOYAD özelliklerini kullanıcıdan alınan verilere atar.
                Notlar.SINAV1 =Convert.ToInt16( TextSinav1.Text);
                Notlar.SINAV2 =Convert.ToInt16(TextSinav2.Text);
                Notlar.SINAV3 = Convert.ToInt16(TextSinav3.Text);
                Notlar.ORTALAMA = Convert.ToDecimal(TextOrtalam.Text);
                Notlar.DURUM = Convert.ToBoolean( TextOrtalam.Text);
                Ogrencikaydet.Ogrenci.Add(Ogr);//Ogrenci nesnesini DbSinavOgrenciEntities bağlamındaki Ogrenci tablosuna ekler.
                Ogrencikaydet.NOTLAR.Add(Notlar);
                Ogrencikaydet.SaveChanges();
                Ogrencikaydet.SaveChanges();//Tüm değişiklikleri veritabanına kaydeder
                MessageBox.Show("Öğrenci kaydedilmiştir");
            }
            catch (Exception)
            
            { 


                MessageBox.Show("Lütfen daha sonra tekrar deneyiniz");
            }

            try
            {
                // Burada da nesneler,sınıflar aynı amaç için kullanılmıştır
                // DERSLER tablosunda bulununan ders kayıtlarını listelemek içindir
           
            if(TextId2.Text != null && Textad2.Text != null)
            {
                DbSinavOgrenciEntities DersKaydet = new DbSinavOgrenciEntities();
                DERSLER Ders = new DERSLER();
                Ders.DERSID = Convert.ToInt32(TextId2.Text);
                Ders.DERSAD = Textad2.Text;
                DersKaydet.DERSLER.Add(Ders);
                DersKaydet.SaveChanges();
                MessageBox.Show("Ders kaydedilmiştir");
            }

                else
                {
                    MessageBox.Show("Lütfen ders adı veya ID giriniz");
                }
            }
           
            
              
            
            catch (Exception)
            {
                MessageBox.Show("Lütfen daha sonra tekrar deneyiniz");
            }

          
           
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            try

            {
                // Burada da nesneler,sınıflar aynı amaç için kullanılmıştır
                // Ogrenci tablosunda bulununan Ogrenci kayıtlarını silmek içindir

                DbSinavOgrenciEntities OgrenciSil = new DbSinavOgrenciEntities();
                int id = Convert.ToInt32(TextId.Text);
                var Query2 = OgrenciSil.Ogrenci.Find(id);
                OgrenciSil.Ogrenci.Remove(Query2);
                OgrenciSil.SaveChanges();
                MessageBox.Show("Ders silinmiştir");
            }
            catch (Exception) 
            {

                MessageBox.Show("Lütfen daha sonra tekrar deneyiniz");

            }

           

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                // Burada da nesneler,sınıflar aynı amaç için kullanılmıştır
                // Ogrenci tablosunda bulununan ogrenci kayıtlarını güncellemek içindir

                DbSinavOgrenciEntities OgrenciGuncelle = new DbSinavOgrenciEntities();
                int Id = Convert.ToInt32(TextId.Text);
                var Query3 = OgrenciGuncelle.Ogrenci.Find(Id);
                Query3.AD = TextAd.Text;
                Query3.SOYAD = TextSoyad.Text;
                Query3.FOTOGRAF = TextFotograf.Text;
                OgrenciGuncelle.SaveChanges();
                MessageBox.Show("Öğrenci Bilgileri Güncellenmiştir");
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen daha sonra tekrar deneyiniz");
            }
            
         

            try
            {
                if (TextId2.Text != null && Textad2 != null)//Bu satır, TextId2 ve Textad2 adlı iki TextBox kontrolünün boş olmadığını kontrol eder. Eğer bu kontrollerde veri varsa if bloğunun içindeki kod çalıştırılır.
                {
                    DbSinavOgrenciEntities DersGuncelle = new DbSinavOgrenciEntities();
                    int Id = Convert.ToInt32(TextId2.Text);//TextId2 TextBox kontrolündeki metni bir tamsayıya (int) dönüştürür ve Id değişkenine atar. Bu güncellemek istenilen dersin ID'sidir.
                    var Query4 = DersGuncelle.DERSLER.Find(Id);//DERSLER tablosunda Id değerine sahip olan dersi bulur ve Query4 değişkenine atar. Find metodu birincil anahtar ile eşleşen ilk kaydı döner.
                    Query4.DERSID = Convert.ToInt32(TextId2.Text);
                    Query4.DERSAD = Textad2.Text;
                    DersGuncelle.SaveChanges();
                    MessageBox.Show("Ders Bilgileri Güncellenmiştir");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen daha sonra tekrar deneyiniz");
            }
          
        }

        private void BtnProsedur_Click(object sender, EventArgs e)
        {
            DbSinavOgrenciEntities Prosedur = new DbSinavOgrenciEntities();
            dataGridView1.DataSource = Prosedur.NotListesi();
        }

        private void BtnBul_Click(object sender, EventArgs e)
        {
            DbSinavOgrenciEntities OgrenciSiralama = new DbSinavOgrenciEntities();
            //Bu satır, veritabanıyla etkileşimde bulunmak için DbSinavOgrenciEntities adında bir bağlam nesnesi oluşturur. 

            if (radioButton1.Checked == true)
            {
                //OrderBy metodu sonuçları sıralamak için kullanılır.
                //x => x.ID > 0 ifadesi, ID'nin 0'dan büyük olup olmadığını kontrol eden bir lambda ifadesidir 
                List<Ogrenci> Liste1 = OgrenciSiralama.Ogrenci.OrderBy(x => x.ID >0).ToList();
                dataGridView1.DataSource = Liste1;
            }

            if (radioButton2.Checked == true)
            {
                List<Ogrenci> Liste2 = OgrenciSiralama.Ogrenci.OrderBy(x => x.AD).ToList();
                dataGridView1.DataSource = Liste2;
            }

            if (radioButton3.Checked == true)
            {
                List<NOTLAR> Liste3 = OgrenciSiralama.NOTLAR.OrderBy(x => x.ORTALAMA >= 50).ToList();
                dataGridView1.DataSource = Liste3;
            }

            if (radioButton4.Checked == true)
            {
                List<NOTLAR> Liste4 = OgrenciSiralama.NOTLAR.OrderBy(x => x.ORTALAMA <= 50).ToList();
                dataGridView1.DataSource = Liste4;
            }

            if (radioButton5.Checked == true)
            {
                int Toplam = OgrenciSiralama.Ogrenci.Count();//Count() metodu, Ogrenci tablosundaki kayıtların toplam sayısını hesaplar. Bu metod tabloyu sorgular ve toplam kayıt sayısını döndürür.
                MessageBox.Show("Toplam öğrenci sayısı" + Toplam.ToString() );
            }
        }
        #endregion

        #region Ogrenci Arama
        private void TextAd_TextChanged(object sender, EventArgs e)
        {
            DbSinavOgrenciEntities OgrenciArama = new DbSinavOgrenciEntities();
            string Aranan = TextAd.Text;
            var Query4 = from item in OgrenciArama.Ogrenci
                         where item.AD.Contains(Aranan)
                         select item;
            dataGridView1.DataSource = Query4.ToList();
        }


        #endregion

        #region NotListeleme
        private void BtnNotGetir_Click(object sender, EventArgs e)
        {
            DbSinavOgrenciEntities NotListeleme = new DbSinavOgrenciEntities();
            var sorgu = from item in NotListeleme.NOTLAR
                        join item2 in NotListeleme.Ogrenci
                        on item.OGR equals item2.ID
                        select new
                        {
                            Ogrenci = item2.AD,
                            SOYAD = item2.SOYAD,
                            SINAV1 = item.SINAV1,
                            SINAV2 = item.SINAV2,
                            SINAV3 = item.SINAV3,
                            ORTALAMA = item.ORTALAMA,
                        };
            dataGridView1.DataSource = sorgu.ToList();
        }
        #endregion
    }
}
