using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using RealEstate.Models;
using RealEstate.Services;
using RealEstate.Services.PropertyService;
using RealEstate.UnitTests.Mocks;
using RealEstate.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.UnitTests.Controllers;

public class GetAllPropertiesTests
{
    private readonly PropertyController _propertyController;
    private readonly Mock<IPropertyService> _propertiesServiceAsyncMock;

    public GetAllPropertiesTests()
    {
        _propertiesServiceAsyncMock = new Mock<IPropertyService>();

        _propertyController = new PropertyController(
            propertiesService: _propertiesServiceAsyncMock.Object);

        Setups();
    }

    private void Setups()
    {
        _propertiesServiceAsyncMock.Setup(m => m.GetAll(PropertyMock.PAGINATED_REQUEST_MODEL_1))
            .ReturnsAsync(PropertyMock.SERVICE_RESPONSE_GET_ALL);

        _propertiesServiceAsyncMock.Setup(m => m.GetAll(PropertyMock.PAGINATED_REQUEST_MODEL_2))
            .ReturnsAsync(PropertyMock.SERVICE_RESPONSE_GET_ALL_EMPTY);
    }

    [Test]
    public async Task GetAllProperties_Controller_Sucess()
    {
        var resultAllProperties = await _propertyController.GetAllProperties(PropertyMock.PAGINATED_REQUEST_MODEL_1);

        _propertiesServiceAsyncMock.Verify(v => v.GetAll(PropertyMock.PAGINATED_REQUEST_MODEL_1), Times.Once);

        _propertiesServiceAsyncMock.VerifyNoOtherCalls();

        Assert.IsInstanceOf<OkObjectResult>(resultAllProperties);
    }

    [Test]
    public async Task GetAllPropoerties_Controller_Failure()
    {
        var resultAllProperties = await _propertyController.GetAllProperties(PropertyMock.PAGINATED_REQUEST_MODEL_2);

        _propertiesServiceAsyncMock.Verify(v => v.GetAll(PropertyMock.PAGINATED_REQUEST_MODEL_2), Times.Once);

        _propertiesServiceAsyncMock.VerifyNoOtherCalls();

        Assert.IsInstanceOf<ObjectResult>(resultAllProperties);
    }
}