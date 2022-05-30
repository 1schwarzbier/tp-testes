using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using RealEstate.Services.PropertyService;
using RealEstate.UnitTests.Mocks;
using RealEstate.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.UnitTests.Controllers;

public class GetPropertyTests
{
    private readonly PropertyController _propertyController;
    private readonly Mock<IPropertyService> _propertiesServiceAsyncMock;

    public GetPropertyTests()
    {
        _propertiesServiceAsyncMock = new Mock<IPropertyService>();

        _propertyController = new PropertyController(
            propertiesService: _propertiesServiceAsyncMock.Object);

        Setups();
    }

    private void Setups()
    {
        _propertiesServiceAsyncMock.Setup(m => m.Get(PropertyMock.GUID_1))
            .ReturnsAsync(PropertyMock.SERVICE_RESPONSE_GET);

        _propertiesServiceAsyncMock.Setup(m => m.Get(PropertyMock.GUID_2))
            .ReturnsAsync(PropertyMock.SERVICE_RESPONSE_GET_NULL);
    }

    [Test]
    public async Task GetProperty_Controller_Sucess()
    {
        var resultProperty = await _propertyController.GetProperty(PropertyMock.GUID_1);

        _propertiesServiceAsyncMock.Verify(v => v.Get(PropertyMock.GUID_1), Times.Once);

        _propertiesServiceAsyncMock.VerifyNoOtherCalls();

        Assert.IsInstanceOf<OkObjectResult>(resultProperty);
    }

    [Test]
    public async Task GetProperty_Controller_Failure()
    {
        var resultProperty = await _propertyController.GetProperty(PropertyMock.GUID_2);

        _propertiesServiceAsyncMock.Verify(v => v.Get(PropertyMock.GUID_2), Times.Once);

        _propertiesServiceAsyncMock.VerifyNoOtherCalls();

        Assert.IsInstanceOf<ObjectResult>(resultProperty);
    }
}

