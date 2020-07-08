using System;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        static readonly string data1 = @"C:\Users\Enes Aydos\Desktop\data.txt"; //data dosyasının konumu
        Random rnd = new Random();
        static string[] ilkdeger = File.ReadAllLines(data1);
        static string _deger = ilkdeger[0].Trim().Replace(" ", "");
        static int length = _deger.Length;
        private void button1_Click(object sender, EventArgs e)
        {
            int time = 0;
            int[] toplamPuanlar = new int[500];
            int min = 0;
            string[] minDugumSirasi = new string[0];
            while (time < 500)
            {
                string[] mevcutDugumSirasi = Baslangic();
                int mevcutDugumSirasiPuani = DugumSirasiPuaniniHesapla(mevcutDugumSirasi);
            ADIM1:
                string[] yeniDugumSirasi = YeniDugumSirasi(mevcutDugumSirasi);// ADIM 1
                int yeniDugumSirasiPuani = DugumSirasiPuaniniHesapla(yeniDugumSirasi);// ADIM 2
                // ADIM 3
                if (mevcutDugumSirasiPuani < yeniDugumSirasiPuani) 
                {
                    goto ADIM1;
                }
                if (yeniDugumSirasiPuani < mevcutDugumSirasiPuani)
                {
                    mevcutDugumSirasi = yeniDugumSirasi;
                    goto ADIM1;
                }
                toplamPuanlar[time] = yeniDugumSirasiPuani;
                if (time == 0)
                {
                    min = toplamPuanlar[time];
                    minDugumSirasi = yeniDugumSirasi;
                }
                if (time > 0 && toplamPuanlar[time] < min)
                {
                    min = toplamPuanlar[time];
                    minDugumSirasi = yeniDugumSirasi;
                }
                time++;
            }
            label1.Text ="Puan: "+ min.ToString();
            textBox1.Text = "Değerler Sırası: " + string.Join(",", minDugumSirasi);
        }
        private string[] Baslangic()
        {
            string[] result = Deger();
            rnd.Karistir(result);
            return result;
        }
        private string[] YeniDugumSirasi(string[] deger)
        {
            int dugum1 = rnd.Next(0, length);
        LOOP:
            int dugum2 = rnd.Next(0, length);
            if (dugum1 == dugum2)
            {
                goto LOOP;
            }
            string temp = deger[dugum1];
            deger[dugum1] = deger[dugum2];
            deger[dugum2] = temp;
            return deger;
        }
        private int DugumSirasiPuaniniHesapla(string[] deger)
        {
            int toplamPuan = 0;
            for (int i = 0; i < length; i++)
            {
                toplamPuan += DugumlerinPuaniniHesapla(deger)[i];
            }
            return toplamPuan;
        }
        private int[] DugumlerinPuaniniHesapla(string[] dugum)
        {
            int k = 0;
            int[] result = new int[dugum.Length];
            for (int i = 0; i < dugum.Length; i++)
            {
                if (i == 0)
                {
                    result[i] += Convert.ToInt32(dugum[i]);
                }
                else
                {
                    while (k <= i)
                    {
                        result[i] += Convert.ToInt32(dugum[i - k]);
                        k++;
                    }
                }
                k = 0;
            }
            return result;
        }
        private string[] Deger()
        {
            string[] deger = new string[length];
            for (int i = 0; i < length; i++)
            {
                deger[i] = _deger.Substring(i, 1);
            }
            return deger;
        }
    }    
    public static class RandomExtensions
    {
        public static void Karistir<T>(this Random rnd, T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = rnd.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }
    }
}
