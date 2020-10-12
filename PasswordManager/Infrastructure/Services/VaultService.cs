using Microsoft.VisualBasic;

using PasswordManager.Application.Interfaces;
using PasswordManager.Infrastructure.Persistance;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Infrastructure.Services
{
    public class VaultService : IVaultService
    {
        public PasswordManagerContext PmContext { get; }
        public UserResolverService UserResolverService { get; }
        public ICryptoService CryptoService { get; }

        public VaultService(PasswordManagerContext pmContext,UserResolverService userResolverService,ICryptoService cryptoService)
        {
            PmContext = pmContext;
            UserResolverService = userResolverService;
            CryptoService = cryptoService;
        }


        public bool ValidateVaultPassword(long vaultId,string password)
        {
            if (password==null)
            {
                throw new Exception("Nie podano hasła głównego");
            }
            var vault = (from v in PmContext.Vaults
                              where v.Id == vaultId && v.Username == UserResolverService.GetUsername()
                              select new { v.Id, v.MasterPassword, v.MasterSalt }
                             ).First();
            return CryptoService.ValidateHash(password, vault.MasterPassword, vault.MasterSalt);
        }
    }
}
