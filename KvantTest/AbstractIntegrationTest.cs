using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using KvantCard.Model;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace KvantTest
{
    public class AbstractIntegrationTest : IClassFixture<TestAppFixture>
    {
        public IServiceCollection Services { get; }

        public ServiceProvider Provider { get; }

        private readonly Func<IServiceScope> _createScope;
        private IServiceScope _currentScope = null;

        // here we get our testAppFixture, also see above IClassFixture.
        protected AbstractIntegrationTest(TestAppFixture testAppFixture)
        {
            Services = testAppFixture.Services;
            Provider = testAppFixture.Provider;
            _createScope = testAppFixture.Provider.CreateScope;
        }

        protected void UseDb(Action<Db> method)
        {
            using (var scope = _createScope())
            using (var db = scope.ServiceProvider.GetService<Db>())
            {
                //db.LogToConsole();
                method(db);
            }
        }

        protected void UseDb(Action<IServiceScope, IServiceProvider, Db> method)
        {
            using (var scope = _createScope())
            using (var db = scope.ServiceProvider.GetService<Db>())
            {
                method(scope, scope.ServiceProvider, db);
            }
        }


        protected T GetService<T>()
        {
            lock (Provider)
            {
                if (_currentScope == null)
                {
                    _currentScope = _createScope();
                }
            }

            return _currentScope.ServiceProvider.GetRequiredService<T>();
        }

        ~AbstractIntegrationTest()
        {
            _currentScope?.Dispose();
        }

        protected void DoInScope(Action<IServiceScope, IServiceProvider> method)
        {
            using (var scope = _createScope())
            {
                method(scope, scope.ServiceProvider);
            }
        }

        protected async Task DoInScopeAsync(Func<IServiceScope, IServiceProvider, Task> method)
        {
            using (var scope = _createScope())
            {
                await method(scope, scope.ServiceProvider).ConfigureAwait(false);
            }
        }
    }
}