using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UnknownCampServer.API.Services;
using UnknownCampServer.Core.DTOs;
using UnknownCampServer.Core.Entities;
using UnknownCampServer.Core.Services;

namespace UnknownCampServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly JwtTokenService _jwtTokenService;

        public AccountController(IAccountService accountService, JwtTokenService jwtTokenService)
        {
            _accountService = accountService;
            _jwtTokenService = jwtTokenService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount(string id)
        {
            try
            {
                Account account = await _accountService.GetAccount(id);
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }


        [HttpPost("Create")]
        public async Task<IActionResult> CreateAccount([FromBody] AccountRegDTO accountDTO)
        {
            if (accountDTO == null)
            {
                return BadRequest(new { Message = "Account data is null" } );
            }

            try
            {
                await _accountService.CreateAccountAsync(accountDTO);
                return Ok(new { Message = "Account created successfully. Please check your email to verify." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AccountLoginDTO loginDto)
        {
            try
            {
                var account = await _accountService.LoginAsync(loginDto);
                var token = _jwtTokenService.GenerateToken(account.Id, account.Email);
                return Ok(new { Token = token, Account = account });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }


        [Authorize]
        [HttpPost("OpenTreasure/{treasureId}")]
        public async Task<IActionResult> OpenTreasure(string treasureId)
        {
            try
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var treasureResult = await _accountService.OpenTreasureAsync(userId, treasureId);
                return Ok(treasureResult);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("BuyMatchBox")]
        public async Task<IActionResult> BuyMatchBox()
        {
            try
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var result = await _accountService.BuyMatchBoxAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("AddMatches/{matches}")] 
        public async Task<IActionResult> AddMatches(int matches)
        {
            try
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var result = await _accountService.AddMatchesAsync(userId, matches);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
