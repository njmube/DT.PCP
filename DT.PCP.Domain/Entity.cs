namespace DT.PCP.Domain
{
    /// <summary>
    /// Базовый класс для всех сущностей
    /// </summary>
    public abstract class Entity:IEntity
    {
        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Указывает, является ли сущность не сохраненной
        /// </summary>
        public virtual bool IsTransient()
        {
            return Id == default(int);
        }
    }
}
