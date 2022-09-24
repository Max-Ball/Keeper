using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepers.Models;

namespace Keepers.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Keep> GetAll()
    {
      string sql = @"
      SELECT 
      k.*,
      a.*
      FROM keeps k
      JOIN accounts a ON a.id = k.creatorId;
      ";
      List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, account) =>
      {
        keep.Creator = account;
        return keep;
      }).ToList();
      return keeps;
    }

    internal Keep GetOne(int id)
    {
      string sql = @"
      SELECT 
      k.*,
      a.*
      FROM keeps k
      JOIN accounts a ON a.id = k.creatorId
      WHERE k.id = @id;
      ";
      Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, account) =>
      {
        keep.Creator = account;
        return keep;
      }, new { id }).FirstOrDefault();
      return keep;
    }

    internal Keep CreateKeep(Keep newKeep)
    {
      string sql = @"
      INSERT INTO keeps
      (name, description, img, creatorId)
      VALUES
      (@name, @description, @img, @creatorId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, newKeep);
      newKeep.Id = id;
      return newKeep;
    }

    internal void Delete(int id)
    {
      string sql = @"
      DELETE FROM keeps
      WHERE id = @id;
      ";
      _db.Execute(sql, new { id });
    }

    internal List<VaultKeepVM> GetKeepsByVaultId(int vaultId)
    {
      string sql = @"
      SELECT
      vk.*,
      k.*,
      a.*
      FROM vaultkeeps vk
      JOIN keeps k ON vk.keepId = k.id
      JOIN vaults v ON vk.vaultId = v.id
      JOIN accounts a ON vk.creatorId = a.id
      WHERE vk.vaultId = @vaultId;
      ";
      List<VaultKeepVM> vaultKeeps = _db.Query<VaultKeep, VaultKeepVM, Account, VaultKeepVM>(sql, (vk, k, a) =>
      {
        k.VaultKeepId = vk.Id;
        k.Creator = a;
        return k;
      }, new { vaultId }).ToList();
      return vaultKeeps;
    }

    internal object Update(Keep original)
    {
      string sql = @"
      UPDATE keeps SET
      name = @name,
      img = @img,
      views = @views,
      kept = @kept,
      description = @description
      WHERE id = @id;
      ";
      int rowsAffected = _db.Execute(sql, original);
      if (rowsAffected == 0)
      {
        throw new Exception("unable to edit this keep");
      }
      return original;
    }

    internal List<Keep> GetKeepsByProfileId(string profileId)
    {
      string sql = @"
      SELECT 
      k.*,
      a.*
      FROM keeps k
      JOIN accounts a ON a.id = k.creatorId
      WHERE k.creatorId = @profileId
      ";
      List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, account) =>
      {
        keep.Creator = account;
        return keep;
      }, new { profileId }).ToList();
      return keeps;
    }
  }
}