using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.objeto
{
    internal class financeiro
    {
        public int id;
        public string descricao;
        public decimal valor;
        public string tipo;
        public string servico;
        public DateTime data_lancamento;
        public Boolean pgto;
        public bool cadastrar(conexao conexao)
        {
            //valor falso
            bool resultado = false;
            string sql = "insert into financeiro (descricao, valor, tipo, servico, data_lancamento, pgto) values (@descricao, @valor, @tipo, @servico, @data_lancamento, @pgto)";
            string[] campos = { "@descricao", "@valor", "@tipo", "@servico", "@data_lancamento", "@pgto" };
            object[] valores = { descricao, valor, tipo, servico, data_lancamento, pgto };
            if(conexao.cadastrar(campos, valores, sql) > 1)
            {
                resultado = true;
            }
            return resultado;
        }
        public bool editar(conexao conexao)
        {
            //valor falso
            bool resultado = false;
            string sql = "update financeiro set descricao=@descricao, valor=@valor, tipo=@tipo, servico=@servico, data_lancamento=@data_lancamento, pgto=@pgto where id=@id";
            string[] campos = { "@descricao", "@valor", "@tipo", "@servico", "@data_lancamento", "@pgto", "@id" };
            object[] valores = { descricao, valor, tipo, servico, data_lancamento, pgto, id };
            if (conexao.cadastrar(campos, valores, sql) > 1)
            {
                resultado = true;
            }
            return resultado;
        }

        public bool Excluir(conexao com)
        {
            bool resultado = false;
            string sql = " delete from financeiro where cod_financeiro=@codigo";
            string[] campos = { "@codigo" };
            object[] valores = { id };
            if (com.cadastrar(campos, valores, sql) >= 1)
            {
                resultado = true;
            }


            return resultado;
        }

    }
}
