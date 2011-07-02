using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Linq;
using PR.Chat.Core.BusinessObjects;

namespace PR.Chat.DAL.EF
{
    public partial class ChatDB : IDatabase
    {
        private readonly ConcurrentDictionary<Type, IQueryable<IEntity>> _sources = new ConcurrentDictionary<Type, IQueryable<IEntity>>();

        private readonly static IDictionary<string, string> EntitySetNames = new Dictionary<string, string>();

        public IQueryable<TEntity> GetSource<TEntity>() where TEntity : class, IEntity
        {
            return (IQueryable<TEntity>)_sources.GetOrAdd(typeof(TEntity), CreateObjectSet<TEntity>());
        }

        public void DeleteOnSubmit(IEntity entity)
        {
            DeleteObject(entity);
        }

        public void AddOnSubmit(IEntity entity)
        {
            AddObject(GetEntitySetName(entity.GetType()), entity);
        }

        public void AddAllOnSubmit(IEnumerable<IEntity> entities)
        {
            for (var enumerator = entities.GetEnumerator(); enumerator.MoveNext();)
                AddOnSubmit(enumerator.Current);
        }

        public void SubmitChanges()
        {
            SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
        }

        public string GetEntitySetName(Type entitySetType)
        {
            lock (EntitySetNames)
            {
                if (EntitySetNames.ContainsKey(entitySetType.FullName))
                {
                    return EntitySetNames[entitySetType.FullName];
                }

                var container = MetadataWorkspace.GetEntityContainer(DefaultContainerName, DataSpace.CSpace);

                var entitySetName = (from meta in container.BaseEntitySets
                                     where meta.BuiltInTypeKind == BuiltInTypeKind.EntitySet &&
                                     meta.ElementType.Name == entitySetType.Name
                                     select meta.Name).First();

                EntitySetNames.Add(entitySetType.FullName, entitySetName);

                return entitySetName;
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                for (var enumerator = _sources.GetEnumerator(); enumerator.MoveNext();)
                {
                    var disposable = enumerator.Current.Value as IDisposable;
                    if (disposable != null)
                        disposable.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}