using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.objeto;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btncadastrar_Click(object sender, EventArgs e)
        {
            conexao con = new conexao();
            con.getConexao();
            //chamando o objeto financeiro
            financeiro fin = new financeiro();
            //populando as informações
            fin.data_lancamento = Data_lancamento.Value;
            fin.descricao = txtDescricao.Text;
            fin.servico = cboServico.Text;
            fin.valor = decimal.Parse(txtValor.Text);
            fin.tipo = cboTipo.Text;
            fin.pgto = chkpagamento.Checked;
            if (fin.cadastrar(con) == true)
            {
                MessageBox.Show("Cadastrado com sucesso");
                dataGridView1.Refresh();// atualiza o grid
            }
            AtualizarTabela();
        }

        private void btneditar_Click(object sender, EventArgs e)
        {
            //chamo o metodo da conexao
            conexao com = new conexao();
            com.getConexao();
            // chama o objeto do financeiro
            financeiro financeiro = new financeiro();
            financeiro.id = Convert.ToInt32(txtCodigo.Text);
            financeiro.descricao = txtDescricao.Text;
            financeiro.servico = cboServico.Text;
            financeiro.tipo = cboTipo.Text;
            financeiro.valor = decimal.Parse(txtValor.Text);
            financeiro.pgto = chkpagamento.Checked;
            financeiro.data_lancamento = Data_lancamento.Value;
            if (financeiro.editar(com) == true)
            {
                MessageBox.Show("Editado com sucesso!");
            }
            AtualizarTabela();
        }

        private void btnexcluir_Click(object sender, EventArgs e)
        {
            //chamo o metodo da conexao
            conexao com = new conexao();
            com.getConexao();
            // chama o objeto do financeiro
            financeiro financeiro = new financeiro();
            financeiro.id = Convert.ToInt32(txtCodigo.Text);
            if (financeiro.Excluir(com) == true)
            {
                MessageBox.Show("Excluido com sucesso");
                dataGridView1.Refresh();
            }

            AtualizarTabela();

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //chamo o objeto da conexao
            conexao com = new conexao();
            com.getConexao();
            dataGridView1.DataSource = com.obterdados("select * from financeiro");
            cboServico.Items.Add("Salário");
            cboServico.Items.Add("despesas");
            cboTipo.Items.Add("Entrada");
            cboTipo.Items.Add("Saída");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnpesquisar_Click(object sender, EventArgs e)
        {
            conexao com = new conexao();
            com.getConexao();
            if (string.IsNullOrEmpty(txtPesquisar.Text))
            {
                dataGridView1.DataSource = com.obterdados("select * from financeiro");
            }
            else
            {
                dataGridView1.DataSource = com.obterdados("select * from financeiro where descricao like '%" + txtPesquisar.Text + "%' or data_lancamento like '%" + txtPesquisar.Text + "%'");
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            arquivos arquivos = new arquivos();
            arquivos.ShowDialog();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int codigo = 0;
            codigo = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            txtCodigo.Text = codigo.ToString();
            txtDescricao.Text = dataGridView1.Rows[e.RowIndex].Cells["descricao"].Value.ToString();
            txtValor.Text = dataGridView1.Rows[e.RowIndex].Cells["valor"].Value.ToString();
            cboServico.Text = dataGridView1.Rows[e.RowIndex].Cells["servico"].Value.ToString();
            cboTipo.Text = dataGridView1.Rows[e.RowIndex].Cells["tipo"].Value.ToString();
            //convertendo o data implicitamente
            Data_lancamento.Value = (DateTime)dataGridView1.Rows[e.RowIndex].Cells["data_lancamento"].Value;
            bool pago = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells["pgto"].Value.ToString());
            if (pago == true)
            {
                chkpagamento.Checked = true;
            }
            else
            {
                chkpagamento.Checked = false;
            }

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AtualizarTabela()
        {
            conexao com = new conexao();
            com.getConexao();

            string sql = "SELECT cod_financeiro, descricao, valor FROM financeiro";
            MySqlCommand cmd = new MySqlCommand(sql, com.getConexao());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
            com.getConexao().Close();
        }
    }
}
