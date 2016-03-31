using System;
using System.Collections.Generic;
using System.Linq;
using OrganizationRegister.Application.Classification;

namespace OrganizationRegister.AcceptanceTests.Infrastructure
{
    internal class ClassHelper
    {
        public static IHierarchicalClass GetClass(IReadOnlyCollection<IHierarchicalClass> classes, string className)
        {
            if (classes.Any(@class => @class.Name.Equals(className)))
            {
                return classes.Single(@class => @class.Name.Equals(className));
            }
            IReadOnlyCollection<IHierarchicalClass> childClasses = classes.SelectMany(@class => @class.SubClasses).ToList();
            if (childClasses.Any())
            {
                return GetClass(childClasses, className);
            }
            throw new ArgumentException(string.Format("Class '{0}' not found.", className));
        }
    }
}
