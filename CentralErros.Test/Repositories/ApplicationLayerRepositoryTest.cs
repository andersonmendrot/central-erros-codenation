using Xunit;
using CentralErros.Domain.Models;
using CentralErros.Infrastructure;
using CentralErros.Infrastructure.Repositories;
using CentralErros.Test.Comparers;
using System.Linq;

namespace CentralErros.Test.Repositories
{
    public class ApplicationLayerRepositoryTest 
    {
        [Fact]
        public void ShouldGetAll()
        {
            var fakeContext = new FakeContext("ApplicationLayerGetAll");
            fakeContext.FillWith<ApplicationLayer>();

            using (var context = new CentralErrosContext(fakeContext.FakeOptions))
            {
                var expected = fakeContext.GetFakeData<ApplicationLayer>();

                var service = new ApplicationLayerRepository(context);
                var actual = service.GetAll();

                Assert.Equal(expected, actual, new ApplicationLayerComparer());
            }
        }

        [Theory]
        [InlineData(1)]
        public void ShouldRemoveById(int id)
        {
            var fakeContext = new FakeContext("RemoveById");
            fakeContext.FillWith<ApplicationLayer>();

            using (var context = new CentralErrosContext(fakeContext.FakeOptions))
            {
                var data = fakeContext.GetFakeData<ApplicationLayer>();

                var service = new ApplicationLayerRepository(context); 
                service.Remove(id);

                Assert.Equal(data.Count - 1, context.ApplicationLayers.Count());
            }
        }

        [Fact]
        public void ShouldAddNewWhenSave()
        {
            var fakeContext = new FakeContext("AddWhenSave");
            var data = fakeContext.GetFakeData<ApplicationLayer>().First();
            data.Id = 0;

            using (var context = new CentralErrosContext(fakeContext.FakeOptions))
            {
                var service = new ApplicationLayerRepository(context);
                service.Save(data);

                Assert.NotEqual(0, context.ApplicationLayers.First().Id);
            }
        }
    }
}