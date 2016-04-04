using System;
using TechTalk.SpecFlow;

namespace OrganizationRegister.Tests.Infrastructure
{
    public static class CurrentScenarioContext
    {
        public static Guid OrganizationId
        {
            get { return ScenarioContext.Current.Get<Guid>("OrganizationId"); }
            set { ScenarioContext.Current.Set(value, "OrganizationId"); }
        }

        public static Guid ServiceId
        {
            get { return ScenarioContext.Current.Get<Guid>("ServiceId"); }
            set { ScenarioContext.Current.Set(value, "ServiceId"); }
        }
    }
}