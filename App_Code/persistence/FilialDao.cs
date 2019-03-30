using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de FilialDao
/// </summary>
public class FilialDao
{
    string V_STRCONN = Conexao.getStrConexao("CONN_ESTOQUE");

    public FilialDao()
    {
        //
        // TODO: Adicionar lógica do construtor aqui
        //
    }

    public List<Filial> obterListaFilialEstoque()
    {
        string v_sql = " select fil.* from estoque est inner join filial fil on est.codfilial = fil.codfilial ";

        List<Filial> lista = new List<Filial>();

        using (DataTable dt = Dao.getDataTable(v_sql, V_STRCONN))
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Filial filial = new Filial();
                    filial.CodFilial = Int32.Parse(dt.Rows[i]["codfilial"].ToString());
                    filial.Descricao = dt.Rows[i]["descricao"].ToString().Trim().ToUpper();

                    lista.Add(filial);
                }
            }
        }

        return lista;
    }
}