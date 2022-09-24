using System.Data;
using System.Linq;
using Dapper;
using Keepers.Models;

namespace Keepers.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Profile GetProfile(string id)
    {
      string sql = @"
      SELECT *
      FROM accounts
      WHERE id = @id
      ";
      Profile profile = _db.Query<Profile>(sql, new { id }).FirstOrDefault();
      return profile;
    }
  }
}