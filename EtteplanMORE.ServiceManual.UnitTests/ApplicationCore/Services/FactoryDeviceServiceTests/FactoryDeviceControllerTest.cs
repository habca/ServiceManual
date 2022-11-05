using System;
using System.Collections.Generic;
using System.Linq;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using EtteplanMORE.ServiceManual.ApplicationCore.Services;
using EtteplanMORE.ServiceManual.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace EtteplanMORE.ServiceManual.UnitTests.ApplicationCore.Services.FactoryDeviceServiceTests;

public class FactoryDevicesControllerTest
{
    private readonly FactoryDevicesController controller;

    public FactoryDevicesControllerTest()
    {
        var service = new FactoryDeviceService();
        controller = new FactoryDevicesController(service);
    }

    [Fact]
    public async void HttpGet()
    {
        string fdId = "6365789260e5e1bc2eaddc66";
        var response = await controller.Get(fdId) as OkObjectResult;

        Assert.NotNull(response);
        Assert.NotNull(response?.Value);

        var content = response?.Value as FactoryDeviceDto;

        Assert.Equal(200, response?.StatusCode);
        Assert.Equal(fdId, content?.Id);
    }

    [Fact]
    public async void HttpGetAll()
    {
        var response = await controller.Get() as OkObjectResult;

        Assert.NotNull(response);
        Assert.NotNull(response?.Value);

        var content = response?.Value as IEnumerable<FactoryDeviceDto>;

        Assert.Equal(200, response?.StatusCode);
        Assert.NotEmpty(content?.ToArray());
    }

    [Fact]
    public async void HttpGetNotFound()
    {
        string fdId = "6365789260e5e1bc2eaddc68";
        var response = await controller.Get(fdId) as NotFoundObjectResult;

        Assert.Null(response);
    }

    [Fact]
    public async void HttpPostAndDelete()
    {
        var dto = new FactoryDeviceDto
        {
            Name = "Device 2022",
            Year = 2022,
            Type = "Type 2022"
        };

        var response = await controller.Post(dto) as CreatedAtActionResult;

        Assert.NotNull(response);
        Assert.NotNull(response?.Value);

        var content = response?.Value as FactoryDeviceDto;

        Assert.Equal(201, response?.StatusCode);
        Assert.Equal("Get", response?.ActionName);

        dto.Id = content?.Id;

        Assert.Equal(dto.Id, content?.Id);
        Assert.Equal(dto.Name, content?.Name);
        Assert.Equal(dto.Year, content?.Year);
        Assert.Equal(dto.Type, content?.Type);

        var noresponse = await controller.Delete(dto) as NoContentResult;

        Assert.NotNull(noresponse);
        Assert.Equal(204, noresponse?.StatusCode);
    }

    [Fact]
    public async void HttpPut()
    {
        string fdId = "6365789260e5e1bc2eaddc67";

        var response = await controller.Get(fdId) as OkObjectResult;
        var content = response?.Value as FactoryDeviceDto;

        Assert.NotNull(response);
        Assert.NotNull(content);

        var noresponse = await controller.Put(content!) as NoContentResult;

        Assert.NotNull(noresponse);
        Assert.Equal(204, noresponse?.StatusCode);
    }

    [Fact]
    public async void HttpPutNotFound()
    {
        string fdId = "6365789260e5e1bc2eaddc68";
        var dto = new FactoryDeviceDto
        {
            Id = fdId,
            Name = "Device 1",
            Year = 2004,
            Type = "Type 19"
        };

        var response = await controller.Put(dto) as NotFoundObjectResult;

        Assert.Null(response);
    }
}