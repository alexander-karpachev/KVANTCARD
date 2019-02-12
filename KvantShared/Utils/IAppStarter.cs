using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace KvantShared.Utils
{
    public interface IAppStarter
    {
        string ContentRoot { get; }

        void ConfigureWithDbBuilder(DbContextOptionsBuilder options, string provider, string connectionStr);
    }
}
