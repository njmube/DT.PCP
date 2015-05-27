using System.Collections.Generic;

namespace DT.PCP.Utils
{
    public interface IViewModelCreator
    {
        TViewModel Create<TViewModel, TEntity>(TEntity entity);
        IEnumerable<TViewModel> Create<TViewModel, TEntity>(IEnumerable<TEntity> entities);
    }
}
