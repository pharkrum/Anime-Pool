using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Data.Odbc;

namespace AnimePool {
    class Consulta2 : Form {
        private Button but01, but02, but03, but04, but05;
        private TextBox  tx2, tx5, tx6;
        private Label lb1, lb2, lb3, lb4, lb6, lb7, lb8;
        private ComboBox combo01, combo02, combo03, combo04, combo05, combo06;
        private Program telaAnterior;
        private OdbcConnection conexao = null;
        private OdbcDataAdapter adaptador;
        private DataSet dados;
        private DataGridView dgv1;

        public Consulta2(Program tela) {
            telaAnterior = tela;
            init();
        }
        public void OpenConn() {
            string sConection = "DSN=MySQL5_32;MultipleActiveResultSets=True";
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
        public void init() {

            this.Text = "Anime Pool";
            this.Size = new Size(800, 400);
            this.CenterToScreen();
            this.BackgroundImage = Image.FromFile(@"../../Images/fundo3.jpg");

            lb1 = new Label();
            lb1.Text = "Consulta agrupada";
            lb1.Size = new Size(300, 30);
            lb1.Location = new Point(20, 10);
            lb1.ForeColor = Color.White;
            lb1.Font = new Font("adobe hebrew", 20);
            lb1.BackColor = Color.Transparent;

            lb2 = new Label();
            lb2.Text = "Tabela 1";
            lb2.Size = new Size(100, 20);
            lb2.Location = new Point(20, 60);
            lb2.ForeColor = Color.White;
            lb2.Font = new Font("adobe hebrew", 13);
            lb2.BackColor = Color.Transparent;

            combo01 = new ComboBox();
            combo01.Location = new Point(20, 85);
            combo01.Size = new Size(100, 30);
            combo01.Items.Add("Anime");
            combo01.Items.Add("Manga");
            combo01.Items.Add("Filme");
            combo01.Font = new Font("adobe hebrew bold", 11);
            combo01.SelectedIndexChanged += new EventHandler(combo01_select);
            combo01.DropDownStyle = ComboBoxStyle.DropDownList;

            lb6 = new Label();
            lb6.Text = "Coluna";
            lb6.Location = new Point(20, 125);
            lb6.Size = new Size(100, 20);
            lb6.BackColor = Color.Transparent;
            lb6.ForeColor = Color.White;
            lb6.Font = new Font("adobe hebrew", 13);
            lb6.Visible = false;

            combo02 = new ComboBox();
            combo02.Location = new Point(20, 145);
            combo02.Font = new Font("adobe hebrew bold", 11);
            combo02.Size = new Size(100, 30);
            combo02.Visible = false;
            combo02.DropDownStyle = ComboBoxStyle.DropDownList;

            tx2 = new TextBox();
            tx2.Location = new Point(20, 180);
            tx2.Size = new Size(100, 20);
            tx2.Font = new Font("adobe hebrew bold", 11);
            tx2.Visible = false;
            /////////////////////////////////////////////////
            but03 = new Button();
            but03.Text = "Voltar";
            but03.Location = new Point(670, 320);
            but03.Size = new Size(100, 30);
            but03.Font = new Font("Arial black", 10);
            but03.Click += new EventHandler(button3_click);

            but04 = new Button();
            but04.Text = "Buscar";
            but04.Location = new Point(20, 320);
            but04.Size = new Size(100, 30);
            but04.Font = new Font("Arial black", 10);
            but04.Click += new EventHandler(button_buscar);

            // data grid
            dgv1 = new DataGridView();
            dgv1.Name = "datagrid1";
            dgv1.Location = new Point(420, 50);
            dgv1.Size = new Size(350, 250);
            dgv1.Visible = false;
            dgv1.Font = new Font("adobe hebrew bold", 11);
            dgv1.ForeColor = Color.MidnightBlue;
            dgv1.BackgroundColor = Color.CornflowerBlue;

            but05 = new Button();
            but05.Text = "Limpar janela";
            but05.Location = new Point(350, 320);
            but05.Size = new Size(140, 30);
            but05.Font = new Font("Arial black", 10);
            but05.Click += new EventHandler(button_novaBusca);
            but05.Visible = false;

            campos_secretos();

            this.Controls.AddRange(new Control[] { lb1, tx2, lb2, lb6, combo01,combo02, but03, but04, but05, dgv1 });
        }
        public void campos_secretos() {
            but01 = new Button();
            but01.Location = new Point(150, 85);
            but01.Size = new Size(80, 26);
            but01.Text = "+";
            but01.Font = new Font("adobe hebrew", 14);
            but01.Click += new EventHandler(but01_click);
            but01.Visible = false;

            lb3 = new Label();
            lb3.Text = "Tabela 2";
            lb3.Location = new Point(150, 60);
            lb3.Size = new Size(100, 20);
            lb3.ForeColor = Color.White;
            lb3.Font = new Font("adobe hebrew", 13);
            lb3.BackColor = Color.Transparent;
            lb3.Visible = false;

            combo03 = new ComboBox();
            combo03.Location = new Point(150, 85);
            combo03.Size = new Size(100, 30);
            combo03.Font = new Font("adobe hebrew bold", 11);
            combo03.SelectedIndexChanged += new EventHandler(combo03_select);
            combo03.DropDownStyle = ComboBoxStyle.DropDownList;
            combo03.Visible = false;

            lb7 = new Label();
            lb7.Text = "Coluna";
            lb7.Location = new Point(150, 125);
            lb7.Size = new Size(100, 20);
            lb7.BackColor = Color.Transparent;
            lb7.ForeColor = Color.White;
            lb7.Font = new Font("adobe hebrew", 13);
            lb7.Visible = false;

            combo05 = new ComboBox();
            combo05.Location = new Point(150, 145);
            combo05.Size = new Size(100, 30);
            combo05.Font = new Font("adobe hebrew bold", 11);
            combo05.DropDownStyle = ComboBoxStyle.DropDownList;
            combo05.Visible = false;

            tx5 = new TextBox();
            tx5.Location = new Point(150, 180);
            tx5.Size = new Size(100, 20);
            tx5.Font = new Font("adobe hebrew bold", 11);
            tx5.Visible = false;
            /////////////////////////////////////////////////////// fim tabela 2
            // inicio tabela 3
            but02 = new Button();
            but02.Location = new Point(280, 85);
            but02.Size = new Size(80, 26);
            but02.Text = "+";
            but02.Font = new Font("adobe hebrew", 14);
            but02.Click += new EventHandler(but02_click);
            but02.Visible = false;

            lb4 = new Label();
            lb4.Text = "Tabela 3";
            lb4.Location = new Point(280, 60);
            lb4.Size = new Size(100, 20);
            lb4.ForeColor = Color.White;
            lb4.BackColor = Color.Transparent;
            lb4.Font = new Font("adobe hebrew", 13);
            lb4.Visible = false;

            combo04 = new ComboBox();
            combo04.Location = new Point(280, 85);
            combo04.Size = new Size(100, 30);
            combo04.Font = new Font("adobe hebrew bold", 11);
            combo04.SelectedIndexChanged += new EventHandler(combo04_select);
            combo04.DropDownStyle = ComboBoxStyle.DropDownList;
            combo04.Visible = false;

            lb8 = new Label();
            lb8.Text = "Coluna";
            lb8.Location = new Point(280, 125);
            lb8.Size = new Size(100, 20);
            lb8.BackColor = Color.Transparent;
            lb8.ForeColor = Color.White;
            lb8.Font = new Font("adobe hebrew", 13);
            lb8.Visible = false;

            combo06 = new ComboBox();
            combo06.Location = new Point(280, 145);
            combo06.Size = new Size(100, 30);
            combo06.Font = new Font("adobe hebrew bold", 11);
            combo06.DropDownStyle = ComboBoxStyle.DropDownList;
            combo06.Visible = false;

            tx6 = new TextBox();
            tx6.Location = new Point(280, 180);
            tx6.Size = new Size(100, 20);
            tx6.Font = new Font("adobe hebrew bold", 11);
            tx6.Visible = false;

            this.Controls.AddRange(new Control[] { lb3, lb4,tx5,tx6,lb7,lb8, combo03, combo04,combo05, combo06, but01, but02 });
        }
        private void combo01_select(object sender, EventArgs e) {
            combo02.Items.Clear();
            combo03.Items.Clear();
            combo04.Items.Clear();
            combo05.Items.Clear();
            combo06.Items.Clear();
            combo03.Text = "";
            combo04.Text = "";
            combo05.Text = "";
            combo06.Text = "";
            combo02.Text = "";
            but02.Visible = false;
            but01.Visible = true;
            lb4.Visible = false;
            lb7.Visible = false;
            lb8.Visible = false;
            tx2.Visible = true;
            tx5.Visible = false;
            tx6.Visible = false;
            tx6.Visible = false;
            combo02.Visible = true;
            combo03.Visible = false;
            combo04.Visible = false;
            combo05.Visible = false;
            combo06.Visible = false;
            lb6.Visible = true;


            if (combo01.Text.Equals("Anime")){
                combo03.Items.Add("estudio");
                combo03.Items.Add("endereco");
                combo04.Items.Add("endereco");
                combo04.Items.Add("estudio");
                /////////////////////////////
                combo02.Items.Add("anime_id");
                combo02.Items.Add("nome");
                combo02.Items.Add("genero");
                combo02.Items.Add("episodios");
                combo02.Items.Add("estado");
                combo02.Items.Add("ano");
                combo02.Items.Add("estudio");
            }
            else if (combo01.Text.Equals("Manga")) {
                combo03.Items.Add("autor");
                combo03.Items.Add("editora");
                combo04.Items.Add("autor");
                combo04.Items.Add("editora");
                ////////////////////////////
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
            else if (combo01.Text.Equals("Filme")) {
                combo03.Items.Add("estudio");
                combo03.Items.Add("endereco");
                combo04.Items.Add("endereco");
                combo04.Items.Add("estudio");
                /////////////////////////////
                combo02.Items.Add("filme_id");
                combo02.Items.Add("nome");
                combo02.Items.Add("genero");
                combo02.Items.Add("estado");
                combo02.Items.Add("duracao");
                combo02.Items.Add("ano");
                combo02.Items.Add("estudio");
            }
        }
        private void combo03_select(object sender, EventArgs e) {
            but02.Visible = true;
            lb8.Visible = false;
            lb7.Visible = true;
            combo04.Text = "";
            combo04.Visible = false;
            lb4.Visible = false;
            combo05.Visible = true;
            tx5.Visible = true;
            combo06.Visible = false;
            tx6.Visible = false;
            combo05.Items.Clear();
            combo06.Items.Clear();
            combo05.Text = "";
            combo06.Text = "";

            if (combo03.Text.Equals("filme")) {
                combo05.Items.Add("filme_id");
                combo05.Items.Add("nome");
                combo05.Items.Add("genero");
                combo05.Items.Add("estado");
                combo05.Items.Add("duracao");
                combo05.Items.Add("ano");
                combo05.Items.Add("estudio");
            }
            else if (combo03.Text.Equals("estudio")) {
                combo05.Items.Add("estudio_id");
                combo05.Items.Add("nome");
                combo05.Items.Add("CNPJ");
                combo05.Items.Add("endereco");
            }
            else if (combo03.Text.Equals("editora")) {
                combo05.Items.Add("editora_id");
                combo05.Items.Add("nome");
                combo05.Items.Add("CNPJ");
                combo05.Items.Add("endereco");
            }
            else if (combo03.Text.Equals("endereco")) {
                combo05.Items.Add("endereco_id");
                combo05.Items.Add("CEP");
                combo05.Items.Add("pais");
                combo05.Items.Add("estado");
                combo05.Items.Add("cidade");
                combo05.Items.Add("bairro");
                combo05.Items.Add("rua");
                combo05.Items.Add("numero");
            }
            else {
                combo05.Items.Add("autor_id");
                combo05.Items.Add("nome");
                combo05.Items.Add("sexo");
                combo05.Items.Add("produto");
            }
        }
        private void combo04_select(object sender, EventArgs e) {
            combo06.Items.Clear();
            combo06.Text = "";
            combo06.Visible = true;
            tx6.Visible = true;
            lb8.Visible = true;

            if (combo04.Text.Equals("filme")) {
                combo06.Items.Add("filme_id");
                combo06.Items.Add("nome");
                combo06.Items.Add("genero");
                combo06.Items.Add("estado");
                combo06.Items.Add("duracao");
                combo06.Items.Add("ano");
                combo06.Items.Add("estudio");
            }
            else if (combo04.Text.Equals("estudio")) {
                combo06.Items.Add("estudio_id");
                combo06.Items.Add("nome");
                combo06.Items.Add("CNPJ");
                combo06.Items.Add("endereco");
            }
            else if (combo04.Text.Equals("editora")) {
                combo06.Items.Add("editora_id");
                combo06.Items.Add("nome");
                combo06.Items.Add("CNPJ");
                combo06.Items.Add("endereco");
            }
            else if (combo04.Text.Equals("endereco")) {
                combo06.Items.Add("endereco_id");
                combo06.Items.Add("CEP");
                combo06.Items.Add("pais");
                combo06.Items.Add("estado");
                combo06.Items.Add("cidade");
                combo06.Items.Add("bairro");
                combo06.Items.Add("rua");
                combo06.Items.Add("numero");
            }
            else {
                combo06.Items.Add("autor_id");
                combo06.Items.Add("nome");
                combo06.Items.Add("sexo");
                combo06.Items.Add("produto");
            }
        }
        private void but01_click(object sender, EventArgs e) {
            but01.Visible = false;
            lb3.Visible = true;
            combo03.Visible = true;
        }
        private void but02_click(object sender, EventArgs e) {
            but02.Visible = false;
            lb4.Visible = true;
            combo04.Visible = true;
        }
        private void button_buscar(object sender, EventArgs e) {
            OpenConn();
            adaptador = new OdbcDataAdapter("SELECT " +combo01.Text+ "." +combo02.Text+ ", " +combo03.Text+ "." +combo05.Text+ ", " +combo04.Text+ "." +combo06.Text+ " FROM " 
            +combo01.Text+ ", " +combo03.Text+ ", " +combo04.Text+  " WHERE " +combo01.Text+"."+combo02.Text+ "='" +tx2.Text+ "' AND "
            +combo03.Text+ "." +combo05.Text+ "='" +tx5.Text+ "' AND " +combo04.Text+ "." +combo06.Text+ "='"+tx6.Text+"'",conexao);
            try {
                dados = new DataSet();
                adaptador.Fill(dados, "info");
                dgv1.DataSource = dados;
                dgv1.DataMember = "info";
                dgv1.Visible = true;
            } catch { MessageBox.Show("Error: Pesquisa incorreta"); dgv1.Visible = false; }

            but05.Visible = true;
            
            CloseConn();
        }
        public void button_novaBusca(object sender, EventArgs e) {
            Consulta2 renova = new Consulta2(telaAnterior);
            renova.Visible = true;
            this.Dispose();
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
    }
}
