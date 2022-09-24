using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepers.Models;

namespace Keepers.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Vault CreateVault(Vault newVault)
    {
      string sql = @"
      INSERT INTO vaults
      (name, description, isPrivate, img, creatorId)
      VALUES
      (@name, @description, @isPrivate, @img, @creatorId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, newVault);
      newVault.Id = id;
      return newVault;
    }

    internal Vault GetOne(int id)
    {
      string sql = @"
      SELECT 
        v.*,
        a.*
        FROM vaults v
        JOIN accounts a ON v.creatorId = a.id
        WHERE v.Id = @id ;
      ";
      Vault vault = _db.Query<Vault, Profile, Vault>(sql, (vault, account) =>
      {
        vault.Creator = account;
        return vault;
      }, new { id }).FirstOrDefault();
      return vault;
    }

    internal object Update(Vault original)
    {
      string sql = @"
      UPDATE vaults SET 
      name = @name,
      description = @description,
      isPrivate = @isPrivate,
      img = @img
      WHERE id = @id;
      ";
      int rowsAffected = _db.Execute(sql, original);
      if (rowsAffected == 0)
      {
        throw new Exception("unable to edit this vault");
      }
      return original;
    }

    internal void Delete(int id)
    {
      string sql = @"
      DELETE FROM vaults
      WHERE id = @id
      ";
      _db.Execute(sql, new { id });
    }


    internal List<Vault> GetVaultsByAccount(string creatorId)
    {
      string sql = @"
      SELECT
      v.*,
      a.*
      FROM vaults v
      JOIN accounts a ON v.creatorId = a.id
      WHERE v.creatorId = @creatorId;
      ";
      List<Vault> vaults = _db.Query<Vault, Account, Vault>(sql, (v, a) =>
      {
        v.Creator = a;
        return v;
      }, new { creatorId }).ToList();
      return vaults;
    }

    internal List<Vault> GetVaultsByProfileId(string profileId)
    {
      string sql = @"
      SELECT
      v.*,
      a.*
      FROM vaults v
      JOIN accounts a ON v.creatorId = a.id
      WHERE v.creatorId = @profileId;
      ";
      List<Vault> vaults = _db.Query<Vault, Profile, Vault>(sql, (vault, account) =>
      {
        vault.Creator = account;
        return vault;
      }, new { profileId }).ToList();
      return vaults;
    }
  }
}