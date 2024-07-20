using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {


        private AnaMenu anaMenu;

        // Butonların koordinatlarını ve renk bilgilerini saklamak için bir sözlük oluşturun
        private Dictionary<Button, Tuple<int, int, bool>> buttonInfo = new Dictionary<Button, Tuple<int, int, bool>>();

        // SQL Server bağlantı dizesi
        private string connectionString = "Server=localhost;Database=uyumsoft;Integrated Security=True;";
        private int kisiNooo = 1;

        public Form1(AnaMenu anaMenu)
        {
            InitializeComponent();
            this.anaMenu = anaMenu;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ListBox'ı en alta yerleştir
            listBox1.SendToBack();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string satirText = textBox1.Text;
            string sutunText = textBox2.Text;
            string salonText = textBox3.Text;

            int satir, sutun, salon;
            if (int.TryParse(satirText, out satir) && int.TryParse(sutunText, out sutun) && int.TryParse(salonText, out salon))
            {
                CreateButtons(satir, sutun, salon);
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir satır, sütun ve salon değeri girin.");
            }
        }

        private void CreateButtons(int rows, int columns, int salon)
        {
            int buttonWidth = 1000 / columns;
            int buttonHeight = 500 / rows;
            int startX = 123;
            int startY = 150;
            int padding = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Button button = new Button();
                    button.Location = new System.Drawing.Point(startX + (j * (buttonWidth + padding)),
                        startY + (i * (buttonHeight + padding)));
                    button.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
                    button.Text = $"ben bir butonum";
                    button.UseVisualStyleBackColor = true;
                    button.Click += Button_Click;

                    // Butonun koordinatlarını ve renk bilgisini sözlüğe ekleyin
                    buttonInfo.Add(button, new Tuple<int, int, bool>(i + 1, j + 1, false));

                    this.Controls.Add(button);
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                if (buttonInfo.TryGetValue(clickedButton, out Tuple<int, int, bool> info))
                {
                    // Butonun renk durumunu değiştirin
                    if (info.Item3)
                    {
                        clickedButton.BackColor = System.Drawing.Color.LightGray; // Eski rengi açık gri yap
                    }
                    else
                    {
                        clickedButton.BackColor = System.Drawing.Color.Red;
                    }
                    // Renk durumunu güncelleyin
                    buttonInfo[clickedButton] = new Tuple<int, int, bool>(info.Item1, info.Item2, !info.Item3);

                    // Satır ve sütun bilgilerini ListBox'a ekleyin
                    listBox1.Items.Add($"Bu buton {info.Item1}. satır {info.Item2}. sütundadır.");

                    // Veritabanına kaydet
                    SaveButtonInfoToDatabase(info.Item1, info.Item2, !info.Item3, textBox3.Text);

                    // Satır ve sütun bilgilerini göster
                    MessageBox.Show($"Bu buton {info.Item1}. satır {info.Item2}. sütundadır.");
                }
            }
        }

        private void SaveButtonInfoToDatabase(int row, int column, bool isRed, string salon)
        {
            // SQL komutu: Veritabanına veri eklemek
            string query = "INSERT INTO uyumsoft.dbo.deneme (kisiNo, salonNo, koordinat) VALUES (@kisiNo, @salonNo, @koordinat)";

            // Parametre değerleri
            int kisiNo = kisiNooo; // Örnek olarak sabit bir değer

            string salonNo = salon; // Örnek olarak sabit bir değer
            string koordinat = row.ToString() + column.ToString();

            // SQL bağlantısı ve komutu
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                // Parametreleri ekleyin
                command.Parameters.AddWithValue("@kisiNo", kisiNo);
                command.Parameters.AddWithValue("@salonNo", salonNo);
                command.Parameters.AddWithValue("@koordinat", koordinat);

                try
                {
                    // Bağlantıyı aç
                    connection.Open();

                    // Komutu çalıştır
                    command.ExecuteNonQuery();

                    // Başarılı mesajı göster
                    kisiNooo += 1;
                    MessageBox.Show("Veri başarıyla kaydedildi.");
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Veritabanına kaydetme sırasında bir SQL hatası oluştu: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanına kaydetme sırasında bir hata oluştu: " + ex.Message);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
