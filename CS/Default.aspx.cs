using DevExpress.Web;
using DevExpress.Web.ASPxThemes;
using DevExpress.Web.Office;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    private string docID = "document1";
    protected void Page_Load(object sender, EventArgs e)
    {      
        if (!IsPostBack)
        {
            DocumentManager.CloseAllDocuments();
            ASPxRichEdit1.Open(Server.MapPath("test.docx"));    
            ASPxRichEdit1.DocumentId = docID;
        }

        ASPxRichEdit1.CreateDefaultRibbonTabs(true);

        ASPxRichEdit1.RibbonTabs[0].Groups[0].Items.RemoveAt(4);
        var newItem = new RibbonButtonItem("Print");
        newItem.LargeImage.IconID = IconID.PrintPrintdialog32x32;
        newItem.Size = RibbonItemSize.Large;
        ASPxRichEdit1.RibbonTabs[0].Groups[0].Items.Add(newItem);

        if (Request.Params["print"] == "true")
        {
            MemoryStream stream = new MemoryStream();
            DocumentManager.FindDocument(docID).SaveCopy(stream);
            RichEditDocumentServer server = new RichEditDocumentServer();
            stream.Position = 0;
            server.LoadDocument(stream, DocumentFormat.OpenXml);
            foreach(Section section in server.Document.Sections)
            {
                UpdateFieldsInHeader(section, HeaderFooterType.Primary);
                UpdateFieldsInHeader(section, HeaderFooterType.First);
                UpdateFieldsInHeader(section, HeaderFooterType.Even);
                UpdateFieldsInHeader(section, HeaderFooterType.Odd);
                UpdateFieldsInFooter(section, HeaderFooterType.Primary);
                UpdateFieldsInFooter(section, HeaderFooterType.First);
                UpdateFieldsInFooter(section, HeaderFooterType.Even);
                UpdateFieldsInFooter(section, HeaderFooterType.Odd);
            }
          
            DevExpress.XtraPrinting.PdfExportOptions opt = new DevExpress.XtraPrinting.PdfExportOptions();
            opt.ShowPrintDialogOnOpen = true;

            MemoryStream ps = new MemoryStream();
            server.ExportToPdf(ps, opt);
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "inline; filename=print.pdf");
            Response.BinaryWrite(ps.ToArray());
            Response.Flush();
            Response.Close();
            Response.End();
            return;
        }
       }

        private void UpdateFieldsInHeader(Section section, HeaderFooterType type)
        {
            if (section.HasHeader(type))
            {
                SubDocument header = section.BeginUpdateHeader(type);
                header.Replace(header.Range, string.Empty);
                section.EndUpdateHeader(header);
            }
        }

        private void UpdateFieldsInFooter(Section section, HeaderFooterType type)
        {
            if (section.HasFooter(type))
            {
                SubDocument footer = section.BeginUpdateFooter(type);
                footer.Replace(footer.Range, string.Empty);
                section.EndUpdateFooter(footer);
            }
        }   

}