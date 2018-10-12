using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Data.Odbc;

namespace AnimePool {
    class Consulta3 : Form {
        private Button but01, but02, but03, but04;
        private DataGridView dgv1;
        private Label lb0, lb1, lb2, lb3;
        private TextBox tx0;
        private ComboBox combo00, combo01, combo02, combo03;
        private RadioButton rbut0, rbut1;
        private CheckBox cb0, cb1, cb2;
        private Program telaAnterior;
        private OdbcConnection conexao = null;
        private OdbcDataAdapter adaptador;
        private OdbcCommand comando;
        private DataSet dados;
        private int contador;

        public Consulta3(Program tela) {
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
        private void init() {

            this.Text = "Anime Pool";
            this.Size = new Size(800, 500);
            this.CenterToScreen();

            campos_secretos();

            lb1 = new Label();
            lb1.Text = "Consultas";
            lb1.Size = new Size(300, 30);
            lb1.Location = new Point(20, 10);
            lb1.ForeColor = Color.DarkBlue;
            lb1.Font = new Font("arial", 15);

            lb2 = new Label();
            lb2.Text = "Escolha uma tabela";
            lb2.Size = new Size(150, 20);
            lb2.Location = new Point(20, 50);
            lb2.ForeColor = Color.Coral;
            lb2.Font = new Font("arial", 11);

            combo01 = new ComboBox();
            combo01.Location = new Point(20, 80);
            combo01.Size = new Size(150, 30);
            combo01.Items.Add("Anime");
            combo01.Items.Add("Manga");
            combo01.Items.Add("Filme");
            combo01.Items.Add("Estudio");
            combo01.Items.Add("Editora");
            combo01.Items.Add("Endereco");
            combo01.Items.Add("Autor");
            //combo01.TextChanged += new EventHandler(combo01_select);
            combo01.SelectedIndexChanged += new EventHandler(combo01_select);
            combo01.DropDownStyle = ComboBoxStyle.DropDownList;

            // data grid  
            dgv1 = new DataGridView();
            dgv1.Name = "datagrid1";
            dgv1.Location = new Point(270, 30);
            dgv1.Size = new Size(500, 350);

            but01 = new Button();
            but01.Text = "Pesquisar";
            but01.Location = new Point(30, 420);
            but01.Size = new Size(100, 30);
            but01.Click += new EventHandler(button_consultar);

            but03 = new Button();
            but03.Text = "Voltar";
            but03.Location = new Point(670, 420);
            but03.Size = new Size(100, 30);
            but03.Click += new EventHandler(button3_click);

            this.Controls.AddRange(new Control[] { dgv1, lb1, lb2, but01, but03, combo01 });
        }
        private void combo00_select(object sender, EventArgs e) {
            combo03.Visible = true;

        }
        private void combo01_select(object sender, EventArgs e) {
            OpenConn();
            adaptador = new OdbcDataAdapter("SELECT * FROM " + combo01.Text, conexao);
            dados = new DataSet();
            adaptador.Fill(dados, "info");
            dgv1.DataSource = dados;
            dgv1.DataMember = "info";

            ativar_campos(combo01.Text);
            CloseConn();
        }
        private void combo02_select(object sender, EventArgs e) {
            rbut0.Visible = true;
            rbut1.Visible = true;
            
        }
        private void combo03_select(object sender, EventArgs e) {
            lb3.Visible = true;
            combo02.Visible = true;
            cb0.Visible = true;
            cb1.Visible = true;
            cb2.Visible = true;
            tx0.Visible = true;
        }
        private void button_consultar(object sender, EventArgs e) {
            OpenConn();
            if (combo03.Text.Equals("Média")) {
                if (cb0.Checked == true) { // maior
                    if (rbut0.Checked == true)
                        adaptador = new OdbcDataAdapter("SELECT * " + ", avg(" +combo00.Text+") " + "FROM " + combo01.Text  + " HAVING " + " avg(" +combo00.Text+ ") " + ">'" + tx0.Text + "' ORDER BY " + combo02.Text + " ASC", conexao);
                    else
                        adaptador = new OdbcDataAdapter("SELECT * " + ", avg(" + combo00.Text + ") " + "FROM " + combo01.Text + " HAVING " + " avg(" + combo00.Text + ") " + ">'" + tx0.Text + "' ORDER BY " + combo02.Text + " DESC", conexao);
                }
                else if (cb1.Checked == true) { // menor
                    if (rbut0.Checked == true)
                        adaptador = new OdbcDataAdapter("SELECT * " + ", avg(" + combo00.Text + ") " + "FROM " + combo01.Text + " HAVING " + " avg(" + combo00.Text + ") " + "<'" + tx0.Text + "' ORDER BY " + combo02.Text + " ASC", conexao);
                    else
                        adaptador = new OdbcDataAdapter("SELECT * " + ", avg(" + combo00.Text + ") " + "FROM " + combo01.Text + " HAVING " + " avg(" + combo00.Text + ") " + "<'" + tx0.Text + "' ORDER BY " + combo02.Text + " DESC", conexao);
                }
                else  { // igual
                    if (rbut0.Checked == true)
                        adaptador = new OdbcDataAdapter("SELECT * " + ", avg(" + combo00.Text + ") " + "FROM " + combo01.Text + " HAVING " + " avg(" + combo00.Text + ") " + "='" + tx0.Text + "' ORDER BY " + combo02.Text + " ASC", conexao);
                    else
                        adaptador = new OdbcDataAdapter("SELECT * " + ", avg(" + combo00.Text + ") " + "FROM " + combo01.Text + " HAVING " + " avg(" + combo00.Text + ") " + ">'" + tx0.Text + "' ORDER BY " + combo02.Text + " DESC", conexao);
                }
            }
            else if (combo03.Text.Equals("Contagem")) {
                if (cb0.Checked == true) { // maior
                    if (rbut0.Checked == true)
                        adaptador = new OdbcDataAdapter("SELECT * " + ", count(" + combo00.Text + ") " + "FROM " + combo01.Text + " HAVING " + " count(" + combo00.Text + ") " + ">'" + tx0.Text + "' ORDER BY " + combo02.Text + " ASC", conexao);
                    else
                        adaptador = new OdbcDataAdapter("SELECT * " + ", count(" + combo00.Text + ") " + "FROM " + combo01.Text + " HAVING " + " count(" + combo00.Text + ") " + ">'" + tx0.Text + "' ORDER BY " + combo02.Text + " DESC", conexao);
                }
                else if (cb1.Checked == true) { // menor
                    if (rbut0.Checked == true)
                        adaptador = new OdbcDataAdapter("SELECT * " + ", count(" + combo00.Text + ") " + "FROM " + combo01.Text + " HAVING " + " count(" + combo00.Text + ") " + "<'" + tx0.Text + "' ORDER BY " + combo02.Text + " ASC", conexao);
                    else
                        adaptador = new OdbcDataAdapter("SELECT * " + ", count(" + combo00.Text + ") " + "FROM " + combo01.Text + " HAVING " + " count(" + combo00.Text + ") " + "<'" + tx0.Text + "' ORDER BY " + combo02.Text + " DESC", conexao);
                }
                else { // igual
                    if (rbut0.Checked == true)
                        adaptador = new OdbcDataAdapter("SELECT * " + ", count(" + combo00.Text + ") " + "FROM " + combo01.Text + " HAVING " + " count(" + combo00.Text + ") " + "='" + tx0.Text + "' ORDER BY " + combo02.Text + " ASC", conexao);
                    else
                        adaptador = new OdbcDataAdapter("SELECT * " + ", count(" + combo00.Text + ") " + "FROM " + combo01.Text + " HAVING " + " count(" + combo00.Text + ") " + ">'" + tx0.Text + "' ORDER BY " + combo02.Text + " DESC", conexao);
                }
            }
            else {
                if (cb0.Checked == true) { // maior
                    if (rbut0.Checked == true)
                        adaptador = new OdbcDataAdapter("SELECT * " + ", sum(" + combo00.Text + ") " + "FROM " + combo01.Text + " HAVING " + " sum(" + combo00.Text + ") " + ">'" + tx0.Text + "' ORDER BY " + combo02.Text + " ASC", conexao);
                    else
                        adaptador = new OdbcDataAdapter("SELECT * " + ", sum(" + combo00.Text + ") " + "FROM " + combo01.Text + " HAVING " + " sum(" + combo00.Text + ") " + ">'" + tx0.Text + "' ORDER BY " + combo02.Text + " DESC", conexao);
                }
                else if (cb1.Checked == true) { // menor
                    if (rbut0.Checked == true)
                        adaptador = new OdbcDataAdapter("SELECT * " + ", sum(" + combo00.Text + ") " + "FROM " + combo01.Text + " HAVING " + " sum(" + combo00.Text + ") " + "<'" + tx0.Text + "' ORDER BY " + combo02.Text + " ASC", conexao);
                    else
                        adaptador = new OdbcDataAdapter("SELECT * " + ", sum(" + combo00.Text + ") " + "FROM " + combo01.Text + " HAVING " + " sum(" + combo00.Text + ") " + "<'" + tx0.Text + "' ORDER BY " + combo02.Text + " DESC", conexao);
                }
                else { // igual
                    if (rbut0.Checked == true)
                        adaptador = new OdbcDataAdapter("SELECT * " + ", sum(" + combo00.Text + ") " + "FROM " + combo01.Text + " HAVING " + " sum(" + combo00.Text + ") " + "='" + tx0.Text + "' ORDER BY " + combo02.Text + " ASC", conexao);
                    else
                        adaptador = new OdbcDataAdapter("SELECT * " + ", sum(" + combo00.Text + ") " + "FROM " + combo01.Text + " HAVING " + " sum(" + combo00.Text + ") " + ">'" + tx0.Text + "' ORDER BY " + combo02.Text + " DESC", conexao);
                }

            }

            dados = new DataSet();
            adaptador.Fill(dados, "info");
            dgv1.DataSource = dados;
            dgv1.DataMember = "info";

            ativar_campos(combo01.Text);
            limpar();
            CloseConn();
        }
        private void ativar_campos(String tabela) {
            combo00.Items.Clear();
            combo02.Items.Clear();
            if (tabela.Equals("Anime")) {
                combo00.Items.Add("anime_id");
                combo00.Items.Add("nome");
                combo00.Items.Add("genero");
                combo00.Items.Add("episodios");
                combo00.Items.Add("estado");
                combo00.Items.Add("ano");
                combo00.Items.Add("estudio");
            }
            else if (tabela.Equals("Manga")) {
                combo00.Items.Add("manga_id");
                combo00.Items.Add("nome");
                combo00.Items.Add("tipo");
                combo00.Items.Add("genero");
                combo00.Items.Add("volumes");
                combo00.Items.Add("estado");
                combo00.Items.Add("ano");
                combo00.Items.Add("autor");
                combo00.Items.Add("editora");
            }
            else if (tabela.Equals("Filme")) {
                combo00.Items.Add("filme_id");
                combo00.Items.Add("nome");
                combo00.Items.Add("genero");
                combo00.Items.Add("estado");
                combo00.Items.Add("duracao");
                combo00.Items.Add("ano");
                combo00.Items.Add("estudio");
            }
            else if (tabela.Equals("Estudio")) {
                combo00.Items.Add("estudio_id");
                combo00.Items.Add("nome");
                combo00.Items.Add("CNPJ");
                combo00.Items.Add("endereco");
            }
            else if (tabela.Equals("Editora")) {
                combo00.Items.Add("editora_id");
                combo00.Items.Add("nome");
                combo00.Items.Add("CNPJ");
                combo00.Items.Add("endereco");
            }
            else if (tabela.Equals("Endereco")) {
                combo00.Items.Add("endereco_id");
                combo00.Items.Add("CEP");
                combo00.Items.Add("pais");
                combo00.Items.Add("estado");
                combo00.Items.Add("cidade");
                combo00.Items.Add("bairro");
                combo00.Items.Add("rua");
                combo00.Items.Add("numero");
            }
            else {
                combo00.Items.Add("autor_id");
                combo00.Items.Add("nome");
                combo00.Items.Add("sexo");
                combo00.Items.Add("produto");
            }

            string[] items = new string[combo00.Items.Count];
            for (int i = 0; i < combo00.Items.Count; i++) {
                items[i] = combo00.Items[i].ToString();
            }
            combo02.Items.AddRange(items);

            combo00.Visible = true;
            lb0.Visible = true;
        }
        private void campos_secretos() {
            lb0 = new Label();
            lb0.Text = "Escolha um atributo";
            lb0.Size = new Size(150, 20);
            lb0.Location = new Point(20, 120);
            lb0.ForeColor = Color.Coral;
            lb0.Font = new Font("arial", 11);
            lb0.Visible = false;

            combo00 = new ComboBox();
            combo00.Location = new Point(20, 150);
            combo00.Size = new Size(150, 30);
            combo00.Visible = false;
            combo00.TextChanged += new EventHandler(combo00_select);

            combo03 = new ComboBox();
            combo03.Location = new Point(20, 180);
            combo03.Size = new Size(150, 30);
            combo03.Visible = false;
            combo03.TextChanged += new EventHandler(combo03_select);
            combo03.Items.Add("Média");
            combo03.Items.Add("Somatório");
            combo03.Items.Add("Contagem");

            cb0 = new CheckBox();
            cb0.Text = "Maior";
            cb0.Location = new Point(20, 210);
            cb0.Size = new Size(55, 20);
            cb0.Click += new EventHandler(inicio_click);
            cb0.Visible = false;

            cb1 = new CheckBox();
            cb1.Text = "Menor";
            cb1.Location = new Point(80, 210);
            cb1.Size = new Size(55, 20);
            cb1.Click += new EventHandler(meio_click);
            cb1.Visible = false;

            cb2 = new CheckBox();
            cb2.Text = "Igual";
            cb2.Location = new Point(140, 210);
            cb2.Size = new Size(55, 20);
            cb2.Click += new EventHandler(fim_click);
            cb2.Visible = false;

            tx0 = new TextBox();
            tx0.Location = new Point(20, 240);
            tx0.Size = new Size(150, 20);
            tx0.Visible = false;


            lb3 = new Label();
            lb3.Text = "Ordenação por";
            lb3.Location = new Point(20, 270);
            lb3.Size = new Size(120, 20);
            lb3.Font = new Font("arial", 11);
            lb3.Visible = false;

            combo02 = new ComboBox();
            combo02.Location = new Point(20, 290);
            combo02.Size = new Size(120, 30);
            combo02.TextChanged += new EventHandler(combo02_select);
            combo02.Visible = false;

            rbut0 = new RadioButton();
            rbut0.Text = "Crescente";
            rbut0.Location = new Point(20, 320);
            rbut0.Size = new Size(90, 30);
            rbut0.Font = new Font("arial", 10);
            rbut0.Checked = true;
            rbut0.Visible = false;

            rbut1 = new RadioButton();
            rbut1.Text = "Decrescente";
            rbut1.Location = new Point(20, 350);
            rbut1.Font = new Font("arial", 10);
            rbut1.Size = new Size(100, 30);
            rbut1.Visible = false;

            this.Controls.AddRange(new Control[] { tx0, lb0, lb3, combo00, combo02, combo03, rbut0, rbut1, cb0, cb1, cb2 });

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

            // quando fechar no X tem que fazer algo ou voltar uma tela ou sair do program
        }
        void limpar() {
            tx0.Clear();
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
