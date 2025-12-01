using FluentAssertions;
using InnoShop.ProductManagment.Domain.Exceptions;
using InnoShop.ProductManagment.Domain.Models;

namespace ProductManagment.UnitTests.Domain
{
    public class ProductTests
    {
        [Fact]
        public void Create_Product_ShouldInitializeWithCorrectValues()
        {
            var name = "Test Product";
            var description = "Test Description";
            var userId = Guid.NewGuid();
            var price = 99.99;

            // Act
            var product = new Product(name, description, userId, price);

            // Assert
            product.Name.Should().Be(name);
            product.Description.Should().Be(description);
            product.UserId.Should().Be(userId);
            product.Price.Should().Be(price);
            product.IsAvailable.Should().BeTrue();
            product.IsDeleted.Should().BeFalse();
            product.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void ChangeName_ShouldUpdateProductName()
        {
            // Arrange
            var product = new Product("Original", "Desc", Guid.NewGuid(), 10.0);
            var newName = "Updated Name";

            // Act
            product.ChangeName(newName);

            // Assert
            product.Name.Should().Be(newName);
        }

        [Fact]
        public void ChangeDescription_ShouldUpdateDescription()
        {
            // Arrange
            var product = new Product("Name", "Original", Guid.NewGuid(), 10.0);
            var newDesc = "Updated Description";

            // Act
            product.ChangeDescription(newDesc);

            // Assert
            product.Description.Should().Be(newDesc);
        }

        [Fact]
        public void ChangePrice_ShouldUpdatePrice()
        {
            // Arrange
            var product = new Product("Name", "Desc", Guid.NewGuid(), 10.0);
            var newPrice = 99.99;

            // Act
            product.ChangePrice(newPrice);

            // Assert
            product.Price.Should().Be(newPrice);
        }

        [Fact]
        public void SetAvailable_WhenUnavailable_ShouldSetToAvailable()
        {
            // Arrange
            var product = new Product("Name", "Desc", Guid.NewGuid(), 10.0);
            product.SetUnavailable();

            // Act
            product.SetAvailable();

            // Assert
            product.IsAvailable.Should().BeTrue();
        }

        [Fact]
        public void SetAvailable_WhenAlreadyAvailable_ShouldThrowException()
        {
            // Arrange
            var product = new Product("Name", "Desc", Guid.NewGuid(), 10.0);

            // Act & Assert
            Assert.Throws<AlreadyDoneException>(() => product.SetAvailable());
        }

        [Fact]
        public void SetUnavailable_WhenAvailable_ShouldSetToUnavailable()
        {
            // Arrange
            var product = new Product("Name", "Desc", Guid.NewGuid(), 10.0);

            // Act
            product.SetUnavailable();

            // Assert
            product.IsAvailable.Should().BeFalse();
        }

        [Fact]
        public void SetUnavailable_WhenAlreadyUnavailable_ShouldThrowException()
        {
            // Arrange
            var product = new Product("Name", "Desc", Guid.NewGuid(), 10.0);
            product.SetUnavailable();

            // Act & Assert
            Assert.Throws<AlreadyDoneException>(() => product.SetUnavailable());
        }

        [Fact]
        public void Delete_ShouldSetIsDeletedToTrue()
        {
            // Arrange
            var product = new Product("Name", "Desc", Guid.NewGuid(), 10.0);

            // Act
            product.Delete();

            // Assert
            product.IsDeleted.Should().BeTrue();
        }

        [Fact]
        public void Delete_WhenAlreadyDeleted_ShouldThrowException()
        {
            // Arrange
            var product = new Product("Name", "Desc", Guid.NewGuid(), 10.0);
            product.Delete();

            // Act & Assert
            Assert.Throws<AlreadyDoneException>(() => product.Delete());
        }

        [Fact]
        public void Recover_ShouldSetIsDeletedToFalse()
        {
            // Arrange
            var product = new Product("Name", "Desc", Guid.NewGuid(), 10.0);
            product.Delete();

            // Act
            product.Recover();

            // Assert
            product.IsDeleted.Should().BeFalse();
        }

        [Fact]
        public void Recover_WhenNotDeleted_ShouldThrowException()
        {
            // Arrange
            var product = new Product("Name", "Desc", Guid.NewGuid(), 10.0);

            // Act & Assert
            Assert.Throws<AlreadyDoneException>(() => product.Recover());
        }
    }
}