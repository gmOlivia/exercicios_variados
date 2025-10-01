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
    public partial class forms2909 : Form
    {
        MySqlConnection conexao;
        public forms2909()
        {
            InitializeComponent();
        }

        string connectionString = "Server=localhost;Database=forms2909;Uid=root;Pwd=senha'';";
        private void AtualizarGrid()
        {
            using (var conexao = new MySqlConnection(connectionString))
            {
                conexao.Open();
                string query = "SELECT v.modelo, v.cor, v.placa, vg.numero, vg.stats FROM veiculo v JOIN vagas vg ON vg.horario IS NOT NULL";
                var adapter = new MySqlDataAdapter(query, conexao);
                var table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }
        }
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
        private void forms2909_Load(object sender, EventArgs e)
        {
            //chama itens pra cmb
            cmbVaga.Items.Add("1");
            cmbVaga.Items.Add("2");
            cmbVaga.Items.Add("3");
            cmbVaga.Items.Add("4");
            cmbVaga.Items.Add("5");
            cmbVaga.Items.Add("6");
            cmbVaga.Items.Add("7");
            cmbVaga.Items.Add("8");
            cmbVaga.Items.Add("9");
            cmbVaga.Items.Add("10");
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            var carro = new veiculo
            {
                modelo = txtModelo.Text,
                cor = txtCor.Text,
                placa = txtPlaca.Text,
            };

            var vega = new vaga
            {
                numero = Convert.ToInt32(cmbVaga.SelectedItem),
                horario = dtpHorario.Value,
                status = "OCUPADA",
            };

            using (var conexao = new MySqlConnection(connectionString))
            {
                conexao.Open();

                string insertVeiculo = "INSERT INTO veiculo (modelo, cor, placa) VALUES (@modelo, @cor, @placa)";
                var cmdVeiculo = new MySqlCommand(insertVeiculo, conexao);
                cmdVeiculo.Parameters.AddWithValue("@modelo", carro.modelo);
                cmdVeiculo.Parameters.AddWithValue("@cor", carro.cor);
                cmdVeiculo.Parameters.AddWithValue("@placa", carro.placa);
                cmdVeiculo.ExecuteNonQuery();

                string updateVaga = "UPDATE vagas SET horario = @horario, stats = @status WHERE numero = @numero";
                var cmdVaga = new MySqlCommand(updateVaga, conexao);
                cmdVaga.Parameters.AddWithValue("@horario", vega.horario);
                cmdVaga.Parameters.AddWithValue("@status", vega.status);
                cmdVaga.Parameters.AddWithValue("@numero", vega.numero);
                cmdVaga.ExecuteNonQuery();
            }

            MessageBox.Show("Veículo registrado e vaga ocupada!");
            AtualizarGrid();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            string placa = txtPlaca.Text;
            int numeroVaga = Convert.ToInt32(cmbVaga.SelectedItem);

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string deleteVeiculo = "DELETE FROM veiculo WHERE placa = @placa";
                var cmdVeiculo = new MySqlCommand(deleteVeiculo, conn);
                cmdVeiculo.Parameters.AddWithValue("@placa", placa);
                cmdVeiculo.ExecuteNonQuery();

                string updateVaga = "UPDATE vagas SET horario = NULL, stats = 'LIVRE' WHERE numero = @numero";
                var cmdVaga = new MySqlCommand(updateVaga, conn);
                cmdVaga.Parameters.AddWithValue("@numero", numeroVaga);
                cmdVaga.ExecuteNonQuery();
            }

            MessageBox.Show("Veículo excluído e vaga liberada.");
            AtualizarGrid();
        }

        

     
    }
}
