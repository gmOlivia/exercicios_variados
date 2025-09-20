using MySql.Data.MySqlClient;
using Mysqlx.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.objeto
{
    internal class conexao
    {
        //variaveis de conexao
        static private string servidor = "localhost";
        static private string banco = "gerenciamento";
        static private string usuario = "root";
        static private string senha = "";

        //variavel conexao mysql
        public MySqlConnection conexaoProjeto = null;

        //string com paremetros do banco
        static private string conexaobd = "server=" + servidor + ";database=" + banco + ";user id=" + usuario + ";password=" + senha;

        //metodo de conexao cm o banco
        public MySqlConnection getConexao()
        {
            conexaoProjeto = new MySqlConnection(conexaobd);
            return conexaoProjeto;
            
        }
        public DataTable obterdados(string sql)
        {
            //criando tabela
            DataTable dt = new DataTable();
            //abrindo conexao
            conexaoProjeto.Open();
            //criando comando sql
            MySqlCommand cmd = new MySqlCommand(sql, conexaoProjeto);
            //monta consulta dos dados 
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);
            //fechando conexao
            conexaoProjeto.Close();
            return dt;
        }
        public int cadastrar(string[] campos, object[] valores, string sql)
        {
            int registro;
            try
            {
                conexaoProjeto.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conexaoProjeto);
                for (int i = 0; i < valores.Length; i++)
                {
                    cmd.Parameters.AddWithValue(campos[i], valores[i]);
                }
                registro = cmd.ExecuteNonQuery();
                conexaoProjeto.Close();
                return registro;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
