using System.Collections.Generic;
using AutoMapper;

namespace DT.PCP.Utils.Impl
{
    public class ViewModelCreator:IViewModelCreator
    {
        #region Implementation of IViewModelCreator

        public TViewModel Create<TViewModel, TEntity>(TEntity entity)
        {
            return Mapper.Map<TEntity, TViewModel>(entity);
        }

        public IEnumerable<TViewModel> Create<TViewModel, TEntity>(IEnumerable<TEntity> entities)
        {
            return Mapper.Map<IEnumerable<TEntity>, IEnumerable<TViewModel>>(entities);
        }

        #endregion
    }
}
