using System;

namespace DocumentService.Domain.Model.DocumentAggregate
{
    public sealed class Document : AggregateRoot
    {
        public string Name { get; private set; }
        public string Location { get; private set; }
        public string FileSize { get; private set; }
        public int SortOrder { get; private set; }

        public void Create(string name, string location, string fileSize, int sortOrder)
        {
            Id = Guid.NewGuid();
            Name = name;
            Location = location;
            FileSize = fileSize;
            SortOrder = sortOrder;
        }

        public void Update(string name, string location, string fileSize, int sortOrder)
        {
            Name = name;
            Location = location;
            FileSize = fileSize;
            SortOrder = sortOrder;
        }

        public void Delete()
        {
            //Could add custom domain logic here to see if deletion is allowed. If not a custom exception would be thrown.
        }
    }
}
