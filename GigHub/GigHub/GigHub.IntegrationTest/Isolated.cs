using NUnit.Framework;
using System;
using System.Transactions;

namespace GigHub.IntegrationTest
{
    /// <summary>
    /// This class is made to be an attribute for method which interacts with the database
    /// when we do a test a new transaction is created  with populated data and when we finish the transation all inserted data are rolledback
    /// to keep the empty state
    /// </summary>
    public class Isolated : Attribute, ITestAction
    {
        private TransactionScope _transactionScope;

        public void BeforeTest(TestDetails testDetails)
        {
            _transactionScope = new TransactionScope();
        }

        public void AfterTest(TestDetails testDetails)
        {
            _transactionScope.Dispose();
        }

        //to specify that it can be only used for test methods
        public ActionTargets Targets { get { return ActionTargets.Test; } }
    }
}
