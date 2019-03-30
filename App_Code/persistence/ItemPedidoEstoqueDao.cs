using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de ItemPedidoEstoqueDao
/// </summary>
public class ItemPedidoEstoqueDao
{
    string V_STRCONN = Conexao.getStrConexao("CONN_ESTOQUE");

    public ItemPedidoEstoqueDao()
    {
        //
        // TODO: Adicionar lógica do construtor aqui
        //
    }

    public void incluirItemPedidoEstoque(ItemPedidoEstoque item)
    {
        string v_sql = " insert into itempedidoestoque (codproduto, quantidade) values (@codproduto, @quantidade); ";

        List<Produto> lista = new List<Produto>();

        Dao.getDataTable(v_sql, V_STRCONN,
            new SqlParameter("@codproduto", item.CodProduto),
            new SqlParameter("@quantidade", item.Quantidade));
    }


}