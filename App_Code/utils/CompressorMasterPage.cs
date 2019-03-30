using System.Text.RegularExpressions;
using System.Web.UI;

public abstract class CompressorMasterPage : System.Web.UI.MasterPage
{
    private static readonly Regex REGEX_BETWEEN_TAGS = new Regex(@">\s+<", RegexOptions.Compiled);
    private static readonly Regex REGEX_LINE_BREAKS = new Regex(@"\n\s+", RegexOptions.Compiled);
    private static readonly Regex REGEX_COMMENTS = new Regex(@"<!--.*?-->", RegexOptions.Compiled);

    protected override void Render(HtmlTextWriter writer)
    {
        using (HtmlTextWriter htmlwriter = new HtmlTextWriter(new System.IO.StringWriter()))
        {
            base.Render(htmlwriter);
            string html = htmlwriter.InnerWriter.ToString();

            //html = REGEX_BETWEEN_TAGS.Replace(html, "><");
            //html = REGEX_LINE_BREAKS.Replace(html, string.Empty);
            //html = html.Replace("<!--[if IE]>", "<[if IE]>").Replace("<![endif]-->", "<![endif]>");
            //html = REGEX_COMMENTS.Replace(html, string.Empty);
            //html = html.Replace("<[if IE]>", "<!--[if IE]>").Replace("<![endif]>", "<![endif]-->");

            writer.Write(html.Trim());
        }
    }

}