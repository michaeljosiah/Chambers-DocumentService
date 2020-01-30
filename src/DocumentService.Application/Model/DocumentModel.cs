using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentService.Application.Model
{
   public class DocumentModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string FileSize { get; set; }
        public int SortOrder { get; set; }
    }
}
