using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Application.Vaults.VaultsList
{
    public class VaultsListQuery : IRequest<IEnumerable<VaultItemVM>>
    {

    }
}
