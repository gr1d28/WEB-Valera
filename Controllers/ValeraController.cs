using Microsoft.AspNetCore.Mvc;
using Valera.Services;

namespace Valera.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValeraController : ControllerBase
    {
        private readonly IValeraService _valeraService;

        public ValeraController(IValeraService valeraService)
        {
            _valeraService = valeraService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Valera.Models.Valera>>> GetAllValeras()
        {
            var valeras = await _valeraService.GetAllValerasAsync();
            return Ok(valeras);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Valera.Models.Valera>> GetValeraById(int id)
        {
            var valera = await _valeraService.GetValeraByIdAsync(id);
            if (valera == null)
                return NotFound();
            return Ok(valera);
        }

        [HttpPost]
        public async Task<ActionResult<Valera.Models.Valera>> CreateValera(Valera.Models.Valera valera)
        {
            var createdValera = await _valeraService.CreateValeraAsync(valera);
            return CreatedAtAction(nameof(GetValeraById), new { id = createdValera.Id }, createdValera);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Valera.Models.Valera>> UpdateValera(int id, Valera.Models.Valera valera)
        {
            var updatedValera = await _valeraService.UpdateValeraAsync(id, valera);
            if (updatedValera == null)
                return NotFound();
            return Ok(updatedValera);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteValera(int id)
        {
            var result = await _valeraService.DeleteValeraAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpPost("{id}/work")]
        public async Task<ActionResult<Valera.Models.Valera>> Work(int id)
        {
            var valera = await _valeraService.WorkAsync(id);
            if (valera == null) return NotFound();
            return Ok(valera);
        }

        [HttpPost("{id}/contemplate-nature")]
        public async Task<ActionResult<Valera.Models.Valera>> ContemplateNature(int id)
        {
            var valera = await _valeraService.ContemplateNatureAsync(id);
            if (valera == null) return NotFound();
            return Ok(valera);
        }

        [HttpPost("{id}/drink-wine-watch-series")]
        public async Task<ActionResult<Valera.Models.Valera>> DrinkWineWatchSeries(int id)
        {
            var valera = await _valeraService.DrinkWineWatchSeriesAsync(id);
            if (valera == null) return NotFound();
            return Ok(valera);
        }

        [HttpPost("{id}/go-to-bar")]
        public async Task<ActionResult<Valera.Models.Valera>> GoToBar(int id)
        {
            var valera = await _valeraService.GoToBarAsync(id);
            if (valera == null) return NotFound();
            return Ok(valera);
        }

        [HttpPost("{id}/drink-with-friends")]
        public async Task<ActionResult<Valera.Models.Valera>> DrinkWithFriends(int id)
        {
            var valera = await _valeraService.DrinkWithFriendsAsync(id);
            if (valera == null) return NotFound();
            return Ok(valera);
        }

        [HttpPost("{id}/sing-in-subway")]
        public async Task<ActionResult<Valera.Models.Valera>> SingInSubway(int id)
        {
            var valera = await _valeraService.SingInSubwayAsync(id);
            if (valera == null) return NotFound();
            return Ok(valera);
        }

        [HttpPost("{id}/sleep")]
        public async Task<ActionResult<Valera.Models.Valera>> Sleep(int id)
        {
            var valera = await _valeraService.SleepAsync(id);
            if (valera == null) return NotFound();
            return Ok(valera);
        }
    }
}
