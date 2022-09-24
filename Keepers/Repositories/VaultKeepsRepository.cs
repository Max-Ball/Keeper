using System.Data;
using Dapper;
using Keepers.Models;

namespace Keepers.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
      string sql = @"
      INSERT INTO vaultkeeps
      (vaultId, keepId, creatorId)
      VALUES
      (@vaultId, @keepId, @creatorId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, newVaultKeep);
      newVaultKeep.Id = id;
      return newVaultKeep;
    }

    internal void Delete(int id)
    {
      string sql = @"
      DELETE FROM vaultkeeps
      WHERE id = @id
      ";
      _db.Execute(sql, new { id });
    }

  }
}