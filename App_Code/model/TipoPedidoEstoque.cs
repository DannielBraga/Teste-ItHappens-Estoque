using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de TipoPedidoEstoque
/// </summary>
public class TipoPedidoEstoque
{
    private Int32 codTipoPedido;
    private String descricao;

    public int CodTipoPedido
    {
        get
        {
            return codTipoPedido;
        }

        set
        {
            codTipoPedido = value;
        }
    }

    public string Descricao
    {
        get
        {
            return descricao;
        }

        set
        {
            descricao = value;
        }
    }

    public TipoPedidoEstoque()
    {
        //
        // TODO: Adicionar lógica do construtor aqui
        //
    }
}