using System.ComponentModel.DataAnnotations;
using Showcase_Contactpagina.Models;

namespace Showcase_Contactpagina.Tests;

[TestFixture]
public class ContactFormTests {
    [Test]
    public void FirstName_RequiredAttribute_Validation() {
        var form = new Contactform
            { FirstName = "", LastName = "Doe", Email = "john.doe@example.com", Message = "Hello", Subject = "Test" };
        var context = new ValidationContext(form, null, null);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(form, context, results, true);

        Assert.IsFalse(isValid);
        Assert.That(results[0].ErrorMessage, Is.EqualTo("The FirstName field is required."));
    }

    [Test]
    public void Email_InvalidFormat_Validation() {
        var form = new Contactform
            { FirstName = "John", LastName = "Doe", Email = "invalid-email", Message = "Hello", Subject = "Test" };
        var context = new ValidationContext(form, null, null);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(form, context, results, true);

        Assert.IsFalse(isValid);
        Assert.That(results[0].ErrorMessage, Is.EqualTo("The Email field is not a valid e-mail address."));
    }

    [Test]
    public void Message_MaxLength_Validation() {
        var longMessage = new string('a', 601);
        var form = new Contactform {
            FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Message = longMessage,
            Subject = "Test"
        };
        var context = new ValidationContext(form, null, null);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(form, context, results, true);

        Assert.IsFalse(isValid);
        Assert.That(results[0].ErrorMessage,
            Is.EqualTo("The field Message must be a string with a maximum length of 600."));
    }
}