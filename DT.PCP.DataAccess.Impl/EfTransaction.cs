using System.Transactions;

namespace DT.PCP.DataAccess.Impl
{
    /// <summary>
    /// Представляет транзакцию для Entity Framework
    /// </summary>
    public class EfTransaction:IGenericTransaction
    {
        #region Private fields

        protected EfRepository _repository { get; private set; }
        protected TransactionScope TransactionScope { get; private set; }

        #endregion

        #region Constructors

        public EfTransaction(EfRepository repository)
        {
            this._repository = repository;
            this.TransactionScope = new TransactionScope();
        }

        #endregion

        #region Implementation of IGenericTransaction

        /// <summary>
        /// Фиксация транзакции 
        /// </summary>
        public void Commit()
        {
            this._repository.SaveChanges();
            this.TransactionScope.Complete();

        }

        /// <summary>
        /// Откат транзакции 
        /// </summary>
        /// <remarks>Для Entity Framework нет необходимости в реализации. 
        /// Транзакция откатится автоматически при вызове метода Dispose 
        /// </remarks>
        public void RollBack()
        {
            
        }

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            if (this.TransactionScope != null)
            {
                this.TransactionScope.Dispose();
                this.TransactionScope = null;
                this._repository = null;
            }

        }

        #endregion
    }
}
