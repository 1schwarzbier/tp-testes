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

public class DeletePropertyTests
{
    private readonly PropertyController _propertyController;
    private readonly Mock<IPropertyService> _propertiesServiceAsyncMock;

    public DeletePropertyTests()
    {
        _propertiesServiceAsyncMock = new Mock<IPropertyService>();

        _propertyController = new PropertyController(
            propertiesService: _propertiesServiceAsyncMock.Object);

        Setups();
    }

    private void Setups()
    {
        _propertiesServiceAsyncMock.Setup(m => m.Delete(PropertyMock.GUID_1))
            .ReturnsAsync(PropertyMock.SERVICE_RESPONSE_SUCESS);

        _propertiesServiceAsyncMock.Setup(m => m.Delete(PropertyMock.GUID_2))
            .ReturnsAsync(PropertyMock.SERVICE_RESPONSE_FAILURE);
    }

    [Test]
    public async Task DeleteProperty_Controller_Sucess()
    {
        var resultDeleteProperty = await _propertyController.DeleteProperty(PropertyMock.GUID_1);

        _propertiesServiceAsyncMock.Verify(v => v.Delete(PropertyMock.GUID_1), Times.Once);

        _propertiesServiceAsyncMock.VerifyNoOtherCalls();

        Assert.IsInstanceOf<NoContentResult>(resultDeleteProperty);
    }

    [Test]
    public async Task DeleteProperty_Controller_Failure()
    {
        var resultDeleteProperty = await _propertyController.DeleteProperty(PropertyMock.GUID_2);

        _propertiesServiceAsyncMock.Verify(v => v.Delete(PropertyMock.GUID_2), Times.Once);

        _propertiesServiceAsyncMock.VerifyNoOtherCalls();

        Assert.IsInstanceOf<ObjectResult>(resultDeleteProperty);
    }
}
