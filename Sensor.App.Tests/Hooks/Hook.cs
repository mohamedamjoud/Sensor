using System;
using Adapter.SQLLit.Context;
using BoDi;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;

namespace Sensor.App.Tests.Hooks
{
    [Binding]
    public class Hooks 
    {

        [BeforeScenario]
        public void  RegisterServices()
        {
        }
        
    }
}
        