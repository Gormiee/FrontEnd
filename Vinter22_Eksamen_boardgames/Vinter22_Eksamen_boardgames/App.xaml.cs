using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Prism.Ioc;
using Vinter22_Eksamen_boardgames.Models;
using Vinter22_Eksamen_boardgames.ViewModels;
using Vinter22_Eksamen_boardgames.Views;
using System.Numerics;

namespace Vinter22_Eksamen_boardgames
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        public Game Games { get; set; }
    }
}
