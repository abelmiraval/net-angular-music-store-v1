using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using MusicStore.DataAccess.Repositories;
using MusicStore.Services;
using MusicStore.Services.Implementations;
using MusicStore.Services.Interfaces;
using Xunit;

namespace MusicStore.UnitTest
{
    public class MusicStoreTest : DbContextUnitTest
    {
        [Fact]
        public void SumaTest()
        {
            // Arrange
            int a = 6;
            int b = 7;

            // Act
            var suma = a + b;
            var expected = 13;

            // Assert
            Assert.Equal(expected, suma);
        }

        [Fact]
        public void PaginationTest()
        {
            // Arrange
            var total = 29;
            var rows = 10;

            // Act
            var resultado = Utils.GetTotalPages(total, rows);
            var expected = 3;

            // Assert
            Assert.Equal(expected, resultado);
        }

        [Theory]
        [InlineData(29, 10, 3)]
        [InlineData(110, 10, 11)]
        [InlineData(200, 5, 40)]
        public void PagationWithParametersTest(int total, int rows, int expected)
        {
            var resultado = Utils.GetTotalPages(total, rows);

            Assert.Equal(expected, resultado);
        }

        [Theory]
        [InlineData("", 10, 10)]
        [InlineData("Concert", 4, 0)]
        [InlineData("Concierto", 4, 25)]
        public async Task PaginationForConcertsTest(string filter, int rows, int expected)
        {
            // Arrange
            var mapper = new Mock<IMapper>();
            var repository = new ConcertRepository(Context, mapper.Object);
            var fileUploader = new Mock<IFileUploader>();
            var logger = new Mock<ILogger<ConcertService>>();

            var service = new ConcertService(repository, mapper.Object, fileUploader.Object, logger.Object);

            // Act
            var actual = await service.GetAsync(filter, 1, rows);

            // Assert

            Assert.Equal(expected, actual.TotalPages);
        }

    }
}