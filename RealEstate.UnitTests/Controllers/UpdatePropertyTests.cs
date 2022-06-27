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

public class UpdatePropertyTests
{
    private readonly PropertyController _propertyController;
    private readonly Mock<IPropertyService> _propertiesServiceAsyncMock;

    public UpdatePropertyTests()
    {
        _propertiesServiceAsyncMock = new Mock<IPropertyService>();

        _propertyController = new PropertyController(
            propertiesService: _propertiesServiceAsyncMock.Object);

        Setups();
    }

    private void Setups()
    {
        _propertiesServiceAsyncMock.Setup(m => m.Update(PropertyMock.UPDATE_PROPERTY_REQUEST_1))
            .ReturnsAsync(PropertyMock.SERVICE_RESPONSE_SUCESS);

        _propertiesServiceAsyncMock.Setup(m => m.Update(PropertyMock.UPDATE_PROPERTY_REQUEST_2))
            .ReturnsAsync(PropertyMock.SERVICE_RESPONSE_FAILURE);
    }

    [Test]
    public async Task UpdateProperty_Controller_Sucess()
    {
        var resultUpdateProperty = await _propertyController.UpdateProperty(PropertyMock.UPDATE_PROPERTY_REQUEST_1);

        _propertiesServiceAsyncMock.Verify(v => v.Update(PropertyMock.UPDATE_PROPERTY_REQUEST_1), Times.Once);

        _propertiesServiceAsyncMock.VerifyNoOtherCalls();

        Assert.IsInstanceOf<NoContentResult>(resultUpdateProperty);
    }

    [Test]
    public async Task UpdateProperty_Controller_Failure()
    {
        var resultUpdateProperty = await _propertyController.UpdateProperty(PropertyMock.UPDATE_PROPERTY_REQUEST_2);

        _propertiesServiceAsyncMock.Verify(v => v.Update(PropertyMock.UPDATE_PROPERTY_REQUEST_2), Times.Once);

        _propertiesServiceAsyncMock.VerifyNoOtherCalls();

        Assert.IsInstanceOf<ObjectResult>(resultUpdateProperty);
    }
}