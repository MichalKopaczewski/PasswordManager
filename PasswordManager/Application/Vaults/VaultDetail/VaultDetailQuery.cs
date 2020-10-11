using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Application.Vaults.VaultDetail
{
    public class VaultDetailQuery : IRequest<VaultDetailVM>
    {
        public long VaultId { get; set; }
    }
}
