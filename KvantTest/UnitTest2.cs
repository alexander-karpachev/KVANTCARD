using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KvantCard;
using KvantShared.Utils;
using Xunit;

namespace KvantTest
{
    public class UnitTest2
    {
        [Fact]
        public void Test()
        {
            var contentRoot = AppStarter.GetContentPath(TestAppFixture.ProjectName);

            Console.WriteLine($"Content Root: {contentRoot}");
        }
    }
}
