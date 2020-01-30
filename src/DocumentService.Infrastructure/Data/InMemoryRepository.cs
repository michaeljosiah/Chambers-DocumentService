using System;
using System.Collections.Generic;
using System.Linq;
using DocumentService.Core.Interfaces;
using DocumentService.Domain.Model.DocumentAggregate;

namespace DocumentService.Infrastructure.Data
{
    public class InMemoryRepository : IRepository
    {
        private List<Document> documents;
        public InMemoryRepository()
        {
            documents = new List<Document>();
        }
        public void AddDocument(Document document)
        {
            documents.Add(document);
        }

        public void DeleteDocument(Guid documentId)
        {
            documents.Remove(documents.FirstOrDefault(x => x.Id == documentId));
        }

        public List<Document> GetDocuments()
        {
            return documents.OrderBy(x=>x.SortOrder).ToList();
        }
    }
}
