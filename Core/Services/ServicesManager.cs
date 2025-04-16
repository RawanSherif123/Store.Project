﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Services.Abstractions;

namespace Services
{
    public class ServicesManager(IUnitOfWork unitOfWork , IMapper mapper) : IServicesManager
    {
        public IProductService productService { get; } = new ProductService (unitOfWork , mapper);
    }
}
