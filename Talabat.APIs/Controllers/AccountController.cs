using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.Domain.Entities.Identity;
using Talabat.Domain.Services;

namespace Talabat.APIs.Controllers
{
    public class AccountController : BaseAPIController
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager
            ,ITokenService tokenService, IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
            this.mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if (EmailExists(model.Email).Result.Value)
            {
                return BadRequest(new ApiResponse(400));
            }
            var user = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email.Split('@')[0]
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));
            return Ok(new UserDto()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                Token = await tokenService.GetTokenAsync(user, userManager)
            });
        }



        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null) return Unauthorized(new ApiResponse(401));
            var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if(!result.Succeeded) return Unauthorized(new ApiResponse(401));
                return Ok(new UserDto()
                {
                    DisplayName = user.DisplayName, Email = user.Email,
                    Token = await tokenService.GetTokenAsync(user, userManager)
                });
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await userManager.FindByEmailAsync(email);

            return new UserDto()
            {
                Email = email,
                DisplayName = user.DisplayName,
                Token = await tokenService.GetTokenAsync(user, userManager)
            };
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<Address>> GetUserAddress()
        {
            var user = await userManager.GetUserAddressAsync(User);

            var MappedAddress = mapper.Map<Address, AddressDto>(user.address);
            return Ok(MappedAddress);
        }
        [Authorize]
        [HttpPut("UpdateAddress")]

        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto addressDTO)
        {
            //var email = User.FindFirstValue(ClaimTypes.Email);

            //var user = await userManager.Users.Include(a => a.address)
            //    .SingleOrDefaultAsync(c => c.Email == email);

            var user = await userManager.GetUserAddressAsync(User);

            var address = mapper.Map<AddressDto, Address>(addressDTO);
            
            address.Id = user.address.Id;

            user.address = address;

            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok(addressDTO);
            }

            return BadRequest(new ApiResponse(400));

        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> EmailExists(string Email)
        {
            return await userManager.FindByEmailAsync(Email) is not null;
        }
    }

    }
