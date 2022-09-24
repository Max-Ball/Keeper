using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepers.Models;
using Keepers.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepers.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vaultsService;
    private readonly KeepsService _keepsService;

    public VaultsController(VaultsService vaultsService, KeepsService keepsService)
    {
      _vaultsService = vaultsService;
      _keepsService = keepsService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault newVault)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        newVault.CreatorId = user.Id;
        Vault vault = _vaultsService.CreateVault(newVault);
        vault.Creator = user;
        return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Vault>> GetOne(int id)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        Vault vault = _vaultsService.GetOne(id, user?.Id);
        return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{vaultId}/keeps")]
    public async Task<ActionResult<List<VaultKeepVM>>> GetKeepsByVaultId(int vaultId)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        List<VaultKeepVM> keeps = _keepsService.GetKeepsByVaultId(vaultId, user?.Id);
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Update(int id, [FromBody] Vault update)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        update.CreatorId = user.Id;
        update.Id = id;
        return Ok(_vaultsService.Update(user, update));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Delete(int id)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        string message = _vaultsService.Delete(user, id);
        return Ok(message);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }

}