using System;
using System.Collections.Generic;
using System.Linq;
using DocumentService.Application.Model;
using DocumentService.Core.Interfaces;
using DocumentService.Domain.Exceptions;
using DocumentService.Domain.Model.DocumentAggregate;

namespace DocumentService.Application.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IRepository repository;
        public DocumentService(IRepository repository)
        {
            this.repository = repository ?? throw new ArgumentException(nameof(repository));
        }
        public List<DocumentModel> GetDocuments()
        {
           return repository.GetDocuments().Select(x=> new DocumentModel
            {
                SortOrder = x.SortOrder,
                Location = x.Location,
                Name = x.Name,
                FileSize = x.FileSize,
                Id = x.Id
            }).ToList();
        }

        public DocumentModel GetDocumentById(Guid documentId)
        {
            var document = repository.GetDocuments().FirstOrDefault(x => x.Id == documentId);
            if (document == null) throw new DocumentNotFoundException(documentId);
            return new DocumentModel
            {
                Id = document.Id,
                SortOrder = document.SortOrder,
                Name = document.Name,
                Location = document.Location,
                FileSize = document.FileSize
            };
        }


        public void DeleteDocument(Guid documentId)
        {
            var doc = repository.GetDocuments().FirstOrDefault(x => x.Id == documentId);
            if(doc == null) throw new DocumentNotFoundException(documentId);
            doc.Delete();
            repository.DeleteDocument(documentId);
        }

        public void AddDocument(DocumentModel document)
        {
            var doc = new Document();
            doc.Create(document.Name,$"http://someplace-out-there/{document.Name}",document.FileSize,document.SortOrder);
            repository.AddDocument(doc);
        }
    }
}
