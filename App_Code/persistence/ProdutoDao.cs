using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de ProdutoDao
/// </summary>
public class ProdutoDao
{
    string V_STRCONN = Conexao.getStrConexao("CONN_ESTOQUE");

    public ProdutoDao()
    {
        //
        // TODO: Adicionar lógica do construtor aqui
        //
    }

    public List<Produto> carregarListaProdutoFilial(Int32 p_codfilial, Int32 p_tipopedido)
    {
        string v_sql = " exec sp_obter_lista_produtos_filial_tipopedido @p_codfilial, @p_tipopedido ";

        List<Produto> lista = new List<Produto>();

        using (DataTable dt = Dao.getDataTable(v_sql, V_STRCONN, new SqlParameter("@p_codfilial", p_codfilial),
            new SqlParameter("@p_tipopedido", p_tipopedido)))
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Produto produto = new Produto();
                    produto.CodProduto = Int32.Parse(dt.Rows[i]["codproduto"].ToString());
                    produto.Descricao = dt.Rows[i]["descricao"].ToString().Trim().ToUpper();

                    lista.Add(produto);
                }
            }
        }

        return lista;
    }
}