using Xunit;
using CentralErros.Domain.Models;
using CentralErros.Infrastructure;
using CentralErros.Infrastructure.Repositories;
using CentralErros.Test.Comparers;
using System.Linq;

namespace CentralErros.Test.Repositories
{
    public class ErrorRepositoryTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void ShouldChangeStatus(int id)
        {
            var fakeContext = new FakeContext("ChangeStatus");
            fakeContext.FillWith<Error>();

            using (var context = new CentralErrosContext(fakeContext.FakeOptions))
            {
                var actual = fakeContext.GetFakeData<Error>().Find(x => x.Id == id);

                var error = context.Errors
                    .Where(x => x.Id == id)
                    .SingleOrDefault();

                var service = new ErrorRepository(context);
                service.ChangeStatus(error);

                Assert.NotEqual(actual.Status, error.Status);
            }
        } 
    }
}
