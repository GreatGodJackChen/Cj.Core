using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CJ.Domain
{
    public class EfCoreRepository<TDbContext, TEntity>
       : EfCoreRepository<TDbContext,TEntity, string>, IRepository<TEntity>
       where TEntity : class, IEntity
        where TDbContext : DbContext
    {
        public EfCoreRepository(IUnitOfWork<TDbContext> unitOfWork) : base(unitOfWork)
        {
        }
    }
    //public class EfCoreRepository<TDbContext, TEntity> : EfCoreRepository<TDbContext, TEntity, string>, IRepository<TEntity>
    //     where TEntity : class, IEntity<string>
    //    where TDbContext : DbContext
    //{
    //    public EfCoreRepository(IUnitOfWork<TDbContext> unitOfWork)
    //       : base(unitOfWork)
    //    {
    //    }
    //}
    public class EfCoreRepository<TDbContext,TEntity, TPrimaryKey>
       : Repository<TEntity, TPrimaryKey>, IRepositoryWithDbContext
       where TEntity : class, IEntity<TPrimaryKey>
                where TDbContext : DbContext
    {
        private readonly DbContext _dbContext;

        public virtual DbSet<TEntity> Table => _dbContext.Set<TEntity>();

        public IUnitOfWork<TDbContext> _unitOfWork;
        public EfCoreRepository(IUnitOfWork<TDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dbContext = _unitOfWork.Get();
        }

        public override IQueryable<TEntity> GetAll()
        {
            return Table.AsQueryable();
        }

        public override TEntity Insert(TEntity entity)
        {
            var newEntity = Table.Add(entity).Entity;
            //_dbContext.SaveChanges();
            return newEntity;
        }

        public override TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;

            //_dbContext.SaveChanges();

            return entity;
        }

        public override void Delete(TEntity entity)
        {
            AttachIfNot(entity);
            Table.Remove(entity);

            //_dbContext.SaveChanges();
        }

        public override void Delete(TPrimaryKey id)
        {
            var entity = GetFromChangeTrackerOrNull(id);
            if (entity != null)
            {
                Delete(entity);
                return;
            }

            entity = FirstOrDefault(id);
            if (entity != null)
            {
                Delete(entity);
                return;
            }
        }

        public DbContext GetDbContext()
        {
            return _dbContext;
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            var entry = _dbContext.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return;
            }

            Table.Attach(entity);
        }

        private TEntity GetFromChangeTrackerOrNull(TPrimaryKey id)
        {
            var entry = _dbContext.ChangeTracker.Entries()
                .FirstOrDefault(
                    ent =>
                        ent.Entity is TEntity &&
                        EqualityComparer<TPrimaryKey>.Default.Equals(id, ((TEntity)ent.Entity).Id)
                );

            return entry?.Entity as TEntity;
        }
    }
}
