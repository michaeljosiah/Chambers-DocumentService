using System;
using System.IO;
using System.Linq;
using DocumentService.Api.DTO;
using DocumentService.Application.Model;
using DocumentService.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DocumentService.Api.Controllers
{
    [Route("api/[controller]")]
    public class DocumentsController : Controller
    {
        private readonly IDocumentService documentService;
        public DocumentsController(IDocumentService documentService)
        {
          this.documentService = documentService ?? throw new ArgumentException(nameof(documentService));
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<GetDocumentsResponse> GetDocuments()
        {
            var response = new GetDocumentsResponse();
            var documents = documentService.GetDocuments();
            response.Documents = documents.Select(x => new Document
            {
                Location = x.Location,
                Name = x.Name,
                FileSize = x.FileSize,
                documentId = x.Id,
                SortOrder = x.SortOrder
            }).ToList();

            return response;

        }

        [HttpGet("{documentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<GetDocumentResponse> GetDocumentById(Guid documentId)
        {
            try
            {
                var document = documentService.GetDocumentById(documentId);
                return Ok(new GetDocumentResponse
                {
                    Document = new Document
                    {
                        documentId = document.Id,
                        SortOrder = document.SortOrder,
                        Name = document.Name,
                        Location = document.Location,
                        FileSize = document.FileSize
                    }
                });
            }
            catch
            {
                return NotFound();
            }
        }

        // POST api/<controller>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult AddDocument(int sortOrder,[FromForm] IFormFile document)
        {
            if(document == null)
            {
                return BadRequest("Document to upload is required.");
            }

            //Reviewer : In future I would check the content type and move this out as a fluent validation.
            if (!Path.GetExtension(document.FileName).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Invalid file type");
            }

            documentService.AddDocument(new DocumentModel
            {
                SortOrder = sortOrder,
                FileSize = document.Length.ToString(),
                Name = document.FileName
            });

            return Ok();
        }

        [HttpDelete("{documentId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Delete(Guid documentId)
        {
            //Reviewer : I would normally bubble up exceptions from the domain and have a custom filter return
            // right status response
            try
            {
                documentService.DeleteDocument(documentId);
                return NoContent();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
