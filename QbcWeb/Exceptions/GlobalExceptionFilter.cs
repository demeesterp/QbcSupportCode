﻿using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QbcWeb.Exceptions
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public GlobalExceptionFilter()
        {
        }

        public void OnException(ExceptionContext context)
        {
            
        }

    }
}
