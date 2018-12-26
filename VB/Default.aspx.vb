Option Infer On

Imports DevExpress.Web
Imports DevExpress.Web.ASPxThemes
Imports DevExpress.Web.Office
Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraRichEdit.API.Native
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Partial Public Class _Default
    Inherits System.Web.UI.Page

    Private docID As String = "document1"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        If Not IsPostBack Then
            DocumentManager.CloseAllDocuments()
            ASPxRichEdit1.Open(Server.MapPath("test.docx"))
            ASPxRichEdit1.DocumentId = docID
        End If

        ASPxRichEdit1.CreateDefaultRibbonTabs(True)

        ASPxRichEdit1.RibbonTabs(0).Groups(0).Items.RemoveAt(4)
        Dim newItem = New RibbonButtonItem("Print")
        newItem.LargeImage.IconID = IconID.PrintPrintdialog32x32
        newItem.Size = RibbonItemSize.Large
        ASPxRichEdit1.RibbonTabs(0).Groups(0).Items.Add(newItem)

        If Request.Params("print") = "true" Then
            Dim stream As New MemoryStream()
            DocumentManager.FindDocument(docID).SaveCopy(stream)

            Dim server_Renamed As New RichEditDocumentServer()
            stream.Position = 0
            server_Renamed.LoadDocument(stream, DocumentFormat.OpenXml)
            For Each section As Section In server_Renamed.Document.Sections
                UpdateFieldsInHeader(section, HeaderFooterType.Primary)
                UpdateFieldsInHeader(section, HeaderFooterType.First)
                UpdateFieldsInHeader(section, HeaderFooterType.Even)
                UpdateFieldsInHeader(section, HeaderFooterType.Odd)
                UpdateFieldsInFooter(section, HeaderFooterType.Primary)
                UpdateFieldsInFooter(section, HeaderFooterType.First)
                UpdateFieldsInFooter(section, HeaderFooterType.Even)
                UpdateFieldsInFooter(section, HeaderFooterType.Odd)
            Next section

            Dim opt As New DevExpress.XtraPrinting.PdfExportOptions()
            opt.ShowPrintDialogOnOpen = True

            Dim ps As New MemoryStream()
            server_Renamed.ExportToPdf(ps, opt)
            Response.Clear()
            Response.ContentType = "application/pdf"
            Response.AddHeader("Content-Disposition", "inline; filename=print.pdf")
            Response.BinaryWrite(ps.ToArray())
            Response.Flush()
            Response.Close()
            Response.End()
            Return
        End If
    End Sub

        Private Sub UpdateFieldsInHeader(ByVal section As Section, ByVal type As HeaderFooterType)
            If section.HasHeader(type) Then

                Dim header_Renamed As SubDocument = section.BeginUpdateHeader(type)
                header_Renamed.Replace(header_Renamed.Range, String.Empty)
                section.EndUpdateHeader(header_Renamed)
            End If
        End Sub

        Private Sub UpdateFieldsInFooter(ByVal section As Section, ByVal type As HeaderFooterType)
            If section.HasFooter(type) Then
                Dim footer As SubDocument = section.BeginUpdateFooter(type)
                footer.Replace(footer.Range, String.Empty)
                section.EndUpdateFooter(footer)
            End If
        End Sub

End Class