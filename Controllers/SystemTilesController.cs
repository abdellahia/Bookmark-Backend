using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using launchpad_backend.Models;
using launchpad_backend.Helper;
using Microsoft.AspNetCore.Authorization;

namespace launchpad_backend.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class SystemTilesController : ControllerBase
    {
        private readonly Models.DbContext _context;

        public SystemTilesController(Models.DbContext context)
        {
            _context = context;
        }

        // GET: api/SystemTiles
        /// <summary>
        /// Get all system tiles
        /// </summary>
        [ProducesResponseType(typeof(IEnumerable<SystemTiles>), 200)]
        [ProducesResponseType(500)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SystemTiles>>> GetSystemTiles()
        {
            var claims = User.Claims;
            var allowedApps = KeyCloak.GetApps(claims);
            return await _context.SystemTiles.Where(o => allowedApps.Contains(o.KeycloakClient)).ToListAsync();
        }

        // GET: api/SystemTiles/5
        /// <summary>
        /// Get the system tile for the given id
        /// </summary>
        /// <param name="id">GUID</param>            
        [ProducesResponseType(typeof(SystemTiles), 200)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        [HttpGet("{id}")]
        public async Task<ActionResult<SystemTiles>> GetSystemTiles(Guid id)
        {
            var claims = User.Claims;
            var allowedApps = KeyCloak.GetApps(claims);            
            var systemTile = await _context.SystemTiles.FindAsync(id);           
            
            if (systemTile == null)
            {
                return NotFound();
            }
            if (!allowedApps.Contains(systemTile.KeycloakClient))
            {
                return Forbid();
            }
            return systemTile;
        }        
    }
}
