using System.Collections.Generic;
using AutoMapper;

namespace DT.PCP.Utils.Impl
{
    public class EntityCreator:IEntityCreator
    {
        #region Implementation of IEntityCreator

        public TEntity Create<TEntity, TDto>(TDto dto)
        {
            return Mapper.Map<TDto, TEntity>(dto);
        }

        public IEnumerable<TEntity> Create<TEntity, TDto>(IEnumerable<TDto> dto)
        {
            return Mapper.Map<IEnumerable<TDto>, IEnumerable<TEntity>>(dto);
        }

        #endregion
    }
}
