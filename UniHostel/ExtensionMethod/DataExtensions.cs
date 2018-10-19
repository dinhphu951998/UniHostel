using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniHostel.ExtensionMethod
{
    public static class DataExtensions
    {
        public static void Remove<TEntity>(
            this System.Data.Entity.DbSet<TEntity> entities,
            System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            var records = entities
                .Where(predicate)
                .ToList();
            if (records.Count > 0)
                entities.RemoveRange(records);
        }
    }
}