using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;

/// <summary>
/// Descrição resumida de ProdutoWs
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
[System.Web.Script.Services.ScriptService]
public class ProdutoWs : System.Web.Services.WebService
{

    public ProdutoWs()
    {

        //Remova os comentários da linha a seguir se usar componentes designados 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    public String carregarDropProdutoFilial(Int32 p_codfilial, Int32 p_tipopedido)
    {
        StringBuilder v_html = new StringBuilder();
        ProdutoDao tipoDao = new ProdutoDao();

        List<Produto> lista = tipoDao.carregarListaProdutoFilial(p_codfilial, p_tipopedido);

        if (lista.Count > 0)
        {
            v_html.Append("<option value='0'>--- Selecione um Produto ---</option>");

            foreach (Produto produto in lista)
            {
                v_html.Append("<option value=" + produto.CodProduto + ">" + produto.Descricao + "</option>");
            }
        }
        else {
            v_html.Append("<option value='0'>Nenhum Produto Encontrado</option>");
        }

        return v_html.ToString();
    }

}
