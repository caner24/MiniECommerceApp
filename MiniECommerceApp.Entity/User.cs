﻿using Microsoft.AspNetCore.Identity;
using MiniECommerceApp.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity
{
    public class User:IdentityUser,IEntity
    {
    }
}
