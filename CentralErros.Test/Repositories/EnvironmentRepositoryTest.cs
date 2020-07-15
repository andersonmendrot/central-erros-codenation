using Xunit;
using CentralErros.Domain.Models;
using CentralErros.Infrastructure;
using CentralErros.Infrastructure.Repositories;
using CentralErros.Test.Comparers;
using System.Linq;

namespace CentralErros.Test.Repositories
{
    public class EnvironmentRepositoryTest
    {
        [Fact]
        public void ShouldGetAll()
        {
            var fakeContext = new FakeContext("EnvironmentGetAll");
            fakeContext.FillWith<Environment>();

            using (var context = new CentralErrosContext(fakeContext.FakeOptions))
            {
                var expected = fakeContext.GetFakeData<Environment>();

                var service = new EnvironmentRepository(context);
                var actual = service.GetAll();

                Assert.Equal(expected, actual, new EnvironmentComparer());
            }
        }

        [Theory]
        [InlineData(1)]
        public void ShouldRemoveById(int id)
        {
            var fakeContext = new FakeContext("RemoveById");
            fakeContext.FillWith<Environment>();

            using (var context = new CentralErrosContext(fakeContext.FakeOptions))
            {
                var data = fakeContext.GetFakeData<Environment>();

                var service = new EnvironmentRepository(context);
                service.Remove(id);

                Assert.Equal(data.Count - 1, context.Environments.Count());
            }
        }

        [Fact]
        public void ShouldAddNewWhenSave()
        {
            var fakeContext = new FakeContext("AddWhenSave");
            var data = fakeContext.GetFakeData<Environment>().First();
            data.Id = 0;

            using (var context = new CentralErrosContext(fakeContext.FakeOptions))
            {
                var service = new EnvironmentRepository(context);
                service.Save(data);

                Assert.NotEqual(0, context.Environments.First().Id);
            }
        }
    }
}
