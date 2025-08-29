using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Generators;

namespace WindowsFormsApp1
{
    public partial class cadastro : Form
    {
        MySqlConnection conexao;
        int usuarioSelecionado = -1;
        public cadastro()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void CarregarUsuarios()
        {
            try
            {
                string data_source = "datasource=localhost;username=root;password='';database=exe";
                using (MySqlConnection conexao = new MySqlConnection(data_source))
                {
                    string sql = "SELECT * FROM usuario";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conexao);
                    DataTable tabela = new DataTable();
                    adapter.Fill(tabela);
                    DGV.DataSource = tabela;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
            }
        }

        private void btnCadastro_Click(object sender, EventArgs e)
        {
            try
            {
                /// <summary>
                /// data_source é o caminho do banco de dados
                /// </summary>
                string data_source = "datasource=localhost;username=root;password='';database=exe";
                conexao = new MySqlConnection(data_source);
                string sql = "insert into usuario(nome,email,ativo, CPF, fone) values (@nome,@email, 1, @CPF, @fone)";
                MySqlCommand command = new MySqlCommand(sql, conexao);
                command.Parameters.AddWithValue("@nome", txtNome.Text);
                command.Parameters.AddWithValue("@email", txtEmail.Text);
                command.Parameters.AddWithValue("@fone", txtFone.Text);
                command.Parameters.AddWithValue("@CPF", txtCPF.Text);
                conexao.Open();
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Cadastro realizado com sucesso");
                    CarregarUsuarios();
                }
                else
                {
                    MessageBox.Show("Erro no cadastro");
                }
                conexao.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
            }

        }

        private void cadastro_Load(object sender, EventArgs e)
        {
            CarregarUsuarios();
            ConfigurarDataGridView();

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (usuarioSelecionado == -1)
            {
                MessageBox.Show("Selecione um funcionário antes de editar.");
                return;
            }
            string data_source = "datasource=localhost;username=root;password='';database=exe";
            using (MySqlConnection conexao = new MySqlConnection(data_source))
            {
                try
                {
                    conexao.Open();
                    string query = "UPDATE usuario SET nome = @nome, fone = @fone, email = @email, CPF = @CPF WHERE codigo = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conexao);
                    cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@fone", txtFone.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@CPF", txtCPF.Text);
                    cmd.Parameters.AddWithValue("@id", usuarioSelecionado);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Funcionário atualizado com sucesso!");
                    CarregarUsuarios();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao editar funcionário: " + ex.Message);
                }
            }
        }

        private void DGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = DGV.Rows[e.RowIndex];
                usuarioSelecionado = Convert.ToInt32(row.Cells["codigouser"].Value);

                txtNome.Text = row.Cells["nome"].Value.ToString();
                txtFone.Text = row.Cells["fone"].Value.ToString();
                txtEmail.Text = row.Cells["email"].Value.ToString();
                txtCPF.Text = row.Cells["CPF"].Value.ToString();
                
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Deseja realmente excluir este ususario?", "Confirmação", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    conexao.Open();
                    string query = "DELETE FROM usuario WHERE codigo = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conexao);
                    cmd.Parameters.AddWithValue("@id", usuarioSelecionado);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Usuário excluído com sucesso!");

                    CarregarUsuarios(); //Recarrega a tabela

                    btnLimpar_Click(sender, e); //limpa os campos do formulário
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao excluir cliente: " + ex.Message);
                }
                finally
                {
                    conexao.Close();
                }
            }
        }
        private void ConfigurarDataGridView()
        {
            DGV.AutoGenerateColumns = false;
            DGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGV.MultiSelect = false;
            DGV.AllowUserToAddRows = false;
            DGV.AllowUserToDeleteRows = false;
            DGV.ReadOnly = false;

           DGV.Columns.Clear();

            DGV.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "codigo",
                HeaderText = "Código",
                Name = "codigouser",
                ReadOnly = true
            });

            DGV.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "nome",
                HeaderText = "Nome",
                Name = "nome"
            });

            DGV.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "email",
                HeaderText = "Email",
                Name = "Email"
            });

            DGV.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CPF",
                HeaderText = "CPF",
                Name = "cpf"
            });

            DGV.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "fone",
                HeaderText = "Telefone",
                Name = "fone"
            });
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtNome.Clear();
            txtFone.Clear();
            txtEmail.Clear();
            txtCPF.Clear();
            usuarioSelecionado = -1;
        }
    }
    }

