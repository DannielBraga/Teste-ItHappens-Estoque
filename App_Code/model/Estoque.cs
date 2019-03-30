using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de Estoque
/// </summary>
public class Estoque
{
    private Int32 codEstoque;
    private Int32 codFilial;
    private DateTime dataInsercao;

    public int CodEstoque
    {
        get
        {
            return codEstoque;
        }

        set
        {
            codEstoque = value;
        }
    }

    public int CodFilial
    {
        get
        {
            return codFilial;
        }

        set
        {
            codFilial = value;
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

    public Estoque()
    {
        //
        // TODO: Adicionar lógica do construtor aqui
        //
    }
}