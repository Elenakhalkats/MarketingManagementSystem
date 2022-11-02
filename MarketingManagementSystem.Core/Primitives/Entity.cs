namespace MarketingManagementSystem.Core.Primitives;
[Serializable]
public abstract class Entity<TId>
{
    public virtual TId Id { get; set; }
}
