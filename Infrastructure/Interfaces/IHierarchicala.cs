using System.Collections.Generic;

namespace Infrastructure.Interfaces
{
    public interface IHierarchical
    {
        public string FullName { get; }
        public List<IHierarchical> ChildCollection { get; }
    }
}
