﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advena.Database
{
    public class AdvenaContext: DbContext
    {
        public AdvenaContext() : base("name=Advena") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Ignore<Basic>();
            modelBuilder.Entity<User>().Map<User>(x => { x.Requires("Deleted").HasValue(false); }).Ignore(x => x.Deleted);
            modelBuilder.Entity<Article>().Map<Article>(x => { x.Requires("Deleted").HasValue(false); }).Ignore(x => x.Deleted);
            modelBuilder.Entity<Comment>().Map<Comment>(x => { x.Requires("Deleted").HasValue(false); }).Ignore(x => x.Deleted);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(p => p.State == EntityState.Deleted))
            {
                SoftDelete(entry);
            }
            return base.SaveChanges();
        }

        private void SoftDelete(DbEntityEntry entry)
        {
            Type entryEntityType = entry.Entity.GetType();

            string tableName = GetTableName(entryEntityType);
            string primaryKeyName = GetPrimaryKeyName(entryEntityType);
            string deletequery = string.Format("UPDATE {0} SET Deleted = 1 WHERE {1} = @id", tableName, primaryKeyName);

            Database.ExecuteSqlCommand(deletequery, new SqlParameter("@id", entry.OriginalValues[primaryKeyName]));
            entry.State = EntityState.Unchanged;
        }

        private static Dictionary<Type, EntitySetBase> _mappingCache =
                    new Dictionary<Type, EntitySetBase>();

        private EntitySetBase GetEntitySet(Type type)
        {
            if (!_mappingCache.ContainsKey(type))
            {
                ObjectContext octx = ((IObjectContextAdapter)this).ObjectContext;

                string typeName = ObjectContext.GetObjectType(type).Name;

                var es = octx.MetadataWorkspace
                                .GetItemCollection(DataSpace.SSpace)
                                .GetItems<EntityContainer>()
                                .SelectMany(c => c.BaseEntitySets.Where(e => e.Name == typeName))
                                .FirstOrDefault();

                if (es == null) throw new ArgumentException("Entity type not found in GetTableName", typeName);

                _mappingCache.Add(type, es);
            }

            return _mappingCache[type];
        }

        private string GetTableName(Type type)
        {
            EntitySetBase es = GetEntitySet(type);
            return string.Format("[{0}].[{1}]", es.MetadataProperties["Schema"].Value, es.MetadataProperties["Table"].Value);
        }

        private string GetPrimaryKeyName(Type type)
        {
            EntitySetBase es = GetEntitySet(type);
            return es.ElementType.KeyMembers[0].Name;
        }
    }
}
