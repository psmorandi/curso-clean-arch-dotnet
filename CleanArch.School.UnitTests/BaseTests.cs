namespace CleanArch.School.UnitTests
{
    using System;

    public abstract class BaseTests : IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}