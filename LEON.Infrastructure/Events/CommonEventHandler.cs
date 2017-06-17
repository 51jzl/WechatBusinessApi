using System;
namespace LEON.Events
{
	public delegate void CommonEventHandler<S, A>(S sender, A eventArgs);
}
