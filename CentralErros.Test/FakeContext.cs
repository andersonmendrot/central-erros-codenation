using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json;
using CentralErros.Infrastructure;
using CentralErros.Domain.Models;
using Environment = CentralErros.Domain.Models.Environment;

namespace CentralErros.Test
{
    public class FakeContext
    {
        public DbContextOptions<CentralErrosContext> FakeOptions { get; }

        private Dictionary<Type, string> DataFileNames { get; } =
            new Dictionary<Type, string>();
        private string FileName<T>() { return DataFileNames[typeof(T)]; }

        public FakeContext(string testName)
        {
            FakeOptions = new DbContextOptionsBuilder<CentralErrosContext>()
            //.UseSqlServer("CentralErrosContext")
            .UseInMemoryDatabase(databaseName: $"CentralErros_{testName}")
            .Options;


            DataFileNames.Add(typeof(ApplicationLayer), $"FakeData{Path.DirectorySeparatorChar}applicationlayer.json");
            DataFileNames.Add(typeof(Environment), $"FakeData{Path.DirectorySeparatorChar}environment.json");
            DataFileNames.Add(typeof(Error), $"FakeData{Path.DirectorySeparatorChar}error.json");
            DataFileNames.Add(typeof(Language), $"FakeData{Path.DirectorySeparatorChar}language.json");
            DataFileNames.Add(typeof(Level), $"FakeData{Path.DirectorySeparatorChar}level.json");
            DataFileNames.Add(typeof(User), $"FakeData{Path.DirectorySeparatorChar}user.json");
        }

        public void FillWithAll()
        {
            FillWith<ApplicationLayer>();
            FillWith<Environment>();
            FillWith<Error>();
            FillWith<Language>();
            FillWith<Level>();
            FillWith<User>();
        }

        public void FillWith<T>() where T : class
        {
            using (var context = new CentralErrosContext(FakeOptions))
            {
                if (context.Set<T>().Count() == 0)
                {
                    foreach (T item in GetFakeData<T>())
                        context.Set<T>().Add(item);
                    context.SaveChanges();
                }
            }
        }

        public List<T> GetFakeData<T>()
        {
            string content = File.ReadAllText(FileName<T>());
            //return JsonSerializer.Deserialize<List<T>>(content);
            return JsonConvert.DeserializeObject<List<T>>(content);
        }
    }
}
