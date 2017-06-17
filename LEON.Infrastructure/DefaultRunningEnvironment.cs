using System;
using System.IO;
using System.Web;
using LEON.Utilities;
namespace LEON
{
    /// <summary>
    /// 默认运行环境实现
    /// </summary>
	public class DefaultRunningEnvironment : IRunningEnvironment
	{
		private const string WebConfigPath = "~/web.config";
		private const string BinPath = "~/bin";
		private const string RefreshHtmlPath = "~/refresh.html";

        /// <summary>
        /// 是否完全信任运行环境
        /// </summary>
		public bool IsFullTrust
		{
			get
			{
				return System.AppDomain.CurrentDomain.IsHomogenous && System.AppDomain.CurrentDomain.IsFullyTrusted;
			}
		}
        /// <summary>
        /// 重新启动AppDomain
        /// </summary>
		public void RestartAppDomain()
		{
			if (this.IsFullTrust)
			{
				HttpRuntime.UnloadAppDomain();
			}
			else if (!this.TryWriteBinFolder() && !this.TryWriteWebConfig())
			{
				throw new System.ApplicationException(string.Format("需要启动站点，在非FullTrust环境下必须给\"{0}\"或者\"~/{1}\"写入的权限", "~/bin", "~/web.config"));
			}
			HttpContext current = HttpContext.Current;
			if (current != null)
			{
				if (current.Request.RequestType == "GET")
				{
					current.Response.Redirect(current.Request.RawUrl, true);
					return;
				}
				current.Response.ContentType = "text/html";
				current.Response.WriteFile("~/refresh.html");
				current.Response.End();
			}
		}
		private bool TryWriteWebConfig()
		{
			bool result;
			try
			{
				System.IO.File.SetLastWriteTimeUtc(WebUtility.GetPhysicalFilePath("~/web.config"), System.DateTime.UtcNow);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}
		private bool TryWriteBinFolder()
		{
			bool result;
			try
			{
				string physicalFilePath = WebUtility.GetPhysicalFilePath("~/binHostRestart");
				System.IO.Directory.CreateDirectory(physicalFilePath);
				using (System.IO.StreamWriter streamWriter = System.IO.File.CreateText(System.IO.Path.Combine(physicalFilePath, "log.txt")))
				{
					streamWriter.WriteLine("Restart on '{0}'", System.DateTime.UtcNow);
					streamWriter.Flush();
				}
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}
	}
}
