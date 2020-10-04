using MediatR;

using PasswordManager.Infrastructure.Persistance;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Application
{
    public class BaseRequestHandler
    {
        protected readonly PasswordManagerContext PmContext;
        public BaseRequestHandler(PasswordManagerContext context)
        {
            PmContext = context;
        }
    }
}
