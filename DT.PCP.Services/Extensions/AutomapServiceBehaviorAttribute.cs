using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using DT.PCP.Utils.Impl;

namespace DT.PCP.Services.Extensions
{
    public sealed class AutomapServiceBehaviorAttribute : Attribute, IServiceBehavior
    {
        public AutomapServiceBehaviorAttribute()
        {
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
            Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
            AutomapBootstrap.InitializeMap();
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {

        }

        public void Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {

        }
    }
}
