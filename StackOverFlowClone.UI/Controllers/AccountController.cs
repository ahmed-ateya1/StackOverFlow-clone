using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackOverFlowClone.Core.Domain.IdentityEntites;
using StackOverFlowClone.Core.DTO;
using StackOverFlowClone.Core.Enumeration;

namespace StackOverFlowClone.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if(!ModelState.IsValid)
            {
                return View(registerDTO);
            }
            
            var user = new ApplicationUser() 
            { 
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user,isPersistent: false);

                var roleType = registerDTO.roleOption == RoleOptions.Admin ?
                    nameof(RoleOptions.Admin) : nameof(RoleOptions.User);
                if(await _roleManager.FindByNameAsync(roleType)==null)
                {
                    var role = new ApplicationRole() { Name = roleType };
                    var roleResult = await _roleManager.CreateAsync(role);
                    if (!roleResult.Succeeded)
                    {
                        ModelState.AddModelError(string.Empty, "Failed Add Error");
                    }
                }
                var resultAddRole = await _userManager.AddToRoleAsync(user, roleType);
                if (!resultAddRole.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Failed to Add Role");
                    return View(registerDTO);
                }
                return RedirectToAction("Login");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(registerDTO);
        }

        [HttpGet]
        public IActionResult Login(string returnURL = null)
        {
            ViewBag.ReturnURL = returnURL;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO loginDTO , string returnURL = null)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ReturnURL = returnURL;
                return View(loginDTO);
            }

            var result = await _signInManager.PasswordSignInAsync(loginDTO.UserName, loginDTO.Password, loginDTO.RememberMe , false);

            if (result.Succeeded)
            {
                if (!String.IsNullOrEmpty(returnURL) && Url.IsLocalUrl(returnURL))
                {
                    return LocalRedirect(returnURL);
                }
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Invaild username or password");
            ViewBag.ReturnURL = returnURL;
            return View(loginDTO);
        }
        
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
