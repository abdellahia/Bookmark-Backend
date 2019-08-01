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
    public class UserTilesController : ControllerBase
    {
        private readonly Models.DbContext _context;

        public UserTilesController(Models.DbContext context)
        {
            _context = context;
        }

        // GET: api/UserTiles
        /// <summary>
        /// Get all user specifiy tiles
        /// </summary>
        [ProducesResponseType(typeof(IEnumerable<UserTiles>), 200)]       
        [ProducesResponseType(500)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTiles>>> GetUserTiles()
        {
            var claims = User.Claims;
            var user = KeyCloak.GetProperty(claims, ClaimTypes.Username);
            return await _context.UserTiles.Where(o => o.Username == user).ToListAsync();
        }

        // GET: api/UserTiles/5
        /// <summary>
        /// Get the user tile for the given id
        /// </summary>
        /// <param name="id">GUID</param>            
        [ProducesResponseType(typeof(UserTiles), 200)]        
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTiles>> GetUserTiles(Guid id)
        {            
            var userTile = await _context.UserTiles.FindAsync(id);            
            if (userTile == null)
            {
                return NotFound();
            }
            if (!IsUserAllowed(userTile))
            {
                return Forbid();
            }

            return userTile;
        }

        // PUT: api/UserTiles/5
        /// <summary>
        /// Update the user tile for the given id
        /// </summary>
        /// <param name="id">GUID</param>        
        /// <param name="payload">Tile data</param>        
        [ProducesResponseType(200)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserTiles(Guid id, UserTiles payload)
        {
            if (id != payload.Guid)
            {
                return BadRequest();
            }
            
            var userTile = await _context.UserTiles.FindAsync(id);           

            if (userTile == null)
            {
                return NotFound();
            }
            if (!IsUserAllowed(userTile))
            {
                return Forbid();
            }

            //Reset the username so the username can't be edited            
            payload.Username = GetUsername();
            //Deatach
            _context.Entry(userTile).State = EntityState.Detached;
            //Modify
            _context.Entry(payload).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTilesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserTiles
        /// <summary>
        /// Create a user tile
        /// </summary>      
        /// <param name="payload">Tile data</param>        
        [ProducesResponseType(typeof(UserTiles), 200)]       
        [ProducesResponseType(500)]
        [HttpPost]
        public async Task<ActionResult<UserTiles>> PostUserTiles(UserTileHttp payload)
        {
            var tile = new UserTiles()
            {
                Titel = payload.Titel,
                Description = payload.Description,
                Link = payload.Link,
                Tags = payload.Tags,
                Guid = Guid.NewGuid(),
                Username = GetUsername()
            };
            _context.UserTiles.Add(tile);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserTilesExists(tile.Guid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(tile);
        }

        // DELETE: api/UserTiles/5
        /// <summary>
        /// Delete the user tile for the given id
        /// </summary>
        /// <param name="id">GUID</param>      
        [ProducesResponseType(typeof(UserTiles), 200)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserTiles>> DeleteUserTiles(Guid id)
        {
            var userTiles = await _context.UserTiles.FindAsync(id);
            if (userTiles == null)
            {
                return NotFound();
            }
            if (!IsUserAllowed(userTiles))
            {
                return Forbid();
            }

            _context.UserTiles.Remove(userTiles);
            await _context.SaveChangesAsync();

            return userTiles;
        }

        private bool UserTilesExists(Guid id)
        {
            return _context.UserTiles.Any(e => e.Guid == id);
        }

        private bool IsUserAllowed(UserTiles userTile)
        {            
            if (GetUsername() == userTile?.Username)
            {
                return true;
            }
            return false;
        }

        private string GetUsername()
        {
            var claims = User.Claims;
            var user = KeyCloak.GetProperty(claims, ClaimTypes.Username);
            return user;
        }
    }
}

