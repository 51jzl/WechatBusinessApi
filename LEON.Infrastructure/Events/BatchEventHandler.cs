using System.Collections.Generic;
namespace LEON.Events
{
    public delegate void BatchEventHandler<S, A>(IEnumerable<S> senders, A eventArgs);
}
