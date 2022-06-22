using Mask.Service;
using Microsoft.AspNetCore.Mvc;

namespace Mask.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MaskController : Controller
    {
        public readonly MaskService _maskservice;
        public MaskController(MaskService maskservice)
        {
            _maskservice = maskservice;
        }
        public async Task<IActionResult> Get()
        {
            try
            {
                var maskcount = await _maskservice.GetMaskInfo();
                return Ok(maskcount);
            }
            catch (HttpRequestException ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
