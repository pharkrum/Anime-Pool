using System;
using System.Windows.Forms;
using System.Drawing;

namespace AnimePool{
    public class Program : Form {
        private Button but01, but02, but03, but04, but05, but06, but07;
        private Label lb1;
        
        public Program() {
            start();
        }     
        private void start() {

            this.Text = "Anime Pool";
            this.Size = new Size(800, 400);
            this.CenterToScreen();
            this.BackgroundImage = Image.FromFile(@"../../Images/fundo.jpg");

            lb1 = new Label();
            lb1.Text = "Anime   Pool";
            lb1.Size = new Size(260, 50);
            lb1.Location = new Point(260, 50);
            lb1.ForeColor = Color.White;
            lb1.Font = new Font("hobo std", 30);
            lb1.BackColor = Color.Transparent;

            but01 = new Button();
            but01.Text = "Consultar dados";
            but01.Location = new Point(175, 170);
            but01.Size = new Size(150, 60);
            but01.Font = new Font("hobo std", 12);
            but01.ForeColor = Color.MidnightBlue;
            but01.Click += new EventHandler(button1_click);

            but02 = new Button();
            but02.Text = "Manipular dados";
            but02.Location = new Point(425, 170);
            but02.Size = new Size(150, 60);
            but02.Font = new Font("hobo std", 12);
            but02.ForeColor = Color.MidnightBlue;
            but02.Click += new EventHandler(button2_click);

            but03 = new Button();
            but03.Text = "Sair";
            but03.Location = new Point(670, 320);
            but03.Size = new Size(100, 30);
            but03.Font = new Font("Arial black", 10);
            but03.ForeColor = Color.MidnightBlue;
            but03.Click += new EventHandler(button3_click);

            this.Controls.AddRange(new Control[] { but01, but02, but03, but04, lb1});

        }
        private void button1_click(object sender, EventArgs e) {
            reset();
            but04 = new Button();
            but04.Text = "Pesquisa Unitária";
            but04.Location = new Point(150, 170);
            but04.Size = new Size(100, 60);
            but04.Font = new Font("times new roman", 13);
            but04.Font = new Font(but04.Font, FontStyle.Bold);
            but04.ForeColor = Color.MidnightBlue;
            but04.Click += new EventHandler(button_simples);
        
            but05 = new Button();
            but05.Text = "Pesquisa Agrupada";
            but05.Location = new Point(350, 170);
            but05.Size = new Size(100, 60);
            but05.Font = new Font("times new roman", 13);
            but05.Font = new Font(but05.Font, FontStyle.Bold);
            but05.ForeColor = Color.MidnightBlue;
            but05.Click += new EventHandler(button_avancada);

            but06 = new Button();
            but06.Text = "Pesquisa Estatística";
            but06.Location = new Point(550, 170);
            but06.Size = new Size(100, 60);
            but06.Font = new Font("times new roman", 13);
            but06.Font = new Font(but05.Font, FontStyle.Bold);
            but06.ForeColor = Color.MidnightBlue;
            but06.Click += new EventHandler(button_estatistica);

            but07 = new Button();
            but07.Text = "Voltar";
            but07.Location = new Point(670, 320);
            but07.Size = new Size(100, 30);
            but07.Font = new Font("Arial black", 10);
            but07.ForeColor = Color.MidnightBlue;
            but07.Click += new EventHandler(voltar_click);

            this.Controls.AddRange(new Control[] { but04, but05, but06, but07 });
            
        }
        private void button_simples(object sender, EventArgs e) {
            Consulta1 novaTela;
            novaTela = new Consulta1(this);
            novaTela.Show();
            this.Hide();
        }
        private void button_avancada(object sender, EventArgs e) {
            Consulta2 novaTela;
            novaTela = new Consulta2(this);
            novaTela.Show();
            this.Hide();
        }
        private void button_estatistica(object sender, EventArgs e) {
            Consulta3 novaTela;
            novaTela = new Consulta3(this);
            novaTela.Show();
            this.Hide();
        }
        private void button2_click(object sender, EventArgs e) {
            Manipulator novaTela;
            novaTela = new Manipulator(this);
            novaTela.Show();
            this.Hide();
        }
        private void button3_click(object sender, EventArgs e) {
            Close();
        }
        private void voltar_click(object sender, EventArgs e) {
            but01.Visible = true; 
            but02.Visible = true;  
            but03.Visible = true;
            but04.Visible = false;
            but05.Visible = false;
            but06.Visible = false;
            but07.Visible = false;
        }
        private void reset() {
            but01.Hide(); 
            but02.Hide(); 
            but03.Hide(); 
        }
        static void Main(string[] args) {
            Application.Run(new Program());
        }
    }
}
