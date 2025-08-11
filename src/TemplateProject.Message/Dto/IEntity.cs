namespace TemplateProject.Message.Dto;

public interface IEntityBase { }

public interface IEntity<TKey> : IEntityBase
{
    TKey Id { get; set; }
}

public interface IEntityAudit<TKey> : IEntity<TKey>, IEntityCreated, IEntityModified
{
    public Guid CreatedBy { get; set; }

    public Guid? LastModifiedBy { get; set; }
}

public interface IEntityCreated : IEntityBase
{
    public DateTimeOffset CreatedAt { get; set; }
}

public interface IEntityModified : IEntityBase
{
    public DateTimeOffset? UpdatedAt { get; set; }
}