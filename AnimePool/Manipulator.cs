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
    public class Manipulator : Form {
        private Button but01, but02, but03, but04;
        private DataGridView dgv1;
        private Label lb1, lb2, lb3, lb4, lb5, lb6, lb7, lb8, lb9, lb10, lb11;
        private TextBox tx1, tx2, tx3, tx4, tx5, tx6, tx7, tx8, tx9, tx10, tx11;
        private ComboBox combo01;
        private Program telaAnterior;
        private OdbcConnection conexao = null;
        private OdbcDataAdapter adaptador;
        private OdbcCommand comando;
        private DataSet dados;

        public Manipulator(Program tela) {
            init();
            telaAnterior = tela;
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
            lb1.Text = "Inserção, Alteração e Deleção";
            lb1.Size = new Size(300, 30);
            lb1.Location = new Point(20, 10);
            lb1.ForeColor = Color.DarkBlue;
            lb1.Font = new Font("arial", 15);

            lb2 = new Label();
            lb2.Text = "Escolha uma tabela";
            lb2.Size = new Size(150, 30);
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
            combo01.TextChanged += new EventHandler(combo01_select);

            // data grid  
            dgv1 = new DataGridView();
            dgv1.Name = "datagrid1";
            dgv1.Location = new Point(20, 250);
            dgv1.Size = new Size(750, 150);

            but01 = new Button();
            but01.Text = "Inserir";
            but01.Location = new Point(30, 420);
            but01.Size = new Size(100, 30);
            but01.Click += new EventHandler(button_inserir);

            but02 = new Button();
            but02.Text = "Alterar";
            but02.Location = new Point(150, 420);
            but02.Size = new Size(100, 30);
            but02.Click += new EventHandler(button_alterar);

            but04 = new Button();
            but04.Text = "Deletar";
            but04.Location = new Point(280, 420);
            but04.Size = new Size(100, 30);
            but04.Click += new EventHandler(button_remover);

            but03 = new Button();
            but03.Text = "Voltar";
            but03.Location = new Point(670, 420);
            but03.Size = new Size(100, 30);
            but03.Click += new EventHandler(button3_click);

            this.Controls.AddRange(new Control[] { dgv1, lb1, lb2, but01, but02, but03,but04, combo01 });
        }
        private void combo01_select(object sender, EventArgs e) {
            OpenConn();
            adaptador = new OdbcDataAdapter("SELECT * FROM " + combo01.Text, conexao);
            dados = new DataSet();
            adaptador.Fill(dados,"info");
            dgv1.DataSource = dados;
            dgv1.DataMember = "info";
            
            ativar_campos(combo01.Text);
            CloseConn();  
        }
        private void button_inserir(object sender, EventArgs e) {
            OpenConn();

            if (combo01.Text.Equals("Anime")) {
                comando = new OdbcCommand("INSERT INTO ANIME (anime_id,nome,genero,episodios,estado,ano,estudio) VALUES " +
                "('" + tx3.Text + "', '" + tx4.Text + "', '" + tx5.Text + "', '" + tx6.Text + "', '" + tx7.Text + "','" + tx8.Text + "','" + tx9.Text + "')", conexao);
            }
            else if (combo01.Text.Equals("Manga")) {
                comando = new OdbcCommand("INSERT INTO MANGA (manga_id,nome,tipo,genero,volumes,estado,ano,autor,editora) VALUES " +
                "('" + tx3.Text + "','" + tx4.Text + "','" + tx5.Text + "','" + tx6.Text + "','" + tx7.Text + "','" + tx8.Text + "','" + tx9.Text + "','" + tx10.Text + "','" + tx11.Text + "')", conexao);
            }
            else if (combo01.Text.Equals("Filme")) {
                comando = new OdbcCommand("INSERT INTO FILME (filme_id,nome,genero,estado,duracao,ano,estudio) VALUES " +
                "('" + tx3.Text + "','" + tx4.Text + "','" + tx5.Text + "','" + tx6.Text + "','" + tx7.Text + "','" + tx8.Text + "','" + tx9.Text + "')", conexao);
            }
            else if (combo01.Text.Equals("Estudio")) {
                comando = new OdbcCommand("INSERT INTO ESTUDIO (estudio_id,nome,CNPJ,endereco) VALUES " +
                "('" + tx3.Text + "','" + tx4.Text + "','" + tx5.Text + "','" + tx6.Text + "')", conexao);
            }
            else if (combo01.Text.Equals("Editora")) {
                comando = new OdbcCommand("INSERT INTO EDITORA (editora_id,nome,CNPJ,endereco) VALUES " +
                "('" + tx3.Text + "','" + tx4.Text + "','" + tx5.Text + "','" + tx6.Text + "')", conexao);
            }
            else if (combo01.Text.Equals("Endereco")) {
                comando = new OdbcCommand("INSERT INTO ENDERECO (endereco_id,cep,pais,estado,cidade,bairro,rua,numero) VALUES " +
                "('" + tx3.Text + "','" + tx4.Text + "','" + tx5.Text + "','" + tx6.Text + "','" + tx7.Text + "','" + tx8.Text + "','" + tx9.Text + "','" + tx10.Text + "')", conexao);
            }
            else {
                comando = new OdbcCommand("INSERT INTO AUTOR (autor_id,nome,sexo,produto) VALUES " +
                "('" + tx3.Text + "','" + tx4.Text + "','" + tx5.Text + "','" + tx6.Text + "')", conexao);
            }

            comando.ExecuteNonQuery();
            CloseConn();
            limpar();
            refresh_();
        }
        private void button_alterar(object sender, EventArgs e) {
            OpenConn();
            comando = conexao.CreateCommand();
            if (combo01.Text.Equals("Anime")) {
                comando.CommandText = "UPDATE ANIME SET nome='"
                + tx4.Text + "',genero= '" + tx5.Text + "',episodios= '" + tx6.Text + "',estado='" + tx7.Text + "',ano='" + tx8.Text +
                "',estudio='" + tx9.Text + "' WHERE anime_id='" + tx3.Text + "'";
            }
            else if (combo01.Text.Equals("Manga")) {
                comando.CommandText = "UPDATE MANGA SET nome='"
                + tx4.Text + "',tipo= '" + tx5.Text + "',genero= '" + tx6.Text + "',volumes='" + tx7.Text + "',estado='" + tx8.Text +
                "',ano='" + tx9.Text + "',autor='" + tx10.Text + "',editora='"+tx11.Text+"' WHERE manga_id='" + tx3.Text + "'";
            }
            else if (combo01.Text.Equals("Filme")) {
                comando.CommandText = "UPDATE FILME SET nome='"
               + tx4.Text + "',genero= '" + tx5.Text + "',estado= '" + tx6.Text + "',duracao='" + tx7.Text + "',ano='" + tx8.Text +
               "',estudio='" + tx9.Text + "' WHERE filme_id='" + tx3.Text + "'";
            }
            else if (combo01.Text.Equals("Estudio")) {
                comando.CommandText = "UPDATE ESTUDIO SET nome='"
                + tx4.Text + "',CNPJ='" + tx5.Text + "',endereco='" + tx6.Text +
                "' WHERE estudio_id ='" + tx3.Text + "'";
            }
            else if (combo01.Text.Equals("Editora")) {
                comando.CommandText = "UPDATE EDITORA SET nome='"
                + tx4.Text + "',CNPJ='" + tx5.Text + "',endereco='" + tx6.Text +
                "' WHERE editora_id ='" + tx3.Text + "'";
            }
            else if (combo01.Text.Equals("Endereco")) {
                comando.CommandText = "UPDATE ENDERECO SET cep='"
                + tx4.Text + "',pais= '" + tx5.Text + "',estado= '" + tx6.Text + "',cidade='" + tx7.Text + "',bairro='" + tx8.Text +
                "',rua='" + tx9.Text + "',numero='" + tx10.Text + "' WHERE endereco_id='" + tx3.Text + "'";
            }
            else {
                comando.CommandText = "UPDATE AUTOR SET nome='"
                + tx4.Text + "',sexo= '" + tx5.Text + "',produto = '" + tx6.Text +
                "' WHERE autor_id ='" + tx3.Text + "'";
            }
            comando.ExecuteNonQuery();
            CloseConn();
            limpar();
            refresh_();
        }
        private void button_remover(object sender, EventArgs e) {
            OpenConn();
            comando = conexao.CreateCommand();

            if (combo01.Text.Equals("Anime")) 
                comando.CommandText = "DELETE FROM ANIME WHERE anime_id = '" + tx3.Text + "'"; 
            else if (combo01.Text.Equals("Manga")) 
                comando.CommandText = "DELETE FROM MANGA WHERE manga_id = '" + tx3.Text + "'"; 
            else if (combo01.Text.Equals("Filme")) 
                comando.CommandText = "DELETE FROM FILME WHERE filme_id = '" + tx3.Text + "'"; 
            else if (combo01.Text.Equals("Estudio"))
                comando.CommandText = "DELETE FROM ESTUDIO WHERE estudio_id = '" + tx3.Text + "'"; 
            else if (combo01.Text.Equals("Editora"))
                comando.CommandText = "DELETE FROM EDITORA WHERE editora_id = '" + tx3.Text + "'";
            else if (combo01.Text.Equals("Endereco")) 
                comando.CommandText = "DELETE FROM ENDERECO WHERE endereco_id = '" + tx3.Text + "'"; 
            else 
                comando.CommandText = "DELETE FROM AUTOR WHERE autor_id = '" + tx3.Text + "'";

            comando.ExecuteNonQuery();
            CloseConn();
            limpar();
            refresh_();
        }
        private void ativar_campos(String tabela) {
            if (tabela.Equals("Anime")) {
                lb3.Text = "Anime ID";
                lb3.Visible = true;
                tx3.Visible = true;
                //
                lb4.Text = "Nome";
                lb4.Visible = true;
                tx4.Visible = true;
                //
                lb5.Text = "Gênero";
                lb5.Visible = true;
                tx5.Visible = true;
                //
                lb6.Text = "Nº de episódios";
                lb6.Visible = true;
                tx6.Visible = true;
                //
                lb7.Text = "Estado";
                lb7.Visible = true;
                tx7.Visible = true;
                //
                lb8.Text = "Ano";
                lb8.Visible = true;
                tx8.Visible = true;
                //
                lb9.Text = "Estúdio";
                lb9.Visible = true;
                tx9.Visible = true;
                //
                lb10.Visible = false;
                tx10.Visible = false;
                //
                lb11.Visible = false;
                tx11.Visible = false;
            }
            else if (tabela.Equals("Manga")) {
                lb3.Text = "Mangá ID";
                lb3.Visible = true;
                tx3.Visible = true;
                //
                lb4.Text = "Nome";
                lb4.Visible = true;
                tx4.Visible = true;
                //
                lb5.Text = "Tipo";
                lb5.Visible = true;
                tx5.Visible = true;
                //
                lb6.Text = "Gênero";
                lb6.Visible = true;
                tx6.Visible = true;
                //
                lb7.Text = "Nº de volumes";
                lb7.Visible = true;
                tx7.Visible = true;
                //
                lb8.Text = "Estado";
                lb8.Visible = true;
                tx8.Visible = true;
                //
                lb9.Text = "Ano";
                lb9.Visible = true;
                tx9.Visible = true;
                //
                lb10.Text = "Autor";
                lb10.Visible = true;
                tx10.Visible = true;
                //
                lb11.Text = "Editora";
                lb11.Visible = true;
                tx11.Visible = true;
            }
            else if (tabela.Equals("Filme")) {
                lb3.Text = "Filme ID";
                lb3.Visible = true;
                tx3.Visible = true;
                //
                lb4.Text = "Nome";
                lb4.Visible = true;
                tx4.Visible = true;
                //
                lb5.Text = "Gênero";
                lb5.Visible = true;
                tx5.Visible = true;
                //
                lb6.Text = "Estado";
                lb6.Visible = true;
                tx6.Visible = true;
                //
                lb7.Text = "Duração";
                lb7.Visible = true;
                tx7.Visible = true;
                //
                lb8.Text = "Ano";
                lb8.Visible = true;
                tx8.Visible = true;
                //
                lb9.Text = "Estúdio";
                lb9.Visible = true;
                tx9.Visible = true;
                //
                lb10.Visible = false;
                tx10.Visible = false;
                //
                lb11.Visible = false;
                tx11.Visible = false;
            }
            else if (tabela.Equals("Estudio")) {
                lb3.Text = "Estudio ID";
                lb3.Visible = true;
                tx3.Visible = true;
                //
                lb4.Text = "Nome";
                lb4.Visible = true;
                tx4.Visible = true;
                //
                lb5.Text = "CNPJ";
                lb5.Visible = true;
                tx5.Visible = true;
                //
                lb6.Text = "Endereço ID";
                lb6.Visible = true;
                tx6.Visible = true;
                //
                lb7.Visible = false;
                tx7.Visible = false;
                //
                lb8.Visible = false;
                tx8.Visible = false;
                //
                lb9.Visible = false;
                tx9.Visible = false;
                //
                lb10.Visible = false;
                tx10.Visible = false;
                //
                lb11.Visible = false;
                tx11.Visible = false;
            }
            else if (tabela.Equals("Editora")) {
                lb3.Text = "Editora ID";
                lb3.Visible = true;
                tx3.Visible = true;
                //
                lb4.Text = "Nome";
                lb4.Visible = true;
                tx4.Visible = true;
                //
                lb5.Text = "CNPJ";
                lb5.Visible = true;
                tx5.Visible = true;
                //
                lb6.Text = "Endereço ID";
                lb6.Visible = true;
                tx6.Visible = true;
                //
                lb7.Visible = false;
                tx7.Visible = false;
                //
                lb8.Visible = false;
                tx8.Visible = false;
                //
                lb9.Visible = false;
                tx9.Visible = false;
                //
                lb10.Visible = false;
                tx10.Visible = false;
                //
                lb11.Visible = false;
                tx11.Visible = false;
            }
            else if (tabela.Equals("Autor")) {
                lb3.Text = "Autor ID";
                lb3.Visible = true;
                tx3.Visible = true;
                //
                lb4.Text = "Nome";
                lb4.Visible = true;
                tx4.Visible = true;
                //
                lb5.Text = "Sexo";
                lb5.Visible = true;
                tx5.Visible = true;
                //
                lb6.Text = "Produto";
                lb6.Visible = true;
                tx6.Visible = true;
                //
                lb7.Visible = false;
                tx7.Visible = false;
                //
                lb8.Visible = false;
                tx8.Visible = false;
                //
                lb9.Visible = false;
                tx9.Visible = false;
                //
                lb10.Visible = false;
                tx10.Visible = false;
                //
                lb11.Visible = false;
                tx11.Visible = false;
            }
            else {
                lb3.Text = "Endereço ID";
                lb3.Visible = true;
                tx3.Visible = true;
                //
                lb4.Text = "CEP";
                lb4.Visible = true;
                tx4.Visible = true;
                //
                lb5.Text = "País";
                lb5.Visible = true;
                tx5.Visible = true;
                //
                lb6.Text = "Estado";
                lb6.Visible = true;
                tx6.Visible = true;
                //
                lb7.Text = "Cidade";
                lb7.Visible = true;
                tx7.Visible = true;
                //
                lb8.Text = "Bairro";
                lb8.Visible = true;
                tx8.Visible = true;
                //
                lb9.Text = "Rua";
                lb9.Visible = true;
                tx9.Visible = true;
                //
                lb10.Text = "Número";
                lb10.Visible = true;
                tx10.Visible = true;
                //
                lb11.Visible = false;
                tx11.Visible = false;
            }

        }
        private void campos_secretos() {
            lb3 = new Label();
            lb3.Text = "NULL";
            lb3.Location = new Point(220, 50);
            lb3.Size = new Size(150, 30);
            lb3.Font = new Font("arial", 11);
            lb3.Visible = false;

            tx3 = new TextBox();
            tx3.Location = new Point(220, 80);
            tx3.Size = new Size(150, 30);
            tx3.Visible = false;
            ///////////////////////////////////
            lb4 = new Label();
            lb4.Text = "NULL";
            lb4.Location = new Point(420, 50);
            lb4.Size = new Size(150, 30);
            lb4.Font = new Font("arial", 11);
            lb4.Visible = false;

            tx4 = new TextBox();
            tx4.Location = new Point(420, 80);
            tx4.Size = new Size(150, 30);
            tx4.Visible = false;
            //////////////////////////////////
            lb5 = new Label();
            lb5.Text = "NULL";
            lb5.Location = new Point(620, 50);
            lb5.Size = new Size(150, 30);
            lb5.Font = new Font("arial", 11);
            lb5.Visible = false;

            tx5 = new TextBox();
            tx5.Location = new Point(620, 80);
            tx5.Size = new Size(150, 30);
            tx5.Visible = false;
            //////////////////////////////////
            lb6 = new Label();
            lb6.Text = "NULL";
            lb6.Location = new Point(220, 120);
            lb6.Size = new Size(150, 30);
            lb6.Font = new Font("arial", 11);
            lb6.Visible = false;

            tx6 = new TextBox();
            tx6.Location = new Point(220, 150);
            tx6.Size = new Size(150, 30);
            tx6.Visible = false;
            //////////////////////////////////
            lb7 = new Label();
            lb7.Text = "NULL";
            lb7.Location = new Point(420, 120);
            lb7.Size = new Size(150, 30);
            lb7.Font = new Font("arial", 11);
            lb7.Visible = false;

            tx7 = new TextBox();
            tx7.Location = new Point(420, 150);
            tx7.Size = new Size(150, 30);
            tx7.Visible = false;
            //////////////////////////////////
            lb8 = new Label();
            lb8.Text = "NULL";
            lb8.Location = new Point(620, 120);
            lb8.Size = new Size(150, 30);
            lb8.Font = new Font("arial", 11);
            lb8.Visible = false;

            tx8 = new TextBox();
            tx8.Location = new Point(620, 150);
            tx8.Size = new Size(150, 30);
            tx8.Visible = false;
            /////////////////////////////////
            lb9 = new Label();
            lb9.Text = "NULL";
            lb9.Location = new Point(220, 190);
            lb9.Size = new Size(150, 20);
            lb9.Font = new Font("arial", 11);
            lb9.Visible = false;

            tx9 = new TextBox();
            tx9.Location = new Point(220, 210);
            tx9.Size = new Size(150, 30);
            tx9.Visible = false;
            ////////////////////////////////
            lb10 = new Label();
            lb10.Text = "NULL";
            lb10.Location = new Point(420, 190);
            lb10.Size = new Size(150, 20);
            lb10.Font = new Font("arial", 11);
            lb10.Visible = false;

            tx10 = new TextBox();
            tx10.Location = new Point(420, 210);
            tx10.Size = new Size(150, 30);
            tx10.Visible = false;
            ////////////////////////////////////
            lb11 = new Label();
            lb11.Text = "NULL";
            lb11.Location = new Point(620, 190);
            lb11.Size = new Size(150, 20);
            lb11.Font = new Font("arial", 11);
            lb11.Visible = false;

            tx11 = new TextBox();
            tx11.Location = new Point(620, 210);
            tx11.Size = new Size(150, 30);
            tx11.Visible = false;

            this.Controls.AddRange(new Control[] { lb3, tx3, lb4, tx4, lb5, tx5, lb6, tx6, lb7, tx7, lb8, tx8, lb9, tx9, lb10, tx10, lb11, tx11 });

        }
        private void button3_click(object sender, EventArgs e) {
            telaAnterior.Visible = true;
            this.Close();
            // quando fechar no X tem que fazer algo ou voltar uma tela ou sair do program
        }
        void limpar() {
            tx3.Clear(); tx4.Clear();  tx5.Clear();
            tx6.Clear(); tx7.Clear();  tx8.Clear();
            tx9.Clear(); tx10.Clear(); tx11.Clear();
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
