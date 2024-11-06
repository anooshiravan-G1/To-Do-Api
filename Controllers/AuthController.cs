using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using To_Do_Api.DTO;
using To_Do_Api.Models;
using To_Do_Api.Helpers;

namespace To_Do_Api.Controllers
{
    public class AuthController : ControllerBase
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly TokenGenerator _tokenGenerator;

        public AuthController(UserManager<IdentityUser> userManager, 
                              SignInManager<IdentityUser> signInManager,
                              IConfiguration configuration,
                              TokenGenerator jwtTokenHelper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = configuration;
            _tokenGenerator = jwtTokenHelper;
        }





        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterModelDTO model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return BadRequest(new
                {
                    Message = "Email already registered"
                });
            }
            if(model.Password == model.PasswordConfirm)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    var token = _tokenGenerator.GenerateJwtToken(user);

                    return Ok(new AuthResponseDTO
                    {
                        Token = token,
                        Email = user.Email,
                        ExpiresAt = DateTime.UtcNow.AddDays(7)
                    });
                }
                return BadRequest(new
                {
                    Message = "Registration failed",
                    Errors = result.Errors.Select(e => e.Description)
                });
            }
            return BadRequest(new
            {
                Message = "Your Passwords are not match together!!!"
            });
        }







        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDTO>> Login([FromBody] LoginModelDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid credentials" });
            }

            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                isPersistent: false,
                lockoutOnFailure: true 
            );

            if (result.Succeeded)
            {
                var token = _tokenGenerator.GenerateJwtToken(user);

                return Ok(new AuthResponseDTO
                {
                    Token = token,
                    Email = user.Email, 
                    ExpiresAt = DateTime.UtcNow.AddDays(7)
                });
            }

            if (result.IsLockedOut)
            {
                return BadRequest(new { Message = "Account locked. Try again later." });
            }

            return Unauthorized(new { Message = "Invalid credentials" });
        }





    }
}
