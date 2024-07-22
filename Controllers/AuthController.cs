using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyShopApi.Repositories;

namespace MyShopApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthRepo _repo;
        public AuthController(IAuthRepo repository)
        {
            _repo = repository;
        }

        [HttpPost("register")]
        public ActionResult Register([FromForm]string username, [FromForm] string password)
        {
            var res = _repo.RegisterNewUser(username, password);
            if (res.Key is false)
                return BadRequest(res.Value);
            return Ok(res.Value);
        }

        [HttpPost("login")]
        public ActionResult Login([FromForm] string username, [FromForm] string password)
        {
            var res = _repo.LoginUser(username, password);
            if (res.Key is false)
                return BadRequest(res.Value);
            return Ok(res.Value);
        }
    }
}
