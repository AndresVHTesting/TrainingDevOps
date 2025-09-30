using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingDevOps.Controllers;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;

namespace TrainingDevOps.Tests {
    [TestClass]
    public class PlayerControllerTests {
        private PlayerController? _controller;

        [TestInitialize]
        public void Setup() {
            // Mock ILogger<PlayerController>
            var loggerMock = new Mock<ILogger<PlayerController>>();
            _controller = new PlayerController(loggerMock.Object);
        }

        [TestMethod]
        public void GetPlayers_ShouldReturnInitialSeededPlayers() {
            // Act
            IActionResult result = _controller.GetPlayers();

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();

            var players = okResult.Value as List<Player>;
            players.Should().NotBeNull();
            players.Count().Should().BeGreaterThanOrEqualTo(5);
            //players.Count.Should().BeGreaterOrEqualTo(5); // You seeded 5 players

            // Optional: Check first player's properties
            players[0].Name.Should().StartWith("Name:");
        }

        [TestMethod]
        public void GetPlayer_WithValidId_ShouldReturnPlayer() {
            // Act
            IActionResult result = _controller.GetPlayer(1);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();

            var player = okResult.Value as Player;
            player.Should().NotBeNull();
            player.Id.Should().Be(1);
        }

        [TestMethod]
        public void GetPlayer_WithInvalidId_ShouldReturnNotFound() {
            // Act
            IActionResult result = _controller.GetPlayer(999);

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.Value.Should().Be("Player with ID 999 not found.");
        }

        // Add more tests for POST, PUT, DELETE similarly...
    }
}


