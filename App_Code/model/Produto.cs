using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de Produto
/// </summary>
public class Produto
{
    private Int32 codProduto;
    private String descricao;
    private DateTime dataInsercao;

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

    public DateTime DataInsercao
    {
        get
        {
            return dataInsercao;
        }

        set
        {
            dataInsercao = value;
        }
    }

    public Produto()
    {
        //
        // TODO: Adicionar lógica do construtor aqui
        //
    }
}