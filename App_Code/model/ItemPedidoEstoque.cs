using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de ItemPedidoEstoque
/// </summary>
public class ItemPedidoEstoque
{
    private Int32 codPedidoEstoque;
    private Int32 codProduto;
    private Int32 quantidade;

    public int CodPedidoEstoque
    {
        get
        {
            return codPedidoEstoque;
        }

        set
        {
            codPedidoEstoque = value;
        }
    }

    public int CodProduto
    {
        get
        {
            return codProduto;
        }

        set
        {
            codProduto = value;
        }
    }

    public int Quantidade
    {
        get
        {
            return quantidade;
        }

        set
        {
            quantidade = value;
        }
    }

    public ItemPedidoEstoque()
    {
        //
        // TODO: Adicionar lógica do construtor aqui
        //
    }
}