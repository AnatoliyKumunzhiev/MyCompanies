using System;

namespace Infrastructure.Interfaces
{
    public interface IHasId
    {
        Guid Id { get; set; }
    }
}
