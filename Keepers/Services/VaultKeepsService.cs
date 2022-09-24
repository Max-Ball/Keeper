using Keepers.Models;
using Keepers.Repositories;

namespace Keepers.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vkRepo;
    private readonly VaultsService _vaultsService;

    public VaultKeepsService(VaultKeepsRepository vkRepo, VaultsService vaultsService)
    {
      _vkRepo = vkRepo;
      _vaultsService = vaultsService;
    }

    internal VaultKeep Create(VaultKeep newVaultKeep, Account user)
    {
      // if (newVaultKeep.CreatorId != user.Id)
      // {
      //   throw new Exception("You cannot add this keep to another user's vault");
      // }
      VaultKeep vaultKeep = _vkRepo.Create(newVaultKeep);
      return vaultKeep;
    }

    internal string Delete(int id, string userId)
    {
      _vkRepo.Delete(id);
      return $"This vaultkeep has been deleted";
    }
  }
}