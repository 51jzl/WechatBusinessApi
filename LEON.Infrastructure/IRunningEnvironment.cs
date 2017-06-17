using System;
namespace LEON
{
	public interface IRunningEnvironment
	{
		bool IsFullTrust
		{
			get;
		}
		void RestartAppDomain();
	}
}
