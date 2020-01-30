using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentService.Api.DTO
{
    public class Document
    {
        public Guid documentId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string FileSize { get; set; }
        public int SortOrder { get; set; }
    }
}
