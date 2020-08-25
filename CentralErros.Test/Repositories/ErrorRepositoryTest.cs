using Xunit;
using CentralErros.Domain.Models;
using CentralErros.Infrastructure;
using CentralErros.Infrastructure.Repositories;
using CentralErros.Test.Comparers;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

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

        [Theory]
        [InlineData('n')]
        [InlineData('y')]
        public void ShouldFindByStatus(char status)
        {
            var fakeContext = new FakeContext("FindByStatus");
            fakeContext.FillWith<Error>();

            using (var context = new CentralErrosContext(fakeContext.FakeOptions))
            {
                var expected = fakeContext.GetFakeData<Error>()
                    .Where(x => x.Status == status);

                var service = new ErrorRepository(context);

                var actual = service.FindByStatus(status);

                Assert.Equal(expected, actual, new ErrorIdComparer());
            }
        }

        [Theory]
        [InlineData("ascending")]
        [InlineData("descending")]
        [InlineData("")]
        public void ShouldOrderByLevel(string orderDirection)
        {
            var fakeContext = new FakeContext("OrderByLevel");
            fakeContext.FillWith<Error>();

            using (var context = new CentralErrosContext(fakeContext.FakeOptions))
            {
                var service = new ErrorRepository(context);
                var actual = service.OrderByLevel(service.GetAll(), orderDirection).ToList();
                var expected = new List<Error>();

                if (orderDirection == "descending")
                {
                    expected = fakeContext.GetFakeData<Error>().OrderByDescending(x => x.LevelId).ToList();
                    Assert.Equal(expected, actual, new ErrorIdComparer());
                }

                else
                {
                    expected = fakeContext.GetFakeData<Error>().OrderBy(x => x.LevelId).ToList();
                    Assert.Equal(expected, actual, new ErrorIdComparer());
                }
            }
        }

        [Fact]
        public void ShouldAddNewWhenSave()
        {
            var fakeContext = new FakeContext("AddWhenSave");
            var data = fakeContext.GetFakeData<Error>().First();
            data.Id = 0;

            using (var context = new CentralErrosContext(fakeContext.FakeOptions))
            {
                var service = new ErrorRepository(context);
                service.Save(data);

                Assert.NotEqual(0, context.Errors.First().Id);
            }
        }

        [Fact]
        public void ShouldGetAll()
        {
            var fakeContext = new FakeContext("GetAll");
            fakeContext.FillWith<Error>();

            using (var context = new CentralErrosContext(fakeContext.FakeOptions))
            {
                var expected = fakeContext.GetFakeData<Error>();
                var service = new ErrorRepository(context);
                var actual = service.GetAll();

                Assert.Equal(expected, actual, new ErrorIdComparer());
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void ShouldFindById(int id)
        {
            var fakeContext = new FakeContext("FindById");
            fakeContext.FillWith<Error>();

            using (var context = new CentralErrosContext(fakeContext.FakeOptions))
            {
                var expected = context.Errors.
                    Where(x => x.Id == id).
                    SingleOrDefault();

                var service = new ErrorRepository(context);
                var actual = service.GetById(id);

                Assert.Equal(expected, actual, new ErrorIdComparer());
            }
        }

        [Fact]
        public void ShouldRemove()
        {
            var fakeContext = new FakeContext("ShouldRemove");
            fakeContext.FillWith<Error>();
            var data = fakeContext.GetFakeData<Error>().First();

            using (var context = new CentralErrosContext(fakeContext.FakeOptions)) 
            {
                var expected = fakeContext.GetFakeData<Error>().Count() - 1;

                var service = new ErrorRepository(context);
                service.Remove(data);

                var actual = context.Errors.Count();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void ShouldUpdate()
        {
            var fakeContext = new FakeContext("ShouldUpdate");
            fakeContext.FillWith<Error>();
            var data = fakeContext.GetFakeData<Error>().FirstOrDefault();
            data.Details = "";

            using (var context = new CentralErrosContext(fakeContext.FakeOptions))
            {
                var service = new ErrorRepository(context);
                service.Update(data);
                var actual = context.Errors.Where(x => x.Id == data.Id).SingleOrDefault();

                Assert.Equal(data, actual, new DetailsErrorComparer());
            }
        }
    }
}