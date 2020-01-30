using System;
using System.Collections.Generic;
using DocumentService.Application.Model;

namespace DocumentService.Application.Services
{
    public interface IDocumentService
    {
        List<DocumentModel> GetDocuments();
        void DeleteDocument(Guid documentId);
        void AddDocument(DocumentModel document);
        DocumentModel GetDocumentById(Guid documentId);
    }
}