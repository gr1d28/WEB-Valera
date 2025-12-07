using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using Valera.Services;

namespace Valera.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ValeraController : ControllerBase
    {
        private readonly IValeraService _valeraService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValeraController(IValeraService valeraService, IHttpContextAccessor httpContextAccessor)
        {
            _valeraService = valeraService;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
        }

        private string GetCurrentUserRole()
        {
            var roleClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role);
            return roleClaim?.Value ?? "User";
        }

        private bool IsAdmin()
        {
            return GetCurrentUserRole() == "Admin";
        }

        [HttpGet("role")]
        public async Task<ActionResult<string>> GetUserInfo()
        {
            return GetCurrentUserRole();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Valera.Models.Valera>>> GetAllValeras()
        {
            try
            {
                var valeras = await _valeraService.GetAllValerasAsync();
                return Ok(valeras);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ошибка при получении списка Валер", Error = ex.Message });
            }
        }

        [HttpGet("my")]
        public async Task<ActionResult<List<Valera.Models.Valera>>> GetMyValeras()
        {
            try
            {
                var userId = GetCurrentUserId();
                var valeras = await _valeraService.GetUserValerasAsync(userId);
                return Ok(valeras);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ошибка при получении ваших Валер", Error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Valera.Models.Valera>> GetValeraById(int id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var valera = await _valeraService.GetValeraByIdAsync(id, userId, IsAdmin());

                if (valera == null)
                    return NotFound(new { Message = "Валера не найден" });

                return Ok(valera);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid("Нет доступа к этому Валере");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ошибка при получении Валеры", Error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Valera.Models.Valera>> CreateValera(Valera.Models.Valera valera)
        {
            try
            {
                var userId = GetCurrentUserId();
                valera.UserId = userId; // Привязываем к текущему пользователю

                var createdValera = await _valeraService.CreateValeraAsync(valera);
                return CreatedAtAction(nameof(GetValeraById), new { id = createdValera.Id }, createdValera);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ошибка при создании Валеры", Error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Valera.Models.Valera>> UpdateValera(int id, Valera.Models.Valera valera)
        {
            try
            {
                var userId = GetCurrentUserId();
                var updatedValera = await _valeraService.UpdateValeraAsync(id, valera, userId, IsAdmin());

                if (updatedValera == null)
                    return NotFound(new { Message = "Валера не найден" });

                return Ok(updatedValera);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid("Нет прав для редактирования этого Валеры");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ошибка при обновлении Валеры", Error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteValera(int id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var result = await _valeraService.DeleteValeraAsync(id, userId, IsAdmin());

                if (!result)
                    return NotFound(new { Message = "Валера не найден" });

                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid("Нет прав для удаления этого Валеры");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ошибка при удалении Валеры", Error = ex.Message });
            }
        }

        [HttpPost("{id}/work")]
        public async Task<ActionResult<Valera.Models.Valera>> Work(int id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var valera = await _valeraService.WorkAsync(id, userId, IsAdmin());

                if (valera == null)
                    return NotFound(new { Message = "Валера не найден" });

                return Ok(valera);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid("Нет доступа к этому Валере");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ошибка при выполнении действия", Error = ex.Message });
            }
        }

        [HttpPost("{id}/contemplate-nature")]
        public async Task<ActionResult<Valera.Models.Valera>> ContemplateNature(int id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var valera = await _valeraService.ContemplateNatureAsync(id, userId, IsAdmin());

                if (valera == null)
                    return NotFound(new { Message = "Валера не найден" });

                return Ok(valera);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid("Нет доступа к этому Валере");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ошибка при выполнении действия", Error = ex.Message });
            }
        }

        [HttpPost("{id}/drink-wine-watch-series")]
        public async Task<ActionResult<Valera.Models.Valera>> DrinkWineWatchSeries(int id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var valera = await _valeraService.DrinkWineWatchSeriesAsync(id, userId, IsAdmin());

                if (valera == null)
                    return NotFound(new { Message = "Валера не найден" });

                return Ok(valera);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid("Нет доступа к этому Валере");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ошибка при выполнении действия", Error = ex.Message });
            }
        }

        [HttpPost("{id}/go-to-bar")]
        public async Task<ActionResult<Valera.Models.Valera>> GoToBar(int id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var valera = await _valeraService.GoToBarAsync(id, userId, IsAdmin());

                if (valera == null)
                    return NotFound(new { Message = "Валера не найден" });

                return Ok(valera);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid("Нет доступа к этому Валере");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ошибка при выполнении действия", Error = ex.Message });
            }
        }

        [HttpPost("{id}/drink-with-friends")]
        public async Task<ActionResult<Valera.Models.Valera>> DrinkWithFriends(int id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var valera = await _valeraService.DrinkWithFriendsAsync(id, userId, IsAdmin());

                if (valera == null)
                    return NotFound(new { Message = "Валера не найден" });

                return Ok(valera);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid("Нет доступа к этому Валере");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ошибка при выполнении действия", Error = ex.Message });
            }
        }

        [HttpPost("{id}/sing-in-subway")]
        public async Task<ActionResult<Valera.Models.Valera>> SingInSubway(int id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var valera = await _valeraService.SingInSubwayAsync(id, userId, IsAdmin());

                if (valera == null)
                    return NotFound(new { Message = "Валера не найден" });

                return Ok(valera);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid("Нет доступа к этому Валере");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ошибка при выполнении действия", Error = ex.Message });
            }
        }

        [HttpPost("{id}/sleep")]
        public async Task<ActionResult<Valera.Models.Valera>> Sleep(int id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var valera = await _valeraService.SleepAsync(id, userId, IsAdmin());

                if (valera == null)
                    return NotFound(new { Message = "Валера не найден" });

                return Ok(valera);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid("Нет доступа к этому Валере");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ошибка при выполнении действия", Error = ex.Message });
            }
        }
    }
}
