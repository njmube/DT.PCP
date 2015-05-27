using System.Collections.Generic;

namespace DT.PCP.Utils
{
    public interface IEntityCreator
    {
        TEntity Create<TEntity, TDto>(TDto dto);
        IEnumerable<TEntity> Create<TEntity, TDto>(IEnumerable<TDto> dto);
    }
}
