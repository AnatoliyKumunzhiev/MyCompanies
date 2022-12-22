using Infrastructure.Enums;

namespace Infrastructure.Interfaces
{
    public interface IHasEntityState
    {
        EntityState PmEntityState { get; set; }
    }
}
