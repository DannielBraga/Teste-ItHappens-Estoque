using System;
using System.Web;
using System.Web.UI;

public class clsMessageBox
{
	public clsMessageBox()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static void MsgBox(string paramMsg)
    {
        paramMsg = paramMsg.Replace("\\", "\\\\");
        paramMsg = paramMsg.Replace("\r", "\\r");
        paramMsg = paramMsg.Replace("\n", "\\n");
        paramMsg = paramMsg.Replace("'", "\\'");

        //string script = "<script type=\"text/javascript\">alert('" + paramMsg + "');</script>";
        string script = "alert('" + paramMsg + "');";
        Page page = HttpContext.Current.CurrentHandler as Page;

        if (page != null)
            ScriptManager.RegisterStartupScript(page, page.GetType(), Guid.NewGuid().ToString(), script, true);

        //if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
        //    page.ClientScript.RegisterClientScriptBlock(typeof(clsMessageBox),"alert", script);
    }

    public static void closeForm()
    {
        string script = "window.close();";
        Page page = HttpContext.Current.CurrentHandler as Page;

        if (page != null)
            ScriptManager.RegisterStartupScript(page, page.GetType(), Guid.NewGuid().ToString(), script, true);
    }
}
