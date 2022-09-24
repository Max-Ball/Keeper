using System;
using System.Collections.Generic;
using Keepers.Models;
using Keepers.Repositories;

namespace Keepers.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _KeepsRepo;
    private readonly VaultsService _vaultsService;

    public KeepsService(KeepsRepository keepsRepo, VaultsService vaultsService)
    {
      _KeepsRepo = keepsRepo;
      _vaultsService = vaultsService;
    }

    internal List<Keep> GetAll()
    {
      return _KeepsRepo.GetAll();
    }

    internal Keep GetOne(int id)
    {
      Keep keep = _KeepsRepo.GetOne(id);
      if (keep == null)
      {
        throw new Exception("No keep at that id");
      }
      return keep;
    }

    internal Keep CreateKeep(Keep newKeep)
    {
      return _KeepsRepo.CreateKeep(newKeep);
    }

    internal object Update(Account user, Keep update)
    {

      Keep original = GetOne(update.Id);
      if (user.Id != original.CreatorId)
      {
        throw new Exception("This is not your keep to edit");
      }

      original.Name = update.Name ?? original.Name;
      original.Description = update.Description ?? original.Description;
      original.Img = update.Img ?? original.Img;

      return _KeepsRepo.Update(original);
    }

    internal string Delete(string userId, int id)
    {
      Keep original = GetOne(id);
      if (userId != original.CreatorId)
      {
        throw new Exception($"{original.Name} is not your keep to delete");
      }
      _KeepsRepo.Delete(id);
      return $"The {original.Name} has been deleted";
    }


    internal List<VaultKeepVM> GetKeepsByVaultId(int vaultId, string userId)
    {
      Vault vault = _vaultsService.GetOne(vaultId, userId);
      List<VaultKeepVM> keeps = _KeepsRepo.GetKeepsByVaultId(vaultId);
      if (keeps == null)
      {
        throw new Exception("This vault does not have any keeps");
      }
      return keeps;
    }

    internal List<Keep> GetKeepsByProfileId(string profileId)
    {
      List<Keep> keeps = _KeepsRepo.GetKeepsByProfileId(profileId);
      return keeps;
    }
  }
}