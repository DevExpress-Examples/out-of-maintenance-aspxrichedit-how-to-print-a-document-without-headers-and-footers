<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/163166045/18.2.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T830487)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# ASPxRichEdit - How to print a document without headers and footers
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/163166045/)**
<!-- run online end -->

<p>In some scenarios, it's necessary to print a document in ASPxRichEdit without its header and footer. Currently, ASPxRichEdit doesn't provide the built-in capability to remove them from the printed document. </p>
<p>This example demonstrates a possible way to implement this export in a custom way on a customized Print ribbon item click and remove the header and footer from the document opened by the <a href="https://documentation.devexpress.com/#CoreLibraries/clsDevExpressXtraRichEditRichEditDocumentServertopic">RichEditDocumentServer</a> component, our non-visual document processing engine. </p>
<br/>
