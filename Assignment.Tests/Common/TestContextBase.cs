namespace Assignment.Tests.Common
{
    using DataAccess;
    using System;

    public class TestContextBase : IDisposable
    {
        protected readonly ApplicationDbContext Context;

        protected TestContextBase()
        {
            Context = TestFactory.CreateContext();
        }

        public void Dispose()
        {
            TestFactory.Destroy(Context);
        }
    }
}
