using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepers.Models;
using Keepers.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepers.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _profilesService;
    private readonly KeepsService _keepsService;
    private readonly VaultsService _vaultsService;

    public ProfilesController(ProfilesService profilesService, KeepsService keepsService, VaultsService vaultsService)
    {
      _profilesService = profilesService;
      _keepsService = keepsService;
      _vaultsService = vaultsService;
    }

    [HttpGet("{profileId}")]
    public ActionResult<Profile> GetProfile(string profileId)
    {
      try
      {
        Profile profile = _profilesService.GetProfile(profileId);
        return Ok(profile);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{profileId}/keeps")]
    public ActionResult<List<Keep>> GetKeepsByProfileId(string profileId)
    {
      try
      {
        List<Keep> keeps = _keepsService.GetKeepsByProfileId(profileId);
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{profileId}/vaults")]
    public async Task<ActionResult<List<Vault>>> GetVaultsByProfileId(string profileId)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        List<Vault> vaults = _vaultsService.GetVaultsByProfileId(profileId, user?.Id);
        return Ok(vaults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}