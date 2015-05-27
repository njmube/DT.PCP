using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.Objects;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using DT.PCP.Domain;

namespace DT.PCP.DataAccess.Impl
{
    public class EfRepository : IRepository
    {
        #region Private fields

        private PcpContext _context;

        #endregion


        #region Constructors

        public EfRepository(PcpContext context)
        {
            _context = context;
        }

        #endregion


        #region Implementation of IRepository

        /// <summary>
        /// Возвращает объект IQueryable
        ///  </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <returns>Оьъект IQueryable</returns>
        public IQueryable<T> Query<T>() where T : class, IEntity
        {
            return _context.Set<T>();
        }

        /// <summary>
        /// Возвращает объект IQueryable
        ///  </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <returns>Оьъект IQueryable</returns>
        public IQueryable<T> NotTrackedQuery<T>() where T : class, IEntity
        {
            return _context.Set<T>().AsNoTracking();
        }


        /// <summary>
        /// Добавление новой сущности
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <param name="entity">Сущность для сохранения в БД</param>
        public void Save<T>(T entity) where T : class, IEntity
        {
            _context.Set<T>().Add(entity);
        }
        
        /// <summary>
        /// Обновление сущности
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <param name="entity">Сущность дял обновления</param>
        public void Update<T>(T entity) where T : class, IEntity
        {
            if (entity == null)
            {
                throw new ArgumentException("Empty entity");
            }

            var entry = _context.Entry<T>(entity);

            if (entry.State == EntityState.Detached)
            {
                var set = _context.Set<T>();
                T attachedEntity = set.Find(entity.Id);

                if (attachedEntity != null)
                {
                    var attachedEntry = _context.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    entry.State = EntityState.Modified;
                }
            }
        }

        /// <summary>
        /// Удаление сущности
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <param name="entity">Сущность для удаления</param>
        public void Delete<T>(T entity) where T : class, IEntity
        {
            if (entity == null)
            {
                throw new ArgumentException("Empty entity");
            }

            var entry = _context.Entry<T>(entity);

            if (entry.State == EntityState.Detached)
            {
                var set = _context.Set<T>();
                T attachedEntity = set.Find(entity.Id);

                if (attachedEntity != null)
                {
                    var attachedEntry = _context.Entry(attachedEntity);
                    attachedEntry.State = EntityState.Deleted;
                }
            }
        }

        /// <summary>
        /// Удаление сущности по идентификатору
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <param name="entityId">Идентификатор сущности для удаления</param>
        public void DeleteById<T>(int entityId) where T : class, IEntity
        {
            var entity = _context.Set<T>().Find(entityId);
            _context.Entry<T>(entity).State = EntityState.Deleted;
        }

        /// <summary>
        /// Фиксация изменений в контексте
        /// </summary>
        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                string error = "";
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                       error += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
        }

        /// <summary>
        /// Начинает новую транзакцию
        /// </summary>
        /// <returns></returns>
        public IGenericTransaction BeginTransaction()
        {
            return new EfTransaction(this);
        }

        /// <summary>
        /// Заканчивает транказцию
        /// </summary>
        /// <param name="transaction">Транзакция</param>
        public void EndTransaction(IGenericTransaction transaction)
        {
            if (transaction != null)
            {
                transaction.Dispose();
                transaction = null;
            }
        }

        /// <summary>
        /// Освобождает ресурсы
        /// </summary>
        public void Dispose()
        {
            if (this._context != null)
            {
                
                (this._context as IDisposable).Dispose();
                this._context = null;
            }
        }

        #endregion

    }
}
