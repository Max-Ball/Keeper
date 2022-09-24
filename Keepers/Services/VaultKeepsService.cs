using System;
using Keepers.Models;
using Keepers.Repositories;

namespace Keepers.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vkRepo;
    private readonly VaultsService _vaultsService;
    private readonly KeepsService _keepsService;
    private readonly KeepsRepository _keepsRepo;

    public VaultKeepsService(VaultKeepsRepository vkRepo, VaultsService vaultsService, KeepsService keepsService, KeepsRepository keepsRepo)
    {
      _vkRepo = vkRepo;
      _vaultsService = vaultsService;
      _keepsService = keepsService;
      _keepsRepo = keepsRepo;
    }

    internal VaultKeep Create(VaultKeep newVaultKeep, Account user)
    {
      VaultKeep vaultKeep = _vkRepo.Create(newVaultKeep);
      Vault vault = _vaultsService.GetOne(newVaultKeep.VaultId, user.Id);
      if (vault.CreatorId != user.Id)
      {
        throw new Exception("You cannot add this keep to another user's vault");
      }
      Keep keep = _keepsService.GetOne(newVaultKeep.KeepId);
      keep.Kept++;
      _keepsRepo.Update(keep);
      return vaultKeep;
    }

    internal string Delete(int id, string userId)
    {
      VaultKeep vaultKeep = _vkRepo.GetOne(id);
      if (vaultKeep == null)
      {
        throw new Exception("There is no keep in this vault by that Id");
      }
      if (vaultKeep.CreatorId != userId)
      {
        throw new Exception("You cannot delete a keep that belongs to another user's vault");
      }
      Keep keep = _keepsService.GetOne(vaultKeep.KeepId);
      keep.Kept--;
      _keepsRepo.Update(keep);
      _vkRepo.Delete(id);
      return $"This vaultkeep has been deleted";
    }
  }
}