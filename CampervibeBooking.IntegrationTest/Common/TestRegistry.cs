﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using CampervibeBooking.Data.Core;
using CampervibeBooking.Data.Common;

namespace CampervibeBooking.IntegrationTest.Common
{
    public static class TestRegistry
    {
        public static IKernel Kernel { get; set; }

        public static void Configure()
        {
            Kernel = new StandardKernel();
            Kernel.Bind<IContextProvider>().To<GenericContextProvider>().InThreadScope();
            new DataRegistry().RegisterServices(Kernel);
        }
    }
}