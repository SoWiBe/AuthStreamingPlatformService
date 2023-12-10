namespace AuthStreamingPlatformService.Entities.Abstractions;

public interface IEntityBase<TId>
{
    TId? Id { get; set; }
}