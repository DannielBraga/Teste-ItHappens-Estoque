using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de Filial
/// </summary>
public class Filial
{
    private Int32 codFilial;
    private String descricao;
    private DateTime dataInsercao;

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

    public Filial()
    {
        //
        // TODO: Adicionar lógica do construtor aqui
        //
    }
}