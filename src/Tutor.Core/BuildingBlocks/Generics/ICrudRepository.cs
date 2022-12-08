﻿using System.Collections.Generic;

namespace Tutor.Core.BuildingBlocks.Generics;

public interface ICrudRepository<TEntity> where TEntity : Entity
{
    PagedResult<TEntity> GetPaged(int page, int pageSize);
    List<TEntity> GetAll();
    TEntity Get(int id);
    TEntity Create(TEntity entity);
    void Update(TEntity entity);
    void Delete(int id);
}