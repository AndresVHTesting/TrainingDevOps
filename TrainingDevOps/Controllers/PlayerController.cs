using Microsoft.AspNetCore.Mvc;

namespace TrainingDevOps.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase {
        
        private readonly ILogger<PlayerController> _logger;
        
        private static List<Player> _players = new(); // In-memory storage (resets on app restart)

        public PlayerController(ILogger<PlayerController> logger) {
            _logger = logger;

            // Seed initial data if empty (runs once on first request)
            if (!_players.Any()) {
                _players.AddRange(Enumerable.Range(1, 5).Select(index => new Player {
                    Id = index,
                    Name = $"Name: {index}",
                    Description = Random.Shared.Next(1, 5).ToString(),
                }));
                _logger.LogInformation("Seeded initial players in memory.");
            }
        }

        // GET: /player (Get All)
        //[HttpGet("/GetAllPlayers", Name = "GetAllPlayers")]
        [HttpGet(Name = "GetAllPlayers")]
        public IActionResult GetPlayers() {
            _logger.LogInformation("Retrieving all players.");
            return Ok(_players);
        }

        // GET: /player/{id} (Get Single)
        [HttpGet("{id}", Name = "GetPlayerById")]
        public IActionResult GetPlayer(int id) {
            _logger.LogInformation("Retrieving player with ID: {Id}", id);
            var player = _players.FirstOrDefault(p => p.Id == id);
            if (player == null) {
                _logger.LogWarning("Player with ID {Id} not found.", id);
                return NotFound($"Player with ID {id} not found.");
            }
            return Ok(player);
        }

        // POST: /player (Create)
        [HttpPost]
        public IActionResult CreatePlayer([FromBody] Player newPlayer) {
            if (newPlayer == null || string.IsNullOrWhiteSpace(newPlayer.Name)) {
                _logger.LogWarning("Invalid player data provided for creation.");
                return BadRequest("Name is required.");
            }

            _logger.LogInformation("Creating new player: {Name}", newPlayer.Name);
            newPlayer.Id = _players.Any() ? _players.Max(p => p.Id) + 1 : 1; // Auto-assign ID
            _players.Add(newPlayer);
            return CreatedAtAction(nameof(GetPlayer), new { id = newPlayer.Id }, newPlayer);
        }

        // PUT: /player/{id} (Update)
        [HttpPut("{id}")]
        public IActionResult UpdatePlayer(int id, [FromBody] Player updatedPlayer) {
            if (updatedPlayer == null || string.IsNullOrWhiteSpace(updatedPlayer.Name)) {
                _logger.LogWarning("Invalid player data provided for update.");
                return BadRequest("Name is required.");
            }

            var existingPlayer = _players.FirstOrDefault(p => p.Id == id);
            if (existingPlayer == null) {
                _logger.LogWarning("Player with ID {Id} not found for update.", id);
                return NotFound($"Player with ID {id} not found.");
            }

            _logger.LogInformation("Updating player with ID: {Id}", id);
            existingPlayer.Name = updatedPlayer.Name;
            existingPlayer.Description = updatedPlayer.Description;
            return Ok(existingPlayer);
        }

        // DELETE: /player/{id}
        [HttpDelete("{id}")]
        public IActionResult DeletePlayer(int id) {
            var player = _players.FirstOrDefault(p => p.Id == id);
            if (player == null) {
                _logger.LogWarning("Player with ID {Id} not found for deletion.", id);
                return NotFound($"Player with ID {id} not found.");
            }

            _logger.LogInformation("Deleting player with ID: {Id}", id);
            _players.Remove(player);
            return NoContent(); // 204 No Content for successful DELETE
        }
    }
}
