using System;
using System.IO.Compression;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

public static class WebHelper
{

	#region HTTP compression

	private const string GZIP = "gzip";
	private const string DEFLATE = "deflate";

	public static void Compress(HttpContext context)
	{
		if (IsEncodingAccepted(GZIP))
		{
			context.Response.Filter = new GZipStream(context.Response.Filter, CompressionMode.Compress);
			SetEncoding(GZIP);
		}
		else if (IsEncodingAccepted(DEFLATE))
		{
			context.Response.Filter = new DeflateStream(context.Response.Filter, CompressionMode.Compress);
			SetEncoding(DEFLATE);
		}
	}

	/// <summary>
	/// Checks the request headers to see if the specified
	/// encoding is accepted by the client.
	/// </summary>
	private static bool IsEncodingAccepted(string encoding)
	{
		return HttpContext.Current.Request.Headers["Accept-encoding"] != null && HttpContext.Current.Request.Headers["Accept-encoding"].Contains(encoding);
	}

	/// <summary>
	/// Adds the specified encoding to the response headers.
	/// </summary>
	/// <param name="encoding"></param>
	private static void SetEncoding(string encoding)
	{
		HttpContext.Current.Response.AppendHeader("Content-encoding", encoding);
	}

	#endregion

	#region Script and CSS urls

    public static void AddScriptsToHeader(params string[] paths)
    {
        if (HttpContext.Current != null)
        {
            HtmlGenericControl link = new HtmlGenericControl("script");
            link.Attributes["type"] = "text/javascript";
            link.Attributes["src"] = "~/js.axd?path=" + HttpUtility.UrlEncode(string.Join(",", paths)) + "?poseydon" + DateTime.Now.ToString("ddMMyyyyHHmmss");

            Page page = HttpContext.Current.CurrentHandler as Page;
            if (page != null)
            {
                //page.Header.Controls.Add(link);
                Control scripts = clsFuncoes.FindControlRecursive(page, "scripts");
                scripts.Controls.Add(link);
            }
        }
    }

    public static void AddCssToHeader(params string[] paths)
    {
        if (HttpContext.Current != null)
        {
            HtmlLink link = new HtmlLink();
            link.Attributes["rel"] = "stylesheet";
            link.Attributes["type"] = "text/css";
            link.Href = "~/css.axd?stylesheets=" + HttpUtility.UrlEncode(string.Join(",", paths)) + "?poseydon" + DateTime.Now.ToString("ddMMyyyyHHmmss");

            Page page = HttpContext.Current.CurrentHandler as Page;
            if (page != null)
                page.Header.Controls.Add(link);
        }
    }

    public static void AddCssToHeaderPrint(params string[] paths)
    {
        if (HttpContext.Current != null)
        {
            HtmlLink link = new HtmlLink();
            link.Attributes["rel"] = "stylesheet";
            link.Attributes["type"] = "text/css";
            link.Attributes["media"] = "print";
            link.Href = "~/css.axd?stylesheets=" + HttpUtility.UrlEncode(string.Join(",", paths)) + "?poseydon" + DateTime.Now.ToString("ddMMyyyyHHmmss");

            Page page = HttpContext.Current.CurrentHandler as Page;
            if (page != null)
                page.Header.Controls.Add(link);
        }
    }


	#endregion

}
