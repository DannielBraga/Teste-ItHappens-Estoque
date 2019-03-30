using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;

/// <summary>
/// Descrição resumida de TipoPedidoEstoqueWs
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
[System.Web.Script.Services.ScriptService]
public class TipoPedidoEstoqueWs : System.Web.Services.WebService
{

    public TipoPedidoEstoqueWs()
    {

        //Remova os comentários da linha a seguir se usar componentes designados 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    public String carregarDropTipoPedido()
    {
        StringBuilder v_html = new StringBuilder();
        TipoPedidoEstoqueDao tipoDao = new TipoPedidoEstoqueDao();

        List<TipoPedidoEstoque> lista = tipoDao.obterListaTipoPedidoEstoque();

        v_html.Append("<option value='0'>--- Selecione um Tipo ---</option>");

        foreach (TipoPedidoEstoque tipo in lista)
        {
            v_html.Append("<option value="+ tipo.CodTipoPedido +">"+ tipo.Descricao +"</option>");
        }

        return v_html.ToString();
    }


 }
