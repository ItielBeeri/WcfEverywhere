using System;

namespace SevriceContract
{
    public interface IDoSomethingService
    {
        SomeResult DoSomething(SomeParameters parmaters);

        Guid DoSomethingAsync(SomeParameters parmaters);

        int DoSomethingGetProgress(Guid guid);

        bool DoSomethingGetIsComplete(Guid guid);

        SomeResult DoSomethingGetResult(Guid guid);
    }
}