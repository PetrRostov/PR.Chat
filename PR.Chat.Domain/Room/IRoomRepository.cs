﻿using System;
using System.Collections.Generic;
using PR.Chat.Infrastructure;

namespace PR.Chat.Domain
{
    public interface IRoomRepository : IRepository<Room, Guid>
    {
        Room GetByName(string name);
    }
    

}