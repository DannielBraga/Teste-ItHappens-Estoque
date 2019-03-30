using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;

/// <summary>
/// Descrição resumida de EstoqueWs
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
[System.Web.Script.Services.ScriptService]
public class EstoqueWs : System.Web.Services.WebService
{

    public EstoqueWs()
    {

        //Remova os comentários da linha a seguir se usar componentes designados 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    public String carregarDropEstoqueFilial()
    {
        StringBuilder v_html = new StringBuilder();
        FilialDao filialDao = new FilialDao();

        List<Filial> lista = filialDao.obterListaFilialEstoque();

        v_html.Append("<option value='0'>--- Selecione uma Filial do Estoque ---</option>");

        foreach (Filial filial in lista)
        {
            v_html.Append("<option value=" + filial.CodFilial + ">" + filial.Descricao + "</option>");
        }

        return v_html.ToString();
    }


}
