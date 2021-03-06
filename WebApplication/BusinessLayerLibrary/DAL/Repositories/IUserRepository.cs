﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayerLibrary.Domain.Model;

namespace BusinessLayerLibrary.DAL.Repositories
{
    public interface IUserRepository: IRepository<User>
    {
        User GetByID(Int32 id);
        User GetByLogin(String login);
    }
}
