using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.AutoGen;
using API.Controllers;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace API.Tests.Controllers
{
    public class PokemonControllerTests
    {
        [Fact]
        public async Task Get_ReturnsAllPokemon_WhenSpeciesIsNullOrEmpty()
        {
            // Arrange
            var mockRepo = new Mock<IPokemonRepository>();
            var samplePokemonList = GetSamplePokemonList();
            mockRepo.Setup(repo => repo.RetrieveAllAsync()).ReturnsAsync(samplePokemonList);

            var controller = new PokemonController(mockRepo.Object);

            // Act
            var result = await controller.Get(null);

            // Assert
            Assert.Equal(samplePokemonList.Count(), result.Count());
            Assert.Equal(samplePokemonList, result);
        }

        [Fact]
        public async Task Get_ReturnsFilteredPokemon_WhenSpeciesIsProvided()
        {
            // Arrange
            var mockRepo = new Mock<IPokemonRepository>();
            var samplePokemonList = GetSamplePokemonList();
            mockRepo.Setup(repo => repo.RetrieveAllAsync()).ReturnsAsync(samplePokemonList);

            var controller = new PokemonController(mockRepo.Object);
            var speciesToFilter = "Pikachu";

            // Act
            var result = await controller.Get(speciesToFilter);

            // Assert
            var filteredPokemon = samplePokemonList.Where(p => p.Species == speciesToFilter);
            Assert.Equal(filteredPokemon.Count(), result.Count());
            Assert.Equal(filteredPokemon, result);
        }

        private IEnumerable<Pokemon> GetSamplePokemonList()
        {
            return new List<Pokemon>
            {
                new Pokemon { Id = 1, Name = "Pikachu", Species = "Pikachu" },
                new Pokemon { Id = 2, Name = "Bulbasaur", Species = "Bulbasaur" },
                new Pokemon { Id = 3, Name = "Charmander", Species = "Charmander" }
            };
        }
    }
}


