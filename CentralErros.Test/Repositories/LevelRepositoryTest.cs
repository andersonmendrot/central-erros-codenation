using Xunit;
using CentralErros.Domain.Models;
using CentralErros.Infrastructure;
using CentralErros.Infrastructure.Repositories;
using CentralErros.Test.Comparers;
using System.Linq;

namespace CentralErros.Test.Repositories
{
    public class LevelRepositoryTest
    {
        [Fact]
        public void ShouldGetAll()
        {
            var fakeContext = new FakeContext("LevelGetAll");
            fakeContext.FillWith<Level>();

            using (var context = new CentralErrosContext(fakeContext.FakeOptions))
            {
                var expected = fakeContext.GetFakeData<Level>();

                var service = new LevelRepository(context);
                var actual = service.GetAll();

                Assert.Equal(expected, actual, new LevelComparer());
            }
        }

        [Theory]
        [InlineData(1)]
        public void ShouldRemoveById(int id)
        {
            var fakeContext = new FakeContext("RemoveById");
            fakeContext.FillWith<Level>();

            using (var context = new CentralErrosContext(fakeContext.FakeOptions))
            {
                var data = fakeContext.GetFakeData<Level>();

                var service = new LevelRepository(context);
                service.Remove(id);

                Assert.Equal(data.Count - 1, context.Levels.Count());
            }
        }

        [Fact]
        public void ShouldAddNewWhenSave()
        {
            var fakeContext = new FakeContext("AddWhenSave");
            var data = fakeContext.GetFakeData<Level>().First();
            data.Id = 0;

            using (var context = new CentralErrosContext(fakeContext.FakeOptions))
            {
                var service = new LevelRepository(context);
                service.Save(data);

                Assert.NotEqual(0, context.Levels.First().Id);
            }
        }
    }
}
