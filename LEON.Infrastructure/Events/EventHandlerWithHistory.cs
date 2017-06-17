using System;
namespace LEON.Events
{
	public delegate void EventHandlerWithHistory<S, A>(S sender, A eventArgs, S historyData);
}
