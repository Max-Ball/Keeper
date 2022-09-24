using System;
using Keepers.Models;
using Keepers.Repositories;

namespace Keepers.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _profilesRepo;

    public ProfilesService(ProfilesRepository profilesRepo)
    {
      _profilesRepo = profilesRepo;
    }

    internal Profile GetProfile(string profileId)
    {
      Profile profile = _profilesRepo.GetProfile(profileId);
      if (profile == null)
      {
        throw new Exception("There is no profile by that Id");
      }
      return profile;
    }
  }
}