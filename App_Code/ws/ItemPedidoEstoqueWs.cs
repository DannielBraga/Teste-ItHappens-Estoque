using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;

/// <summary>
/// Descrição resumida de ItemPedidoEstoqueWs
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
[System.Web.Script.Services.ScriptService]
public class ItemPedidoEstoqueWs : System.Web.Services.WebService
{

    public ItemPedidoEstoqueWs()
    {

        //Remova os comentários da linha a seguir se usar componentes designados 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    public void salvarListaItemPedidoEstoque(List<ItemPedidoEstoque> v_lista_itempedidoestoque)
    {
        StringBuilder v_html = new StringBuilder();
        ItemPedidoEstoqueDao itemDao = new ItemPedidoEstoqueDao();

        foreach (ItemPedidoEstoque item in v_lista_itempedidoestoque) {
            itemDao.incluirItemPedidoEstoque(item);
        }

    }
}
