using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace KvantShared.Repos
{
    public class UnitOfWorkFactory
    {
        private readonly IServiceScopeFactory _factory;

        public UnitOfWorkFactory(IServiceScopeFactory factory)
        {
            _factory = factory;
        }

        public IUnitOfWork Create()
        {
            var unit = new UnitOfWork(_factory.CreateScope());
            return unit;
        }
    }
}
