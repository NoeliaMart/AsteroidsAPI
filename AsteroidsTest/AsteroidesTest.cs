using NUnit.Framework;
using System.Net;

using AsteroidsAPI.Controllers;
using AsteroidsAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AsteroidsTest
{
    [TestFixture]
    public class AsteroidesTest
    {

        [Test]
        public void getDays7()
        {

           AsteroideController controller = new AsteroideController();
            AsteroideParameters parameters = new AsteroideParameters();
            parameters.days = 7;

            IActionResult result = controller.GetAsteroides(parameters);

            // Assert
            var okObjectResult = result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);

            string? value = okObjectResult.Value as string;
            Assert.IsNotNull(value);

            Assert.AreEqual(Regex.Matches(value, "Nombre").Count, 6);

        }

        [Test]
        public void getDays1()
        {

           AsteroideController controller = new AsteroideController();
            AsteroideParameters parameters = new AsteroideParameters();
            parameters.days = 1;

            IActionResult result = controller.GetAsteroides(parameters);

            // Assert
            var okObjectResult = result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);

            string? value = okObjectResult.Value as string;
            Assert.IsNotNull(value);

            Assert.AreEqual(Regex.Matches(value, "Nombre").Count, 0);

        }


    }
}
