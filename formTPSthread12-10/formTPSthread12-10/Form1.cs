using System;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Text;
using System.Timers;
using Microsoft.VisualBasic.ApplicationServices;
using System.Reflection.Emit;

namespace formTPSthread12_10
{
   

    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer[] timer;
        private System.Windows.Forms.Label[] lblTempo;
        private int cont = 0;
        public Form1()
        {
            InitializeComponent();
            
        }


        private void Form1_Load(object sender, EventArgs e)
        {
        timer = new System.Windows.Forms.Timer[100];
        lblTempo = new System.Windows.Forms.Label[100];

    }

        private void button1_Click(object sender, EventArgs e)
        {   
            lblTempo[cont] = new System.Windows.Forms.Label();
            lblTempo[cont].Location = new Point((cont%10)*100, cont/10*100);
            lblTempo[cont].Font = new Font(FontFamily.Families[0], 36);
            lblTempo[cont].Size = new Size(100, 100);
            this.Controls.Add(lblTempo[cont]);
            lblTempo[cont].Text = numericUpDown1.Value.ToString();

            int i = cont;
            Thread threadTimer = new Thread(() => Countdown(i));
            threadTimer.Start();
            cont++;
        }

        private void Countdown(int i)
        {
            int tempoRimanente = Convert.ToInt32(lblTempo[i].Text);

            
            while (tempoRimanente > 0)
            {
                Thread.Sleep(1000);
                tempoRimanente--;

                this.Invoke(() =>
                {
                    lblTempo[i].Text = tempoRimanente.ToString();
                });
            }

            
            this.Invoke(() =>
            {
                MostraImmagine();
            });
        }

        private void MostraImmagine()
        {
            Form formBoom = new Form();
            formBoom.Size = new System.Drawing.Size(275, 255);


            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "immagini", "boom.png"));
            pictureBox.Dock = DockStyle.Fill;

            Button buttonOk = new Button();
            buttonOk.Text = "OK";
            buttonOk.Dock = DockStyle.Bottom;
            buttonOk.Click += (sender, e) => formBoom.Close();

            formBoom.Controls.Add(pictureBox);
            formBoom.Controls.Add(buttonOk);

            formBoom.ShowDialog();
        }
    }
}



