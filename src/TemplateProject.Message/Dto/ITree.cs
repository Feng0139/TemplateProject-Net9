using System.ComponentModel.DataAnnotations.Schema;

namespace TemplateProject.Message.Dto;

public interface ITree<TKey, TEntity> where TEntity : class
{
    TKey Id { get; set; }
    
    TKey Pid { get; set; }
    
    [NotMapped]
    List<TEntity> Children { get; set; }
}

public interface ITreeEntityAudit<TKey, TEntity> : ITree<TKey, TEntity>, IEntityAudit<TKey>
    where TEntity : class
{
    new TKey Id { get; set; }

    new TKey Pid { get; set; }
    
    [NotMapped] 
    new List<TEntity> Children { get; set; }
}