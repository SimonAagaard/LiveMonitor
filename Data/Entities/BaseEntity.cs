﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public abstract class BaseEntity : IEntity
    {
        public Guid Id { get; set; }
    }
}
