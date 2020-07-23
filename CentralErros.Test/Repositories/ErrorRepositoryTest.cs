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
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void ShouldFindByApplicationLayerId(int applicationLayerId)
        {
            //ARRANGE
            //1  criar um contexto falso FakeContext
            var fakeContext = new FakeContext("FindByApplicationLayerId");

            //2  preencher o contexto falso com dados de erro
            fakeContext.FillWith<Error>();

            //3  instanciar um contexto real CentralErrosContext e
            //   fazer com que o contexto real receba as opcoes do contexto falso
            using (var context = new CentralErrosContext(fakeContext.FakeOptions))
            {
                //4  usar o FakeContext para preencher uma variavel expected com os dados esperados
                var expected = fakeContext.GetFakeData<Error>().
                    Where(x => x.ApplicationLayerId == applicationLayerId);
                //ACT
                //5  instanciar um servico ErrorRepository recebendo contexto real 
                //   como parametro para injecao de dependencia
                var service = new ErrorRepository(context);

                //6  preencher uma variavel actual com os dados obtidos pelo servico
                var actual = context.Errors.
                    Where(x => x.ApplicationLayerId == applicationLayerId);

                //ASSERT
                //7  comparar expected com actual
                Assert.Equal(expected, actual, new ErrorIdComparer());
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void ShouldFindByEnvironmentId(int environmentId)
        {
            var fakeContext = new FakeContext("FindByEnvironmentId");
            fakeContext.FillWith<Error>();

            using (var context = new CentralErrosContext(fakeContext.FakeOptions))
            {
                var expected = context.Errors.
                    Where(x => x.EnvironmentId == environmentId);

                var service = new ErrorRepository(context);
                var actual = service.FindByEnvironmentId(environmentId);

                Assert.Equal(expected, actual, new ErrorIdComparer());
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void ShouldFindByLevelId(int levelId)
        {
            var fakeContext = new FakeContext("FindByLevelId");
            fakeContext.FillWith<Error>();

            using (var context = new CentralErrosContext(fakeContext.FakeOptions))
            {
                var expected = context.Errors.
                    Where(x => x.LevelId == levelId);

                var service = new ErrorRepository(context);
                var actual = service.FindByLevelId(levelId);

                Assert.Equal(expected, actual, new ErrorIdComparer());
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void ShouldFindByLanguageId(int languageId)
        {
            var fakeContext = new FakeContext("FindByLanguageId");
            fakeContext.FillWith<Error>();

            using (var context = new CentralErrosContext(fakeContext.FakeOptions))
            {
                var expected = context.Errors.
                    Where(x => x.LanguageId == languageId);

                var service = new ErrorRepository(context);
                var actual = service.FindByLanguageId(languageId);

                Assert.Equal(expected, actual, new ErrorIdComparer());
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

        [Theory]
        [InlineData("Ascending")]
        [InlineData("Descending")]
        [InlineData("")]
        public void ShouldOrderByQuantity(string orderDirection)
        {
            var fakeContext = new FakeContext("OrderByQuantity");
            fakeContext.FillWith<Error>();

            using (var context = new CentralErrosContext(fakeContext.FakeOptions))
            {
                var service = new ErrorRepository(context);
                var actual = service.OrderByQuantity(service.GetAll(),orderDirection).ToList();
                var expected = new List<Error>();

                if (orderDirection == "Descending")
                {
                    expected = fakeContext.GetFakeData<Error>().OrderByDescending(x => x.NumberEvents).ToList();
                    Assert.Equal(expected, actual, new ErrorIdComparer());
                }

                else
                {
                    expected = fakeContext.GetFakeData<Error>().OrderBy(x => x.NumberEvents).ToList();
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
            data.NumberEvents *= 2;

            using (var context = new CentralErrosContext(fakeContext.FakeOptions))
            {
                var service = new ErrorRepository(context);
                service.Update(data);
                var actual = context.Errors.Where(x => x.Id == data.Id).SingleOrDefault();

                Assert.Equal(data, actual, new ErrorNumberEventsComparer());
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void ShouldLimitErrorListLength(int limit)
        {
            var fakeContext = new FakeContext("LimitListLength");
            fakeContext.GetFakeData<Error>();

            using (var context = new CentralErrosContext(fakeContext.FakeOptions))
            {
                var expected = context.Errors.Take(limit).ToList();

                var service = new ErrorRepository(context);
                var actual = service.LimitResultNumber(context.Errors.ToList(), limit);

                Assert.Equal(expected, actual, new ErrorIdComparer());
            }
        }
    }
}