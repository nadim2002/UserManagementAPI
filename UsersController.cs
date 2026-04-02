using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static List<User> users = new List<User>();

        // GET: api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(users);
        }

        // GET: api/users/1
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            try
            {
                var user = users.FirstOrDefault(u => u.Id == id);

                if (user == null)
                    return NotFound("User not found");

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        // POST: api/users
        [HttpPost]
        public ActionResult<User> CreateUser(User newUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            newUser.Id = users.Any() ? users.Max(u => u.Id) + 1 : 1;

            users.Add(newUser);

            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }

        // PUT: api/users/1
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User updatedUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = users.FirstOrDefault(u => u.Id == id);

            if (user == null)
                return NotFound("User not found");

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            user.Role = updatedUser.Role;

            return NoContent();
        }

        // DELETE: api/users/1
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);

            if (user == null)
                return NotFound("User not found");

            users.Remove(user);

            return NoContent();
        }
    }
}