using System;
using System.Collections.Generic;
using Keepers.Models;
using Keepers.Repositories;

namespace Keepers.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _vaultsRepo;

    public VaultsService(VaultsRepository vaultsRepo)
    {
      _vaultsRepo = vaultsRepo;
    }

    internal Vault CreateVault(Vault newVault)
    {
      return _vaultsRepo.CreateVault(newVault);
    }

    internal Vault GetOne(int id, string userId)
    {
      Vault vault = _vaultsRepo.GetOne(id);
      if (vault == null)
      {
        throw new Exception("No vault by that id");
      }

      if (vault.IsPrivate == true && vault.CreatorId != userId)
      {

      }
      return vault;
    }

    internal object Update(Account user, Vault update)
    {
      Vault original = GetOne(update.Id, user.Id);
      original.Name = update.Name ?? original.Name;
      original.Description = update.Description ?? original.Description;
      original.IsPrivate = update.IsPrivate ?? original.IsPrivate;
      original.Img = update.Img ?? original.Img;

      if (user.Id != update.CreatorId)
      {
        throw new Exception("This is not your vault to edit");
      }
      return _vaultsRepo.Update(original);
    }

    internal string Delete(Account user, int id)
    {
      Vault vault = GetOne(id, user.Id);
      if (user.Id != vault.CreatorId)
      {
        throw new Exception("This is not your vault to delete");
      }
      _vaultsRepo.Delete(id);
      return $"The {vault.Name} has been deleted";
    }

    internal List<Vault> GetVaultsByAccount(string creatorId)
    {
      return _vaultsRepo.GetVaultsByAccount(creatorId);
    }

    internal List<Vault> GetVaultsByProfileId(string profileId, Account user)
    {
      List<Vault> vaults = _vaultsRepo.GetVaultsByProfileId(profileId);
      return vaults;
    }
  }
}