using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using KvantCard.View;
using KvantShared.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace KvantCard
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private AppStarter _starter;

        public App()
        {

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _starter = new AppStarter(e.Args);
            //var isConsole = AppStarter.IsConsole();

            var workWindow = _starter.Provider.GetService<MainWindow>();
            workWindow.Show();
            base.OnStartup(e);
        }



    }
}
