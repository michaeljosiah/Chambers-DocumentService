using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xbehave;
using FluentAssertions;
using DocumentService.Api.Controllers;
using DocumentService.Api.DTO;
using DocumentService.Application.Model;
using DocumentService.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DocumentService.UnitTest.Controllers
{
    //As a publisher
    //I would like to upload, manually re-order, download and delete pdf's
    //So, I can place a list of documents on my client apps and website for users to download
    //And in an arbitrary order of my choosing
    public class DocumentFeature
    {
        
        [Scenario]
        public void SuccesfullDocumentUpload()
        {
            ActionResult<AddDocumentResponse> expectedResult = null;
            IFormFile file = null;
            var documentServiceMock = new Mock<IDocumentService>();
            documentServiceMock
                .Setup(x => x.AddDocument(It.IsAny<DocumentModel>()));
            var controller = new DocumentsController(documentServiceMock.Object);

            "Given I have a PDF to upload"
                .x(() => file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Document")), 0, 0, "TestDoc", "testdoc.pdf"));

            "When I send the PDF to the API"
                .x(() =>
                {
                    expectedResult = controller.AddDocument(1, file);
                });

            "Then it is uploaded succesfully"
                .x(() =>
                {
                    ((OkResult)expectedResult.Result).StatusCode.Should().Be(200);
                });
        }

        [Scenario]
        public void InvalidDocumentUpload()
        {
            ActionResult<AddDocumentResponse> expectedResult = null;
            IFormFile file = null;
            var documentServiceMock = new Mock<IDocumentService>();
            documentServiceMock
                .Setup(x => x.AddDocument(It.IsAny<DocumentModel>()));
            var controller = new DocumentsController(documentServiceMock.Object);

            "Given I have a non-pdf to upload"
                .x(() =>
                {

                    file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Document")), 0, 0, "TestDoc",
                            "TestDoc.txt");
                });

            "When I send the non-pdf to the API"
                .x(() =>
                {
                    expectedResult = controller.AddDocument(1, file);
                });

            "Then the API does not accept the file and returns a 400 status"
                .x(() =>
                {
                    ((ObjectResult)expectedResult.Result).StatusCode.Should().Be(400);
                    ((ObjectResult)expectedResult.Result).Value.Should().Be("Invalid file type");
                });
        }

        [Scenario]
        public void GetListOfDocuments()
        {
            var documentServiceMock = new Mock<IDocumentService>();
            documentServiceMock
                .Setup(x => x.GetDocuments())
                .Returns(new List<DocumentModel>
                {
                    new DocumentModel
                    {
                        Location = "http://some-place.com/test1.pdf",
                        Id = Guid.NewGuid(),
                        Name = "test1.pdf",
                        FileSize = "1000",
                        SortOrder =1
                    },
                    new DocumentModel
                    {
                        Location = "http://some-place.com/test2.pdf",
                        Id = Guid.NewGuid(),
                        Name = "test2.pdf",
                        FileSize = "1000",
                        SortOrder =2
                    },
                    new DocumentModel
                    {
                        Location = "http://some-place.com/test3.pdf",
                        Id = Guid.NewGuid(),
                        Name = "test3.pdf",
                        FileSize = "2000",
                        SortOrder =3
                    }

                });
           
            ActionResult<GetDocumentsResponse> expectedResult = null;
            DocumentsController controller = null;
            "Given I call the new document service API"
                .x(() => { controller = new DocumentsController(documentServiceMock.Object); });

            "When I call the API to get a list of documents"
                .x(() => { expectedResult = controller.GetDocuments(); });

            "Then a list of PDF's is returned"
                .x(() =>
                {
                    var documents = expectedResult.Value?.Documents;
                    documents?.Count.Should().BeGreaterThan(0);
                    documents?[0].Name.Should().Be("test1.pdf");
                    documents?[0].Location.Should().Be("http://some-place.com/test1.pdf");
                    documents?[0].FileSize.Should().Be("1000");
                });
        }
    }
}
