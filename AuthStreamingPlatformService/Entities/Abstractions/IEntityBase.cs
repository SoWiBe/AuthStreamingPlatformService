namespace TechDaily.Entities.Abstractions;

public interface IEntityBase<TId>
{
    TId? Id { get; set; }
}