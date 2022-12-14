using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Keepers.Models;
using Keepers.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepers.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly AccountService _accountService;
    private readonly VaultsService _vaultsService;

    public AccountController(AccountService accountService, VaultsService vaultsService)
    {
      _accountService = accountService;
      _vaultsService = vaultsService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountService.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [HttpGet("{vaults}")]
    [Authorize]
    public async Task<ActionResult<List<Vault>>> GetVaultsByAccount()
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        string creatorId = user.Id;
        List<Vault> vaults = _vaultsService.GetVaultsByAccount(creatorId);
        return Ok(vaults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }



}