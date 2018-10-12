using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Data.Odbc;

namespace AnimePool {
    class Consulta1 : Form {
        private Button but01, but02, but03, but04;
        private DataGridView dgv1;
        private Label lb0, lb1, lb2, lb3, lb4;
        private TextBox tx2;
        private ComboBox combo01,combo02, combo03;
        private RadioButton rbut0, rbut1;
        private CheckBox cb0, cb1, cb2;
        private Program telaAnterior;
        private OdbcConnection conexao = null;
        private OdbcDataAdapter adaptador;
        private DataSet dados;
        private int contador;

        public Consulta1(Program tela) {
            telaAnterior = tela;
            init();
        }
        public void OpenConn() {
            string sConection = "Dsn=MySQL5_32;uid=root";
            conexao = new OdbcConnection(sConection);
            try {
                conexao.Open();
            } catch (OdbcException) {

            }
        }
        public void CloseConn() {
            try {
                conexao.Close();
            } catch (OdbcException) {

            }
        }
        private void init() {

            this.Text = "Anime Pool";
            this.Size = new Size(800, 500);
            this.CenterToScreen();
            this.BackgroundImage = Image.FromFile(@"../../Images/fundo2.jpg");  

            lb0 = new Label();
            lb0.Text = "Consultas unitárias";
            lb0.Size = new Size(300, 30);
            lb0.Location = new Point(20, 15);
            lb0.ForeColor = Color.DarkBlue;
            lb0.Font = new Font("adobe hebrew", 20);
            lb0.ForeColor = Color.White;
            lb0.BackColor = Color.Transparent;

            lb1 = new Label();
            lb1.Text = "Escolha uma tabela";
            lb1.Size = new Size(160, 20);
            lb1.Location = new Point(20, 60);
            lb1.ForeColor = Color.White;
            lb1.Font = new Font("adobe hebrew bold", 12);
            lb1.BackColor = Color.Transparent;
            
            combo01 = new ComboBox(); // tabela escolhida
            combo01.Location = new Point(20, 85);
            combo01.Size = new Size(130, 30);
            combo01.Items.Add("Anime");
            combo01.Items.Add("Manga");
            combo01.Items.Add("Filme");
            combo01.Items.Add("Estudio");
            combo01.Items.Add("Editora");
            combo01.Items.Add("Endereco");
            combo01.Items.Add("Autor");
            combo01.Font = new Font("adobe hebrew bold", 11);
            combo01.SelectedIndexChanged += new EventHandler(combo01_select);
            combo01.DropDownStyle = ComboBoxStyle.DropDownList;

            // data grid  
            dgv1 = new DataGridView();
            dgv1.Font = new Font("adobe hebrew bold", 11);
            dgv1.Name = "datagrid1";
            dgv1.Location = new Point(270, 60);
            dgv1.Size = new Size(500, 300);
            dgv1.Visible = false;
            dgv1.ForeColor = Color.MidnightBlue;
            dgv1.BackgroundColor = Color.CornflowerBlue;

            but01 = new Button();
            but01.Text = "Pesquisar";
            but01.Location = new Point(30, 420);
            but01.Size = new Size(100, 30);
            but01.Font = new Font("Arial black", 10);
            but01.ForeColor = Color.MidnightBlue;
            but01.Click += new EventHandler(button_consultar);
            but01.Visible = false;

            lb4 = new Label();
            lb4.Text = "Tipo de busca:";
            lb4.Size = new Size(160, 20);
            lb4.Location = new Point(20, 125);
            lb4.ForeColor = Color.White;
            lb4.Font = new Font("adobe hebrew bold", 12);
            lb4.BackColor = Color.Transparent;
            lb4.Visible = false;

            but02 = new Button();
            but02.Text = "Básica";
            but02.Location = new Point(20, 155);
            but02.Size = new Size(130,25);
            but02.Font = new Font("adobe hebrew bold", 11);
            but02.ForeColor = Color.MidnightBlue;
            but02.Click += new EventHandler(button_basica);
            but02.Visible = false;

            but04 = new Button();
            but04.Text = "Parametrizada";
            but04.Location = new Point(20,190);
            but04.Size = new Size(130, 25);
            but04.Font = new Font("adobe hebrew bold", 11);
            but04.ForeColor = Color.MidnightBlue;
            but04.Click += new EventHandler(button_parametrizada);
            but04.Visible = false;

            but03 = new Button();
            but03.Text = "Voltar";
            but03.Location = new Point(670, 420);
            but03.Size = new Size(100, 30);
            but03.Font = new Font("Arial black", 10);
            but03.ForeColor = Color.MidnightBlue;
            but03.Click += new EventHandler(button3_click);

            campos_secretos();

            this.Controls.AddRange(new Control[] { dgv1, lb0, lb1,lb4, but01, but02, but03,but04, combo01 });
        }
        private void combo01_select(object sender, EventArgs e) {
            OpenConn();
            dgv1.Visible = true;
            adaptador = new OdbcDataAdapter("SELECT * FROM " + combo01.Text, conexao);
            dados = new DataSet();
            adaptador.Fill(dados, "info");
            dgv1.DataSource = dados;
            dgv1.DataMember = "info";
            limpar();
            ativar_campos(combo01.Text);
            CloseConn();
        }
        private void combo02_select(object sender, EventArgs e) {
            if (contador == 0) {
                tx2.Visible = true;
                lb3.Visible = true;
                combo03.Visible = true;
            }
            else {
                cb0.Visible = true;
                cb1.Visible = true;
                cb2.Visible = true;
                tx2.Visible = true;
                lb3.Visible = true;
                combo03.Visible = true;
            }
        }
        private void combo03_select(object sender, EventArgs e) {
            rbut0.Visible = true;
            rbut1.Visible = true;
        }
        private void button_consultar(object sender, EventArgs e) {
            OpenConn();
            if (contador == 0) { // é básica
                if (rbut0.Checked == true)
                    adaptador = new OdbcDataAdapter("SELECT * FROM " + combo01.Text + " WHERE " + combo02.Text + "='" + tx2.Text + "' ORDER BY " + combo03.Text + " ASC", conexao);
                else
                    adaptador = new OdbcDataAdapter("SELECT * FROM " + combo01.Text + " WHERE " + combo02.Text + "='" + tx2.Text + "' ORDER BY " + combo03.Text + " DESC", conexao);
            }
            else { // é parametrizada
                if(cb0.Checked == true) {
                    if (rbut0.Checked == true)
                        adaptador = new OdbcDataAdapter("SELECT * FROM " + combo01.Text + " WHERE " + combo02.Text + " LIKE '" + tx2.Text + "%" + "' ORDER BY " + combo03.Text + " ASC", conexao);
                    else
                        adaptador = new OdbcDataAdapter("SELECT * FROM " + combo01.Text + " WHERE " + combo02.Text + " LIKE '" + tx2.Text + "%" + "' ORDER BY " + combo03.Text + " DESC", conexao);
                }
                else if(cb1.Checked == true) {
                    if (rbut0.Checked == true)
                        adaptador = new OdbcDataAdapter("SELECT * FROM " + combo01.Text + " WHERE " + combo02.Text + " LIKE '" + "%"+tx2.Text +"%"+ "' ORDER BY " + combo03.Text + " ASC", conexao);
                    else
                        adaptador = new OdbcDataAdapter("SELECT * FROM " + combo01.Text + " WHERE " + combo02.Text + " LIKE '" + "%"+tx2.Text +"%"+ "' ORDER BY " + combo03.Text + " DESC", conexao);
                }
                else if (cb2.Checked == true) {
                    if (rbut0.Checked == true)
                        adaptador = new OdbcDataAdapter("SELECT * FROM " + combo01.Text + " WHERE " + combo02.Text + " LIKE '" + "%" + tx2.Text + "' ORDER BY " + combo03.Text + " ASC", conexao);
                    else
                        adaptador = new OdbcDataAdapter("SELECT * FROM " + combo01.Text + " WHERE " + combo02.Text + " LIKE '" + "%" + tx2.Text + "' ORDER BY " + combo03.Text + " DESC", conexao);
                }
            }
            dados = new DataSet();
            try {
                adaptador.Fill(dados, "info");
                dgv1.DataSource = dados;
                dgv1.DataMember = "info";
            } catch { MessageBox.Show("Error: Pesquisa Incorreta");  }
            
            limpar();
            CloseConn();
        }
        private void ativar_campos(String tabela) {
            combo02.Items.Clear();
            combo03.Items.Clear();
            if (tabela.Equals("Anime")) {
                combo02.Items.Add("anime_id");
                combo02.Items.Add("nome");
                combo02.Items.Add("genero");
                combo02.Items.Add("episodios");
                combo02.Items.Add("estado");
                combo02.Items.Add("ano");
                combo02.Items.Add("estudio");
            }
            else if (tabela.Equals("Manga")) {
                combo02.Items.Add("manga_id");
                combo02.Items.Add("nome");
                combo02.Items.Add("tipo");
                combo02.Items.Add("genero");
                combo02.Items.Add("volumes");
                combo02.Items.Add("estado");
                combo02.Items.Add("ano");
                combo02.Items.Add("autor");
                combo02.Items.Add("editora");
            }
            else if (tabela.Equals("Filme")) {
                combo02.Items.Add("filme_id");
                combo02.Items.Add("nome");
                combo02.Items.Add("genero");
                combo02.Items.Add("estado");
                combo02.Items.Add("duracao");
                combo02.Items.Add("ano");
                combo02.Items.Add("estudio");
            }
            else if (tabela.Equals("Estudio")) {
                combo02.Items.Add("estudio_id");
                combo02.Items.Add("nome");
                combo02.Items.Add("CNPJ");
                combo02.Items.Add("endereco");
            }
            else if (tabela.Equals("Editora")) {
                combo02.Items.Add("editora_id");
                combo02.Items.Add("nome");
                combo02.Items.Add("CNPJ");
                combo02.Items.Add("endereco");
            }
            else if (tabela.Equals("Endereco")) {
                combo02.Items.Add("endereco_id");
                combo02.Items.Add("CEP");
                combo02.Items.Add("pais");
                combo02.Items.Add("estado");
                combo02.Items.Add("cidade");
                combo02.Items.Add("bairro");
                combo02.Items.Add("rua");
                combo02.Items.Add("numero");
            }
            else {
                combo02.Items.Add("autor_id");
                combo02.Items.Add("nome");
                combo02.Items.Add("sexo");
                combo02.Items.Add("produto");
            }

            string[] items = new string[combo02.Items.Count];
            for (int i = 0; i < combo02.Items.Count; i++) {
                items[i] = combo02.Items[i].ToString();
            }
            combo03.Items.AddRange(items);
            lb4.Visible = true;
            but02.Visible = true;
            but04.Visible = true;
            but01.Visible = true;
        }
        private void campos_secretos() {
            lb2 = new Label();
            lb2.Text = "Escolha uma coluna";
            lb2.Size = new Size(160, 20);
            lb2.Location = new Point(20, 125);
            lb2.ForeColor = Color.White;
            lb2.Font = new Font("adobe hebrew bold", 12);
            lb2.BackColor = Color.Transparent;
            lb2.Visible = false;

            combo02 = new ComboBox();
            combo02.Location = new Point(20, 150);
            combo02.Size = new Size(130, 30);
            combo02.Font = new Font("adobe hebrew bold",11);
            combo02.Visible = false;
            combo02.SelectedIndexChanged += new EventHandler(combo02_select);
            combo02.DropDownStyle = ComboBoxStyle.DropDownList;

            tx2 = new TextBox();
            tx2.Location = new Point(20, 180);
            tx2.Size = new Size(130, 30);
            tx2.Font = new Font("adobe hebrew bold", 11);
            tx2.Visible = false;

            cb0 = new CheckBox();
            cb0.Text = "Início";
            cb0.Location = new Point(20,215);
            cb0.Size = new Size(55, 20);
            cb0.Click += new EventHandler(inicio_click);
            cb0.Visible = false;
            cb0.ForeColor = Color.MidnightBlue;
            cb0.Font = new Font("adobe hebrew bold", 9);
            cb0.BackColor = Color.Transparent;

            cb1 = new CheckBox();
            cb1.Text = "Meio";
            cb1.Location = new Point(80, 215);
            cb1.Size = new Size(50, 20);
            cb1.Click += new EventHandler(meio_click);
            cb1.Visible = false;
            cb1.ForeColor = Color.MidnightBlue;
            cb1.Font = new Font("adobe hebrew bold", 9);
            cb1.BackColor = Color.Transparent;

            cb2 = new CheckBox();
            cb2.Text = "Fim";
            cb2.Location = new Point(140, 215);
            cb2.Size = new Size(50, 20);
            cb2.Click += new EventHandler(fim_click);
            cb2.Visible = false;
            cb2.Font = new Font("adobe hebrew bold", 9);
            cb2.ForeColor = Color.MidnightBlue;
            cb2.BackColor = Color.Transparent;

            lb3 = new Label();
            lb3.Text = "Ordenação por";
            lb3.Location = new Point(20, 250);
            lb3.Size = new Size(120, 20);
            lb3.ForeColor = Color.MidnightBlue;
            lb3.Font = new Font("adobe hebrew bold", 12);
            lb3.BackColor = Color.Transparent;
            lb3.Visible = false;

            combo03 = new ComboBox();
            combo03.Location = new Point(20, 280);
            combo03.Size = new Size(130, 30);
            combo03.Font = new Font("adobe hebrew bold", 11);
            combo03.SelectedIndexChanged += new EventHandler(combo03_select);
            combo03.DropDownStyle = ComboBoxStyle.DropDownList;
            combo03.Visible = false;

            rbut0 = new RadioButton();
            rbut0.Text = "Crescente";
            rbut0.Location = new Point(20, 310);
            rbut0.Size = new Size(110, 30);
            rbut0.Font = new Font("adobe hebrew bold", 11);
            rbut0.Checked = true;
            rbut0.Visible = false;
            rbut0.ForeColor = Color.MidnightBlue;
            rbut0.BackColor = Color.Transparent;

            rbut1 = new RadioButton();
            rbut1.Text = "Decrescente";
            rbut1.Location = new Point(20, 340);
            rbut1.Font = new Font("adobe hebrew bold", 11);
            rbut1.Size = new Size(110, 30);
            rbut1.Visible = false;
            rbut1.ForeColor = Color.MidnightBlue;
            rbut1.BackColor = Color.Transparent;

            this.Controls.AddRange(new Control[] { tx2, lb2, lb3, combo02, combo03, rbut0, rbut1, cb0, cb1, cb2 });

        }
        private void button_basica(object sender, EventArgs e) {
            lb2.Visible = true;
            combo02.Visible = true;
            lb4.Visible = false;
            but02.Visible = false;
            but04.Visible = false;
            contador = 0;
        }
        private void button_parametrizada(object sender, EventArgs e) {
            combo02.Visible = true;
            lb2.Visible = true;
            lb4.Visible = false;
            but02.Visible = false;
            but04.Visible = false;
            contador = 1;
        }
        private void inicio_click(object sender, EventArgs e) {
            cb1.Checked = false;
            cb2.Checked = false;
        }
        private void meio_click(object sender, EventArgs e) {
            cb0.Checked = false;
            cb2.Checked = false;
        }
        private void fim_click(object sender, EventArgs e) {
            cb0.Checked = false;
            cb1.Checked = false;
        }
        private void button3_click(object sender, EventArgs e) {
            telaAnterior.Visible = true;
            this.Close();
        }
        protected override void OnFormClosing(FormClosingEventArgs e) {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            telaAnterior.Visible = true;
        }
        void limpar() {
            tx2.Clear();
            lb2.Visible = false;
            lb3.Visible = false;
            lb4.Visible = false;
            but01.Visible = false;
            but02.Visible = false;
            but04.Visible = false;
            combo02.Visible = false;
            combo03.Visible = false;
            rbut0.Visible = false;
            rbut1.Visible = false;
            cb0.Visible = false;
            cb1.Visible = false;
            cb2.Visible = false;
            tx2.Visible = false;
            combo02.Items.Clear();
            combo03.Items.Clear();
            cb0.Checked = false;
            cb1.Checked = false;
            cb2.Checked = false;
            tx2.Clear();
        }
        void refresh_() {
            adaptador = new OdbcDataAdapter("SELECT * FROM " + combo01.Text, conexao);
            dados = new DataSet();
            adaptador.Fill(dados, "info");
            dgv1.DataSource = dados;
            dgv1.DataMember = "info";
            conexao.Close();
        }
    }
    

}
