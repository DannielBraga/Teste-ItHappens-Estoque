using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de TipoPedidoEstoqueDao
/// </summary>
public class TipoPedidoEstoqueDao
{
    string V_STRCONN = Conexao.getStrConexao("CONN_ESTOQUE");

    public TipoPedidoEstoqueDao()
    {
        //
        // TODO: Adicionar lógica do construtor aqui
        //
    }

    public List<TipoPedidoEstoque> obterListaTipoPedidoEstoque()
    {
        string v_sql = " select * from tipopedidoestoque ";

        List<TipoPedidoEstoque> lista = new List<TipoPedidoEstoque>();

        using (DataTable dt = Dao.getDataTable(v_sql, V_STRCONN))
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TipoPedidoEstoque tipo = new TipoPedidoEstoque();
                    tipo.CodTipoPedido = Int32.Parse(dt.Rows[i]["codtipopedido"].ToString());
                    tipo.Descricao = dt.Rows[i]["descricao"].ToString().Trim().ToUpper();
                   
                    lista.Add(tipo);
                }
            }
        }

        return lista;
    }

}