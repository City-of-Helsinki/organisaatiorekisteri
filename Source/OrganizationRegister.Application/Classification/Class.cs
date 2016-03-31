using System;

namespace OrganizationRegister.Application.Classification
{
    internal class Class : IClass
    {
        public Class(Guid id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name must be given", "name");
            }
            if (id.Equals(Guid.Empty))
            {
                throw new ArgumentException("Id must be given.", "id");
            }

            Id = id;
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
    }
}
