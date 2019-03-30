using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

public class CssCompressorHandler : IHttpHandler
{
    public bool IsReusable
    {
        get { return false; }
    }

    public void ProcessRequest(HttpContext context)
    {
        if (!string.IsNullOrEmpty(context.Request.QueryString["stylesheets"]))
        {
            string[] relativeFiles = context.Request.QueryString["stylesheets"].Split(',');
            string[] absoluteFiles = new string[relativeFiles.Length];

            for (int i = 0; i < relativeFiles.Length; i++)
            {
                string file = relativeFiles[i];

                if (file.IndexOf("?poseydon") > -1)
                    file = file.Split(new string[] { "?poseydon" }, StringSplitOptions.None)[0];

                if (file.EndsWith(".css"))
                {
                    string absoluteFile = context.Server.MapPath(file);
                    WriteContent(context, absoluteFile);
                    absoluteFiles[i] = absoluteFile;
                }
            }

            SetHeaders(context, absoluteFiles);
            WebHelper.Compress(context);
        }
    }

    /// <summary>
    /// Writes the content of the individual stylesheets to the response stream.
    /// </summary>
    private void WriteContent(HttpContext context, string file)
    {
        using (StreamReader reader = new StreamReader(file))
        {
            string body = reader.ReadToEnd();
            body = StripWhitespace(body);
            context.Response.Write(body);
        }
    }

    /// <summary>
    /// Strips the whitespace from any .css file.
    /// </summary>
    private static string StripWhitespace(string body)
    {
        body = body.Replace("  ", " ");
        body = body.Replace(Environment.NewLine, String.Empty);
        body = body.Replace("\t", string.Empty);
        body = body.Replace(" {", "{");
        body = body.Replace(" :", ":");
        body = body.Replace(": ", ":");
        body = body.Replace(", ", ",");
        body = body.Replace("; ", ";");
        body = body.Replace(";}", "}");

        // sometimes found when retrieving CSS remotely
        body = body.Replace(@"?", string.Empty);

        //body = Regex.Replace(body, @"/\*[^\*]*\*+([^/\*]*\*+)*/", "$1");
        body = Regex.Replace(body, @"(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,}(?=&nbsp;)|(?<=&ndsp;)\s{2,}(?=[<])", String.Empty);

        //Remove comments from CSS
        body = Regex.Replace(body, @"/\*[\d\D]*?\*/", string.Empty);

        string varUrl = clsFuncoes.SiteRootUrl() + "App_Themes/T1/";
        body = body.Replace("url(imagens", "url(" + varUrl + "imagens");

        return body;
    }

    /// <summary>
    /// This will make the browser and server keep the output
    /// in its cache and thereby improve performance.
    /// </summary>
    private static void SetHeaders(HttpContext context, string[] files)
    {
        context.Response.ContentType = "text/css";
        //return;
        //context.Response.AddFileDependencies(files);
        //context.Response.Cache.VaryByParams["stylesheets"] = true;
        //context.Response.Cache.SetETagFromFileDependencies();
        //context.Response.Cache.SetLastModifiedFromFileDependencies();
        //context.Response.Cache.SetValidUntilExpires(true);
        //context.Response.Cache.SetExpires(DateTime.Now.AddDays(7));
        //context.Response.Cache.SetCacheability(HttpCacheability.Public);
    }
}

/// <summary>
/// Find scripts and change the src to the ScriptCompressorHandler.
/// </summary>
public class CssCompressorModule : IHttpModule
{

    #region IHttpModule Members

    void IHttpModule.Dispose()
    {
        // Nothing to dispose; 
    }

    void IHttpModule.Init(HttpApplication context)
    {
        context.PostAuthenticateRequest += new EventHandler(context_BeginRequest);
    }

    #endregion

    void context_BeginRequest(object sender, EventArgs e)
    {
        HttpApplication app = sender as HttpApplication;
        Page page = app.Context.CurrentHandler as Page;
        if (page != null)
        {
            CombineCss(page);
        }
    }

    private void CombineCss(Page page)
    {
        Collection<HtmlControl> stylesheets = new Collection<HtmlControl>();
        foreach (Control control in page.Header.Controls)
        {
            HtmlControl c = control as HtmlControl;

            if (c != null && c.Attributes["rel"] != null && c.Attributes["rel"].Equals("stylesheet", StringComparison.OrdinalIgnoreCase))
            {
                if (!c.Attributes["href"].StartsWith("http://"))
                    stylesheets.Add(c);
            }
        }

        string[] paths = new string[stylesheets.Count];
        for (int i = 0; i < stylesheets.Count; i++)
        {
            page.Header.Controls.Remove(stylesheets[i]);
            paths[i] = stylesheets[i].Attributes["href"];
        }

        AddStylesheetsToHeader(page, paths);
    }

    private void AddStylesheetsToHeader(Page page, string[] paths)
    {
        HtmlLink link = new HtmlLink();
        link.Attributes["rel"] = "stylesheet";
        link.Attributes["type"] = "text/css";
        link.Href = "~/css.axd?stylesheets=" + HttpUtility.UrlEncode(string.Join(",", paths)) + "?" + DateTime.Now.ToString("ddMMyyyyHHmmss");
        page.Header.Controls.Add(link);
    }

}