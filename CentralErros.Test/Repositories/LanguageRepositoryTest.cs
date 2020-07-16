using Xunit;
using CentralErros.Domain.Models;
using CentralErros.Infrastructure;
using CentralErros.Infrastructure.Repositories;
using CentralErros.Test.Comparers;
using System.Linq;

namespace CentralErros.Test.Repositories
{
    public class LanguageRepositoryTest
    {
        [Fact]
        public void ShouldGetAll()
        {
            var fakeContext = new FakeContext("LanguagesGetAll");
            fakeContext.FillWith<Language>();

            using (var context = new CentralErrosContext(fakeContext.FakeOptions))
            {
                var expected = fakeContext.GetFakeData<Language>();

                var service = new LanguageRepository(context);
                var actual = service.GetAll();

                Assert.Equal(expected, actual, new LanguageComparer());
            }
        }

        [Theory]
        [InlineData(1)]
        public void ShouldRemoveById(int id)
        {
            var fakeContext = new FakeContext("RemoveById");
            fakeContext.FillWith<Language>();

            using (var context = new CentralErrosContext(fakeContext.FakeOptions))
            {
                var data = fakeContext.GetFakeData<Language>();

                var service = new LanguageRepository(context);
                service.Remove(id);

                Assert.Equal(data.Count - 1, context.Languages.Count());
            }
        }

        [Fact]
        public void ShouldAddNewWhenSave()
        {
            var fakeContext = new FakeContext("AddWhenSave");
            var data = fakeContext.GetFakeData<Language>().First();
            data.Id = 0;

            using (var context = new CentralErrosContext(fakeContext.FakeOptions))
            {
                var service = new LanguageRepository(context);
                service.Save(data);

                Assert.NotEqual(0, context.Languages.First().Id);
            }
        }


    }
}
