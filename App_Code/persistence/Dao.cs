using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public class Dao
{
    public Dao()
    {

    }

    public static int getCmdTimeout()
    {
        return int.Parse(ConfigurationManager.AppSettings["CMD_TIMEOUT"]);
    }

    public static int ExecutarSql(string p_sql, string p_strconn, params SqlParameter[] p_sqlparam)
    {
        using (SqlConnection conn = new SqlConnection(p_strconn))
        {
            conn.Open();

            SqlCommand objCmd = new SqlCommand(p_sql, conn);
            objCmd.CommandTimeout = getCmdTimeout();

            if (p_sqlparam != null)
            {
                foreach (SqlParameter param in p_sqlparam)
                    objCmd.Parameters.Add(param);
            }

            int v_total = objCmd.ExecuteNonQuery();
            objCmd.Parameters.Clear();
            objCmd.Dispose();

            return v_total;
        }
    }

    public static int getScopeIdentity(string p_sql, string p_strconn, params SqlParameter[] p_sqlparam)
    {
        using (SqlConnection conn = new SqlConnection(p_strconn))
        {
            conn.Open();

            SqlCommand objCmd = new SqlCommand(p_sql + ";select scope_identity() statement", conn);
            objCmd.CommandTimeout = getCmdTimeout();

            if (p_sqlparam != null)
            {
                foreach (SqlParameter param in p_sqlparam)
                    objCmd.Parameters.Add(param);
            }

            int scopeid = Convert.ToInt32(objCmd.ExecuteScalar());

            objCmd.Parameters.Clear();

            return scopeid;
        }
    }

    public static string getScalar(string p_sql, string p_strconn, params SqlParameter[] p_sqlparam)
    {
        using (SqlConnection conn = new SqlConnection(p_strconn))
        {
            conn.Open();

            SqlCommand objCmd = new SqlCommand(p_sql, conn);
            objCmd.CommandTimeout = getCmdTimeout();

            if (p_sqlparam != null)
            {
                foreach (SqlParameter param in p_sqlparam)
                    objCmd.Parameters.Add(param);
            }

            string varRetorno;

            if (objCmd.ExecuteScalar() == null)
                varRetorno = "-1";
            else
                varRetorno = objCmd.ExecuteScalar().ToString();

            objCmd.Parameters.Clear();

            return varRetorno;
        }
    }

    public static bool getResultado(string p_sql, string p_strconn, params SqlParameter[] p_sqlparam)
    {
        using (SqlConnection conn = new SqlConnection(p_strconn))
        {
            conn.Open();

            SqlCommand objCmd = new SqlCommand(p_sql, conn);
            objCmd.CommandTimeout = getCmdTimeout();

            if (p_sqlparam != null)
            {
                foreach (SqlParameter param in p_sqlparam)
                    objCmd.Parameters.Add(param);
            }

            SqlDataReader objreader = objCmd.ExecuteReader();

            objCmd.Parameters.Clear();

            if (objreader.Read() == true)
                return true;
            else
                return false;
        }
    }

    public static DataTable getDataTable(string p_sql, string p_strconn, params SqlParameter[] p_sqlparam)
    {
        using (SqlConnection conn = new SqlConnection(p_strconn))
        {
            conn.Open();

            using (SqlCommand objCmd = new SqlCommand(p_sql, conn))
            {
                objCmd.CommandTimeout = getCmdTimeout();

                if (p_sqlparam != null)
                {
                    foreach (SqlParameter param in p_sqlparam)
                        objCmd.Parameters.Add(param);
                }

                using (DataTable objTable = new DataTable())
                {
                    using (SqlDataReader objReader = objCmd.ExecuteReader())
                    {
                        objTable.Load(objReader);
                        objCmd.Parameters.Clear();

                        return objTable;
                    }
                }
            }
        }
    }

    #region SEM PARAMETRO

    public static void ExecutarSql(string p_sql, string p_strconn)
    {
        ExecutarSql(p_sql, p_strconn, null);
    }

    public static int getScopeIdentity(string p_sql, string p_strconn)
    {
        return getScopeIdentity(p_sql, p_strconn, null);
    }

    public static string getScalar(string p_sql, string p_strconn)
    {
        return getScalar(p_sql, p_strconn, null);
    }

    public static bool getResultado(string p_sql, string p_strconn)
    {
        return getResultado(p_sql, p_strconn, null);
    }

    public static DataTable getDataTable(string p_sql, string p_strconn)
    {
        return getDataTable(p_sql, p_strconn, null);
    }

    #endregion
}