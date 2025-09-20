//adicionado para o PDF
using iTextSharp;// biblioteca do pdf
using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.objeto;
using Document = iTextSharp.text.Document;
using Font = System.Drawing.Font;
using PageSize = iTextSharp.text.PageSize;

namespace WindowsFormsApp1
{
    public partial class arquivos : Form
    {
        public arquivos()
        {
            InitializeComponent();
        }

        private void btnPdf_Click(object sender, EventArgs e)
        {
            //criar o arquivo referente ao caminho da pasta
            string destinoPDF = Path.Combine(Application.StartupPath, "pdf");
            //verifica se o diretorio não existir
            if (!Directory.Exists(destinoPDF))
            {
                //criar o diretorio PDF
                Directory.CreateDirectory(destinoPDF);
            }
            //criar o arquivo do relatorio na pasta
            string caminhoPDf = Path.Combine(destinoPDF, "relatorioCliente.pdf");

            //criar o documento PDF
            Document doc = new Document(PageSize.A4);
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(caminhoPDf, FileMode.Create));
                doc.Open();
                //definir a fonte
                Font fonte = new Font("microsoft sans serif", 9f);
                //criar o paragrafo
                Paragraph titulo = new Paragraph("Relatorio Financeiro");
                //alinhamento centro
                titulo.Alignment = Element.ALIGN_CENTER;
                doc.Add(titulo);
                //linha em branco
                doc.Add(new Paragraph(""));
                doc.Add(new Paragraph(""));
                conexao com = new conexao();
                com.getConexao();
                DataTable cliente = new DataTable();

                cliente = com.obterdados("select * from financeiro ");
                //montando a tabela para ler as informações
                PdfPTable table = new PdfPTable(5);
                table.WidthPercentage = 100;
                //define a largura das colunas
                table.SetWidths(new float[] { 40F, 15F, 20f, 25f, 15f });
                //criar o cabeçalho
                table.AddCell(new Phrase("descrição"));
                table.AddCell(new Phrase("valor"));
                table.AddCell(new Phrase("tipo"));
                table.AddCell(new Phrase("data lancamento"));
                table.AddCell(new Phrase("pgo"));
                //listar os ites da tabela do banco
                //varrer todas as linhas da consulta
                for (int i = 0; i < cliente.Rows.Count; i++)
                {

                    //pegando a 1 linha e 2 coluna
                    table.AddCell(new Phrase(cliente.Rows[i][1].ToString()));
                    table.AddCell(new Phrase(cliente.Rows[i][2].ToString()));
                    table.AddCell(new Phrase(cliente.Rows[i][3].ToString()));
                    table.AddCell(new Phrase(cliente.Rows[i][5].ToString()));
                    table.AddCell(new Phrase(cliente.Rows[i][6].ToString()));


                }
                //adiciono as informações na tabela
                doc.Add(table);
                //fecha o documento

                doc.Close();
                MessageBox.Show("Relatorio foi gerado com sucesso");
                Process.Start(caminhoPDf);//abrir o pdf gerado
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gerar o PDF", ex.Message);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            //nome do arquivo que será salvo
            string endereco = "RelatorioExcel.csv";
            //cria o caminho da pasta do relatorio
            string pastadestino = Path.Combine(Application.StartupPath, "Relatorio");
            // verifica se o diretorio existe
            if (!Directory.Exists(pastadestino))
            {
                //cria o diretorio se nao existir
                Directory.CreateDirectory(pastadestino);
            }
            //combinar o arquivo com a pasta
            string caminhodestino = Path.Combine(pastadestino, endereco);

            using (StreamWriter writer = new StreamWriter(caminhodestino, false, Encoding.GetEncoding("iso-8859-15")))
            {
                conexao comexao = new conexao(); //instancio a conexao
                                                 //instancio a nova conexao 
                MySqlConnection con = comexao.getConexao();
                //cabeçalho do excel
                writer.WriteLine("Relatório  Financeiro");
                writer.WriteLine("Data;Valor;Tipo");
                string entrada = @"select data_lancamento,valor,tipo from financeiro";
                MySqlCommand cmd = new MySqlCommand(entrada, con);

                //abro o banco de dados
                con.Open();
                //popular as informações no excel
                using (IDataReader reader = cmd.ExecuteReader())
                {
                    //enquanto tiver linha
                    while (reader.Read())

                    {
                        writer.WriteLine(Convert.ToString(reader["data_lancamento"]) + ";"
                            + Convert.ToString(reader["valor"] + ";"
                           + Convert.ToString(reader["tipo"]))
                            );
                    }
                }
                con.Close();
                MessageBox.Show("Relatorio gerado com sucesso.", "atenção");
            }
        }
    }
}
