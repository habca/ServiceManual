using System;
using System.Linq;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using EtteplanMORE.ServiceManual.ApplicationCore.Services;
using Xunit;

// Database actions are not thread-save but there should be another way.
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace EtteplanMORE.ServiceManual.UnitTests.ApplicationCore.Services.FactoryDeviceServiceTests
{
    public class FactoryDeviceServiceTest
    {
        private IFactoryDeviceService<FactoryDevice, string> factoryDeviceService;

        public FactoryDeviceServiceTest()
        {
            factoryDeviceService = new FactoryDeviceService();
        }

        [Fact]
        public async void MongoGetAll()
        {
            var fds = await factoryDeviceService.GetAll();

            Assert.NotNull(fds);
            Assert.NotEmpty(fds);
        }

        [Fact]
        public async void MongoGet()
        {
            const string fdId = "6365789260e5e1bc2eaddc66";
            var fd = await factoryDeviceService.Get(fdId);

            Assert.NotNull(fd);
            Assert.Equal(fdId, fd.Id);
        }

        [Fact]
        public async void MongoGetNotFound()
        {
            const string fdId = "6365789260e5e1bc2eaddc68";
            var fd = await factoryDeviceService.Get(fdId);

            Assert.Null(fd);
        }

        [Fact]
        public async void MongoPostAndDelete()
        {
            int expected = (await factoryDeviceService.GetAll()).Count();
            var fd = await factoryDeviceService.Post(new FactoryDevice
            {
                Name = "Device 2022",
                Year = 2022,
                Type = "Type 2022"
            });
            var fds = await factoryDeviceService.GetAll();

            Assert.Equal(expected, fds.Count() - 1);
            Assert.NotNull(fd.Id);

            await factoryDeviceService.Delete(fd);

            fds = await factoryDeviceService.GetAll();
            Assert.Equal(expected, fds.Count());
        }

        [Fact]
        public async void MongoPut()
        {
            const string fdId = "6365789260e5e1bc2eaddc67";
            var fd = await factoryDeviceService.Get(fdId);
            var name = fd.Name;
            fd.Name = "Device XYZ";

            await factoryDeviceService.Put(fd);

            fd = await factoryDeviceService.Get(fdId);
            Assert.Equal("Device XYZ", fd.Name);

            fd.Name = name;
            await factoryDeviceService.Put(fd);
        }
    }
}