# ASPxRichEdit - How to print a document without headers and footers

<p>In some scenarios, it's necessary to print a document in ASPxRichEdit without its header and footer. Currently, ASPxRichEdit doesn't provide the built-in capability to remove them from the printed document. </p>
<p>This example demonstrates a possible way to implement this export in a custom way on a customized Print ribbon item click and remove the header and footer from the document opened by the <a href="https://documentation.devexpress.com/#CoreLibraries/clsDevExpressXtraRichEditRichEditDocumentServertopic">RichEditDocumentServer</a> component, our non-visual document processing engine. </p>
<br/>