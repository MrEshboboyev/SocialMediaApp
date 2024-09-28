using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SocialMediaApp.Application.Common.Models;
using SocialMediaApp.Application.Services.Interfaces;
using SocialMediaApp.UI.Services.IServices;
using Microsoft.AspNetCore.Identity;
using SocialMediaApp.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace SocialMediaApp.UI.Controllers
{
    public class AccountController(IAuthService authService,
        ITokenProvider tokenProvider,
        SignInManager<AppUser> signInManager) : Controller
    {
        private readonly IAuthService _authService = authService;
        private readonly ITokenProvider _tokenProvider = tokenProvider;
        private readonly SignInManager<AppUser> _signInManager = signInManager;

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Error with entered values");
                return View(model);
            }

            RegisterModel registerModel = new()
            {
                Email = model.Email,
                Password = model.Password,
                DateOfBirth = model.DateOfBirth,
                ProfilePictureUrl = model.ProfilePictureUrl,
                Website = model.Website
            };

            var result = await _authService.RegisterAsync(registerModel);

            if (result.Success)
            {
                TempData["success"] = result.Message;
                return RedirectToAction(nameof(Login));
            }

            TempData["error"] = result.Message;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var result = await _authService.LoginAsync(loginModel);

            if (result.Success)
            {
                // sign in user applied
                await SignInUser(result.Data);

                // set token for user
                _tokenProvider.SetToken(result.Data);

                TempData["success"] = "Login successfully!";
                return RedirectToAction("Index", "Home");
            }

            TempData["error"] = result.Message;

            return View(loginModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToAction("Index", "Home");
        }

        #region Private Methods
        // Sign In User
        private async Task SignInUser(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            // adding claims
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));

            identity.AddClaim(new Claim(ClaimTypes.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(ClaimTypes.Role,
                jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));


            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
        #endregion
    }
}
