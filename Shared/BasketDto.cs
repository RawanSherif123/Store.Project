﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class BasketDto
    {
        public int Id { get; set; }
        public IEnumerable<BasketItemDto> Items { get; set; }
    }
}
