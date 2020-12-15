using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ValueLabtest.Controllers;
using ValueLabtest.Service;

namespace ValueLabtestTests
{
    class StockControllerTests
    {
        private Mock<IStockServices> stockservice;
        private StockController stockController;

        [SetUp]
        public void Setup()
        {
            stockservice = new Mock<IStockServices>();
            stockController = new StockController(stockservice.Object);
        }
        [Test]
        public void GivenNullstockservice_ThenThrowArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _ = new StockController(null));
        }

        [Test]
        public void GivenEmptyQuoteResult_ThenReturnNotFound()
        {
            stockservice.Setup(x => x.GetQuoteDetailAsync()).ReturnsAsync(new List<ValueLabtest.Model.StockModel>());
            var response = stockController.GetStockItemsAsync();

            var notFoundResult = response.Result as NotFoundResult;

            Assert.IsTrue(response.IsCompletedSuccessfully);
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);

        }
        [Test]
        public void GivenNotEmptyQuoteResult_ThenReturnNotFound()
        {
            stockservice.Setup(x => x.GetQuoteDetailAsync()).ReturnsAsync(new List<ValueLabtest.Model.StockModel>() { new ValueLabtest.Model.StockModel() { } });
            var response = stockController.GetStockItemsAsync();
            var okResult =  response.Result as OkObjectResult;

            Assert.IsTrue(response.IsCompletedSuccessfully);
                Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }
    }
}
