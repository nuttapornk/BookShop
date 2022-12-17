using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookShop.WebUi.Controllers
{
    public class ValuesController : Controller
    {
        private readonly ILogger _logger;
        public ValuesController(ILogger logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = "admins")]
        public IActionResult Admin()
        {
            return View();
        }

        [Authorize(Policy = "noaccess")]
        public IActionResult Noaccess()
        {
            return View();
        }

        [Authorize(Policy = "users")]
        public async Task<IActionResult> Authentication()
        {
            ClaimsPrincipal currentUser = this.User;

            var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            _logger.LogError(currentUserName);

            string accessToken = await HttpContext.GetTokenAsync("access_token");
            string idToken = await HttpContext.GetTokenAsync("id_token");
            string refreshToken = await HttpContext.GetTokenAsync("refresh_token");

            IEnumerable<Claim> roleClaims = User.FindAll(ClaimTypes.Role);
            IEnumerable<string> roles = roleClaims.Select(r => r.Value);
            foreach (var role in roles)
            {
                _logger.LogError(role);
            }

            //Another way to display all role claims
            var currentClaims = currentUser.FindAll(ClaimTypes.Role).ToList();
            foreach (var claim in currentClaims)
            {
                _logger.LogError(claim.ToString());
            }

            return View();

        }
    }
}
