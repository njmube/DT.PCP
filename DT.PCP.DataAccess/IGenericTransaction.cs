using System;

namespace DT.PCP.DataAccess
{
    /// <summary>
    /// Представляет транзакцию
    /// </summary>
    public interface IGenericTransaction : IDisposable
    {
        /// <summary>
        /// Фиксирует транзакцию
        /// </summary>
        void Commit();

        /// <summary>
        /// Откатывает транзакцию
        /// </summary>
        void RollBack();
    }
}
