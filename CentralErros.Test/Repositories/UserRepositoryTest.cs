using Xunit;
using CentralErros.Domain.Models;
using CentralErros.Infrastructure;
using CentralErros.Infrastructure.Repositories;
using CentralErros.Test.Comparers;
using System.Linq;

namespace CentralErros.Test.Repositories
{
    public class UserRepositoryTest
    {
        [Fact]
        public void ShouldAddNewWhenSave()
        {
            var fakeContext = new FakeContext("AddWhenSave");
            var data = fakeContext.GetFakeData<User>().First();
            data.Id = 0;

            using (var context = new CentralErrosContext(fakeContext.FakeOptions))
            {
                var service = new UserRepository(context);
                service.Save(data);

                Assert.NotEqual(0, context.Users.First().Id);
            }
        }
    }
}
