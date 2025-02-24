using System.ComponentModel.DataAnnotations;
using NUnit.Framework;
using Showcase_Contactpagina.Models;

namespace Showcase_Contactpagina.Tests
{
    [TestFixture]
    public class ContactFormTests
    {
        [Test]
        public void FirstName_RequiredAttribute_Validation()
        {
            // Arrange
            var form = new Contactform { FirstName = "", LastName = "Doe", Email = "john.doe@example.com", Message = "Hello", Subject = "Test" };
            var context = new ValidationContext(form, null, null);
            var results = new System.Collections.Generic.List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(form, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual("The FirstName field is required.", results[0].ErrorMessage);
        }

        [Test]
        public void Email_InvalidFormat_Validation()
        {
            // Arrange
            var form = new Contactform { FirstName = "John", LastName = "Doe", Email = "invalid-email", Message = "Hello", Subject = "Test" };
            var context = new ValidationContext(form, null, null);
            var results = new System.Collections.Generic.List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(form, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual("The Email field is not a valid e-mail address.", results[0].ErrorMessage);
        }

        [Test]
        public void Message_MaxLength_Validation()
        {
            // Arrange
            var longMessage = new string('a', 601);
            var form = new Contactform { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Message = longMessage, Subject = "Test" };
            var context = new ValidationContext(form, null, null);
            var results = new System.Collections.Generic.List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(form, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual("The field Message must be a string with a maximum length of 600.", results[0].ErrorMessage);
        }
    }
}