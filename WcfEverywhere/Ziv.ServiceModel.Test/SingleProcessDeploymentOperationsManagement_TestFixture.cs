using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ziv.ServiceModel.Operations;
using Ziv.ServiceModel.Operations.OperationsManager;

namespace Ziv.ServiceModel.Test
{
    [TestClass]
    public class SingleProcessDeploymentOperationsManagement_TestFixture : IOperationsManager_TestFixture
    {
        protected override IOperationsManager GetOperationsManager()
        {
            return new SingleProcessDeploymentOperationsManager();
        }
    }
}
