using System.Configuration;

public class Conexao
{
    static string v_srv;
    public Conexao()
    {
    }

    public static string getStrConexao(string p_name)
    {
        string v_strconn = string.Empty;

        if (System.Web.HttpContext.Current.Session["SRVBD"] == null)
            v_srv = "localhost";
        else if ((string)System.Web.HttpContext.Current.Session["SRVBD"] == string.Empty)
            v_srv = "localhost";
        else
        {
            v_srv = "localhost";
        }

        switch (p_name)
        {
            case "CONN_ESTOQUE":
                v_strconn = "Data Source=" + v_srv + ";Initial Catalog=estoque;User ID=sa;pwd=sigma;";
                break;
        }

        return v_strconn;
    }
}