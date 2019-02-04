using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using KvantCard;
using KvantCard.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace KvantTest
{
    public class UnitTest1 : AbstractIntegrationTest
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal("C:\\Apps\\Projects\\_Dy\\KVANTCARD", AppStarter.ContentRoot);
            var loggerFactory = Provider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<UnitTest1>();
            logger.LogError("Error test");
            DoInScope((s, p) =>
            {
                using (var db = p.GetRequiredService<Db>())
                {
                    var students = db.Students.ToList();
                }
            });

        }

        public UnitTest1(TestAppFixture testAppFixture) : base(testAppFixture)
        {
        }
    }
}
