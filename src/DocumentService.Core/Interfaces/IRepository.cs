using System;
using System.Collections.Generic;
using DocumentService.Domain.Model.DocumentAggregate;

namespace DocumentService.Core.Interfaces
{
    public interface IRepository
    {
        List<Document> GetDocuments();
        void DeleteDocument(Guid documentId);
        void AddDocument(Document document);
    }
}
