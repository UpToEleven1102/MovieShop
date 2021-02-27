using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.ServiceInterface;

namespace MovieShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> GetPurchases(int pageNumber, int pageSize)
        {
            pageNumber -= 1;
            if (pageSize <= 0 || pageSize > 30) pageSize = 30;
            if (pageNumber < 0) pageNumber = 0;
            
            try
            {
                var response = await _adminService.GetAllPurchases(pageNumber, pageSize);
                return Ok(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}