using System.Collections.Generic;

namespace DocumentService.Api.DTO
{
    public class GetDocumentsResponse
    {
        public List<Document> Documents { get; set; } = new List<Document>();
    }
}
