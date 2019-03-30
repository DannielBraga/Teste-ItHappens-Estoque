using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class clsFuncoes
{
    public clsFuncoes()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string SiteRootUrl()
    {
        //return VirtualPathUtility.ToAbsolute("~/").Replace("/", string.Empty);
        return ConfigurationManager.AppSettings["CONFIG_SITE_SERVIDOR_WEB_PREFIX"];
    }

    public static string SiteNome()
    {
        return ConfigurationManager.AppSettings["CETRUS_NOME"];
    }

    public static string SiteDesc()
    {
        return ConfigurationManager.AppSettings["CETRUS_DESC"];
    }

    public static string SiteEmail()
    {
        return ConfigurationManager.AppSettings["CONFIG_SITE_EMAIL_SISTEMA"];
    }

    /// <summary>
    /// Passa uma data no formato 20121212 e retorna no formato 12/12/2012
    /// </summary>
    /// <param name="p_dataymd"></param>
    /// <returns></returns>
    public static string getFormatDataDMY(string p_dataymd)
    {
        string dia = p_dataymd.Substring(6, 2);
        string mes = p_dataymd.Substring(4, 2);
        string ano = p_dataymd.Substring(0, 4);

        return dia + "/" + mes + "/" + ano;
    }

    public static bool IsNumeric(string paramValor)
    {
        int resultado;
        if (int.TryParse(paramValor, out resultado))
            return true;
        else
            return false;
    }

    public static void SetFocus(Control ctrl)
    {
        Page page = HttpContext.Current.CurrentHandler as Page;
        string script = "document.getElementById('" + ctrl.ClientID + "').focus();";
        ScriptManager.RegisterStartupScript(page, page.GetType(), Guid.NewGuid().ToString(), script, true);
    }

    public static int GetUltimoDiaDoMes(int ano, int mes)
    {
        int dia = DateTime.DaysInMonth(ano, mes);
        return dia;
    }

    public static DateTime GetUltimaTercaFeiraDoMes(int ano, int mes)
    {
        CultureInfo culture = new CultureInfo(CultureInfo.CurrentCulture.ToString());
        DateTimeFormatInfo dtfi = culture.DateTimeFormat;
        DateTime var_Data = new DateTime(ano, mes, 1);
        string varData;

        for (int i = GetUltimoDiaDoMes(ano, mes); i >= 1; i--)
        {
            var_Data = new DateTime(ano, mes, i);
            varData = dtfi.GetDayName(var_Data.DayOfWeek);

            if (varData.Equals("segunda-feira") == true)
                break;
        }
        return var_Data;
    }

    public static void MudarCorLinhaSelecionada(GridView paramGridView, HtmlInputHidden paramTxtRowIndex, Int32 paramRowIndexMarcar)
    {
        if (paramTxtRowIndex.Value.Equals("") == false)
        {
            paramGridView.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            paramGridView.Rows[Convert.ToInt32(paramTxtRowIndex.Value)].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        }
        paramGridView.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        paramGridView.Rows[paramRowIndexMarcar].BackColor = System.Drawing.ColorTranslator.FromHtml("#e3e3e3");
        paramTxtRowIndex.Value = paramRowIndexMarcar.ToString();
    }

    public static string ReplaceTextoXml(string varTexto)
    {
        varTexto = varTexto.Replace("&", "&amp;");
        varTexto = varTexto.Replace("<", "&alt;");
        varTexto = varTexto.Replace(">", "&gt;");
        //varTexto = varTexto.Replace("'", "&após;");
        varTexto = varTexto.Replace("\"", "&quot;");

        return varTexto;
    }

    public static Control FindControlRecursive(Control Root, string Id)
    {
        if (Root.ID == Id)
            return Root;

        foreach (Control Ctl in Root.Controls)
        {
            Control FoundCtl = FindControlRecursive(Ctl, Id);
            if (FoundCtl != null)
                return FoundCtl;
        }
        return null;
    }

    //public static Control FindControlRecursive(Control container, string name)
    //{
    //    if ((container.ID != null) && (container.ID.Equals(name)))
    //        return container;

    //    foreach (Control ctrl in container.Controls)
    //    {
    //        Control foundCtrl = FindControlRecursive(ctrl, name);
    //        if (foundCtrl != null)
    //            return foundCtrl;
    //    }
    //    return null;
    //}

    //public static string ResolveUrl(string relativeUrl)
    //{
    //    if (HttpContext.Current != null)
    //    {
    //        System.Web.UI.Page p = HttpContext.Current.Handler as System.Web.UI.Page;
    //        if (p != null)
    //            return p.ResolveUrl(relativeUrl);
    //        else
    //            throw new InvalidOperationException("Unable to Resolve: Not in a Page Context");
    //    }
    //    else
    //        throw new InvalidOperationException("Unable to Resolve: Not in a HttpContext");
    //}

    public static string ResolveUrl(string originalUrl)
    {

        if (originalUrl == null)

            return null;



        // *** Absolute path - just return

        if (originalUrl.IndexOf("://") != -1)

            return originalUrl;



        // *** Fix up image path for ~ root app dir directory

        if (originalUrl.StartsWith("~"))
        {

            string newUrl = "";

            if (HttpContext.Current != null)

                newUrl = HttpContext.Current.Request.ApplicationPath +

                      originalUrl.Substring(1).Replace("//", "/");

            else

                // *** Not context: assume current directory is the base directory

                throw new ArgumentException("Invalid URL: Relative URL not allowed.");



            // *** Just to be sure fix up any double slashes

            return newUrl;

        }



        return originalUrl;

    }

    #region [ROTINAS IMAGEM]

    public static byte[] imageToByteArray(System.Drawing.Image imageIn)
    {
        MemoryStream ms = new MemoryStream();
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        return ms.ToArray();
    }

    public static System.Drawing.Image byteArrayToImage(byte[] byteArrayIn)
    {
        MemoryStream ms = new MemoryStream(byteArrayIn);
        System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
        return returnImage;
        ms.Dispose();
    }

    public static byte[] GetFileBytes(string url)
    {
        WebRequest webRequest = WebRequest.Create(url);
        byte[] fileBytes = null;
        byte[] buffer = new byte[4096];
        WebResponse webResponse = webRequest.GetResponse();

        try
        {
            using (Stream stream = webResponse.GetResponseStream())
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    int chunkSize = 0;
                    do
                    {
                        chunkSize = stream.Read(buffer, 0, buffer.Length);
                        memoryStream.Write(buffer, 0, chunkSize);
                    } while (chunkSize != 0);

                    fileBytes = memoryStream.ToArray();
                }
            }
        }
        catch
        {
            throw;
        }

        return fileBytes;
    }

    public static string ImageToBase64(System.Drawing.Image image, System.Drawing.Imaging.ImageFormat format)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            // Convert Image to byte[]
            image.Save(ms, format);
            byte[] imageBytes = ms.ToArray();

            // Convert byte[] to Base64 String
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }
    }

    public static System.Drawing.Image Base64ToImage(string base64String)
    {
        // Convert Base64 String to byte[]
        byte[] imageBytes = Convert.FromBase64String(base64String);
        MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

        // Convert byte[] to Image
        ms.Write(imageBytes, 0, imageBytes.Length);
        System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
        return image;
    }

    #endregion

    #region [ASPNET_REGIIS]
    public static void CryptConnectionStringsWebConfig(String paramDirVirtual)
    {
        String varComando = "-pe \"connectionStrings\" -app \"/" + paramDirVirtual + "\" -prov \"DataProtectionConfigurationProvider\"";
        System.Diagnostics.Process.Start(@"C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\aspnet_regiis.exe", varComando);
    }
    public static void DecryptConnectionStringsWebConfig(String paramDirVirtual)
    {
        String varComando = "-pd \"connectionStrings\" -app \"/" + paramDirVirtual + "\"";
        System.Diagnostics.Process.Start(@"C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\aspnet_regiis.exe", varComando);
    }

    public static void aspnet_regiis(String paramComando)
    {
        System.Diagnostics.Process.Start(@"C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\aspnet_regiis.exe", paramComando);
    }
    #endregion

    #region [EXPORTAR PARA EXCEL]
    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="gv"></param>
    public static void ExportarGridViewParaExcel(string fileName, GridView gv)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            {
                //  Create a table to contain the grid
                Table table = new Table();

                //  include the gridline settings
                table.GridLines = gv.GridLines;

                //  add the header row to the table
                if (gv.HeaderRow != null)
                {
                    clsFuncoes.PrepareControlForExport(gv.HeaderRow);
                    table.Rows.Add(gv.HeaderRow);
                }

                //  add each of the data rows to the table
                foreach (GridViewRow row in gv.Rows)
                {
                    clsFuncoes.PrepareControlForExport(row);
                    table.Rows.Add(row);
                }

                //  add the footer row to the table
                if (gv.FooterRow != null)
                {
                    clsFuncoes.PrepareControlForExport(gv.FooterRow);
                    table.Rows.Add(gv.FooterRow);
                }

                //  render the table into the htmlwriter
                table.RenderControl(htw);

                //  render the htmlwriter into the response
                HttpContext.Current.Response.Write(sw.ToString());
                HttpContext.Current.Response.End();
            }
        }
    }

    /// <summary>
    /// Replace any of the contained controls with literals
    /// </summary>
    /// <param name="control"></param>
    private static void PrepareControlForExport(Control control)
    {
        for (int i = 0; i < control.Controls.Count; i++)
        {
            Control current = control.Controls[i];
            if (current is LinkButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
            }
            else if (current is ImageButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
            }
            else if (current is HyperLink)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
            }
            else if (current is DropDownList)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
            }
            else if (current is CheckBox)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
            }

            if (current.HasControls())
            {
                clsFuncoes.PrepareControlForExport(current);
            }
        }
    }

    protected void ExportarExcel()
    {
        //ROTINAS PARA PAGINAS ASPX (CODEBEHIND)

        //System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        //HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        //GridView gridViewTemp = new GridView();
        //Session["ExportGridviewColumns"] = GridView1.Columns;

        //foreach (DataControlField dcf in (DataControlFieldCollection)Session["ExportGridviewColumns"])
        //    gridViewTemp.Columns.Add(dcf);

        //gridViewTemp.AllowPaging = false;
        //gridViewTemp.AllowSorting = false;
        //gridViewTemp.Font.Name = "Verdana";
        //gridViewTemp.Font.Size = 8;
        //gridViewTemp.CssClass = "Grid1";
        //gridViewTemp.DataSource = Session["ExportGridViewDataSource"];
        //gridViewTemp.DataBind();
        //gridViewTemp.RenderControl(htmlWrite);

        //Response.Clear();
        //Response.AddHeader("content-disposition", "attachment; filename=Equipamentos.xls");
        ////Response.AddHeader("content-disposition", "inline; filename=Equipamentos.xls")
        ////Response.AddHeader("Content-Length", new System.IO.FileInfo("Equipamentos.xlsx").Length.ToString());
        //Response.Charset = "";
        ////Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //Response.ContentType = "application/ms-excel";
        //Response.Write(stringWrite.ToString());
        ////Response.Flush();
        //Response.End();

        ////application/pdf
        ////application/x-pdf
        ////application/msword
        ////application/vnd.xls"
        ////application/vnd.ms-excel
        ////application/x-msexcel
        ////application/ms-excel
        ////application/vnd.ms-powerpoint 
        ////application/vnd.openxmlformats-officedocument.spreadsheetml.sheet
        ////application/ms-powerpoint 
    }

    protected void Exportar_Excel()
    {
        //Response.Clear();
        //Response.AddHeader("content-disposition", "attachment; filename=Equipamentos.xls");
        //Response.Charset = "";
        //Response.ContentType = "application/ms-excel";
        //System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        //System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        //GridView gridViewTemp = new GridView();

        //Session["ExportGridviewColumns"] = GridView1.Columns;

        //foreach (DataControlField dcf in (DataControlFieldCollection)Session["ExportGridviewColumns"])
        //{
        //    gridViewTemp.Columns.Add(dcf);
        //}

        //gridViewTemp.AllowPaging = false;
        //gridViewTemp.AllowSorting = false;
        //gridViewTemp.Font.Name = "Verdana";
        //gridViewTemp.Font.Size = 8;
        //gridViewTemp.CssClass = "Grid1";
        //gridViewTemp.DataSource = Session["ExportGridViewDataSource"]; //Carrega de novo a rotina para popular o gridview
        //gridViewTemp.DataBind();
        //gridViewTemp.RenderControl(htmlWrite);

        //Response.Write(stringWrite.ToString());
        //Response.End();
    }
    #endregion

    #region [HTML HEADER ITENS]

    public static void AddMetaContentType()
    {
        //HtmlMeta meta = new HtmlMeta();
        //meta.HttpEquiv = "content-type";
        //meta.Content = Response.ContentType + "; charset=" + Response.ContentEncoding.HeaderName;
        //Page page = HttpContext.Current.CurrentHandler as Page;
        //page.Header.Controls.Add(meta);
    }

    public static void AddMetaTag(string paramName, string paramValue)
    {
        HtmlMeta meta = new HtmlMeta();
        meta.Name = paramName;
        meta.Content = paramValue;
        Page page = HttpContext.Current.CurrentHandler as Page;
        page.Header.Controls.Add(meta);
    }

    public static void AddStyleSheet(string paramCaminho)
    {
        HtmlLink link = new HtmlLink();
        link.Href = paramCaminho;
        link.Attributes["type"] = "text/css";
        link.Attributes["rel"] = "stylesheet";
        Page page = HttpContext.Current.CurrentHandler as Page;
        page.Header.Controls.Add(link);
    }

    public static void AddJavaScript(params string[] paths)
    {
        for (int i = 0; i < paths.Length; i++)
        {
            HtmlGenericControl script = new HtmlGenericControl("script");
            script.Attributes.Add("type", "text/javascript");
            script.Attributes.Add("src", paths[i].ToString());
            Page page = HttpContext.Current.CurrentHandler as Page;
            page.Header.Controls.Add(script);
        }
    }

    #endregion

    #region ROTINAS DATA
    public static string getMes(DateTime p_data)
    {
        string v_mes = string.Empty;

        switch (p_data.Month)
        {
            case 1:
                v_mes = "Jan";
                break;
            case 2:
                v_mes = "Fev";
                break;
            case 3:
                v_mes = "Mar";
                break;
            case 4:
                v_mes = "Abr";
                break;
            case 5:
                v_mes = "Mai";
                break;
            case 6:
                v_mes = "Jun";
                break;
            case 7:
                v_mes = "Jul";
                break;
            case 8:
                v_mes = "Ago";
                break;
            case 9:
                v_mes = "Set";
                break;
            case 10:
                v_mes = "Out";
                break;
            case 11:
                v_mes = "Nov";
                break;
            case 12:
                v_mes = "Dez";
                break;
        }

        return v_mes;
    }
    #endregion


    //public static void SalvarHtmlImage(string inputUrl, string outputPath, Rectangle crop)
    //{
    //    System.Windows.Forms.WebBrowser wb = new System.Windows.Forms.WebBrowser();
    //    wb.ScrollBarsEnabled = false;
    //    wb.ScriptErrorsSuppressed = true;
    //    wb.Navigate(inputUrl);

    //    while (wb.ReadyState !=System.Windows.Forms.WebBrowserReadyState.Complete)
    //    {
    //        System.Windows.Forms.Application.DoEvents();
    //    }

    //    wb.Width = wb.Document.Body.ScrollRectangle.Width;
    //    wb.Height = wb.Document.Body.ScrollRectangle.Height;
    //    using (Bitmap bitmap = new Bitmap(wb.Width, wb.Height))
    //    {
    //        wb.DrawToBitmap(bitmap, new Rectangle(0, 0, wb.Width, wb.Height));
    //        wb.Dispose();
    //        Rectangle rect = new Rectangle(crop.Left, crop.Top, wb.Width - crop.Width - crop.Left, wb.Height - crop.Height - crop.Top);
    //        Bitmap cropped = bitmap.Clone(rect, bitmap.PixelFormat);
    //        cropped.Save(outputPath, System.Drawing.Imaging.ImageFormat.Png);
    //    }
    //}
}
