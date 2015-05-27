using System;
using System.Linq;
using DT.PCP.Domain;

namespace DT.PCP.DataAccess
{
    /// <summary>
    /// Представляет обобщенный репозиторий для объектов
    /// </summary>
    public interface IRepository : IDisposable
    {
        /// <summary>
        /// Возвращает объект IQueryable
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <returns>Объект IQueryable</returns>
        IQueryable<T> Query<T>() where T : class, IEntity;

        /// <summary>
        /// Возвращает объект IQueryable
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <returns>Объект IQueryable</returns>
        IQueryable<T> NotTrackedQuery<T>() where T : class, IEntity;

        /// <summary>
        /// Фиксация изменений
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Сохраняет сущность
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <param name="entity">Сущность</param>
        void Save<T>(T entity) where T : class, IEntity;

        /// <summary>
        /// Обновляет сущность
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <param name="entity">Сущность</param>
        void Update<T>(T entity) where T : class, IEntity;

        /// <summary>
        /// Удаляет сущность
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <param name="entity">Сущность</param>
        void Delete<T>(T entity) where T : class, IEntity;

        /// <summary>
        /// Удаляет сущность по идентификатору
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <param name="entityId">Идентификатор сущности</param>
        void DeleteById<T>(int entityId) where T : class, IEntity;

        /// <summary>
        /// Начинает транзакцию
        /// </summary>
        /// <returns></returns>
        IGenericTransaction BeginTransaction();

        /// <summary>
        /// Заканчивает транзакцию
        /// </summary>
        /// <param name="transaction">Транзакция</param>
        void EndTransaction(IGenericTransaction transaction);

    }
}
