﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Core.DataAccess.EntityFramework
{
    public class EFRepositoryBase<TContext,TEntity> : IEFRepositoryBase<TEntity> where TEntity:class,IEntity
    {


    }
}
