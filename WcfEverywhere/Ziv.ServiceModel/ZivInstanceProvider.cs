﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel;
using Ziv.ServiceModel.DependencyInjection;

namespace Ziv.ServiceModel
{
    public class ZivInstanceProvider : IInstanceProvider
    {

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return DependencyResolver.Current.Resolve(instanceContext.Host.Description.ServiceType);

        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null);

        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
        }

    }
}
