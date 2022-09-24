using System;
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
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vkService;

    public VaultKeepsController(VaultKeepsService vkService)
    {
      _vkService = vkService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeepVM>> Create([FromBody] VaultKeep newVaultKeep)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        newVaultKeep.CreatorId = user.Id;
        VaultKeep vaultKeep = _vkService.Create(newVaultKeep, user);
        return Ok(newVaultKeep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<VaultKeepVM>> Delete(int id)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        string message = _vkService.Delete(id, user.Id);
        return Ok(message);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }



  }

}