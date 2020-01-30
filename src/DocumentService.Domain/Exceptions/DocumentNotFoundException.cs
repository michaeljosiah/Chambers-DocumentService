using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentService.Domain.Exceptions
{
    public class DocumentNotFoundException : Exception
    {
        public DocumentNotFoundException(Guid documentId) :
            base($"Document {documentId} not found")
        {
        }

    }
}
