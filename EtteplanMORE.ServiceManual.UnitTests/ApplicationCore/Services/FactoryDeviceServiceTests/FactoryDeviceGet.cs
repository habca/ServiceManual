using System;
using System.Linq;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using EtteplanMORE.ServiceManual.ApplicationCore.Services;
using Xunit;

namespace EtteplanMORE.ServiceManual.UnitTests.ApplicationCore.Services.FactoryDeviceServiceTests
{
    public class FactoryDeviceGet
    {
        [Fact]
        public async void AllCars()
        {
            IFactoryDeviceService factoryDeviceService = new FactoryDeviceService();

            var fds = (await factoryDeviceService.GetAll()).ToList();

            Assert.NotNull(fds);
            Assert.NotEmpty(fds);
            Assert.Equal(2, fds.Count);
        }

        [Fact]
        public async void ExistingCardWithId()
        {
            IFactoryDeviceService FactoryDeviceService = new FactoryDeviceService();
            string fdId = "6365789260e5e1bc2eaddc66";

            var fd = await FactoryDeviceService.Get(fdId);

            Assert.NotNull(fd);
            Assert.Equal(fdId, fd.Id);
        }

        [Fact]
        public async void NonExistingCardWithId()
        {
            IFactoryDeviceService FactoryDeviceService = new FactoryDeviceService();
            string fdId = "6365789260e5e1bc2eaddc68";

            var fd = await FactoryDeviceService.Get(fdId);

            Assert.Null(fd);
        }
    }
}