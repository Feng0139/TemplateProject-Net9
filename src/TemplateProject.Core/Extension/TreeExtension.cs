using TemplateProject.Message.Dto;

namespace TemplateProject.Core.Extension;

public static class TreeExtension
{
    public static List<TEntity> ConvertTree<TKey, TEntity>(this IEnumerable<TEntity> sourceList, TKey? rootPid = default)
        where TKey : notnull
        where TEntity : class, ITree<TKey, TEntity>
    {
        var tree = new List<TEntity>();

        var list = sourceList.ToList();
        var dict = list.ToDictionary(x => x.Id);

        foreach (var entity in list)
        {
            if (entity.Pid.Equals(rootPid) || !dict.TryGetValue(entity.Pid, out var parent))
            {
                tree.Add(entity);
                continue;
            }

            parent.Children ??= new List<TEntity>();
            parent.Children.Add(entity);
        }
        
        return tree;
    }
}