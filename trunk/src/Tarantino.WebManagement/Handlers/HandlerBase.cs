using System.Web;

namespace Tarantino.WebManagement.Handlers
{
	public abstract class HandlerBase : IHttpHandler
	{
		protected HttpContext m_context;
		protected bool m_authenticated = false;
		protected bool m_openHtmlWriten = false;
		protected bool m_closeHtmlWritten = false;

		protected abstract void OnProcessRequest();

		protected void WriteMenu()
		{
			System.Text.StringBuilder output = new System.Text.StringBuilder();
			output.Append("<table valign=top width='100%'><tr><td><a href='callawaygolf.tx.web.management.application.axd'>Applications</a> | <a href='callawaygolf.tx.web.management.cache.axd'>Cache</a> | <a href='callawaygolf.tx.web.management.assemblies.axd'>Assemblies</a> | <a href='callawaygolf.tx.web.management.loadbalancer.axd'>Load Balancer</a> | <a href='callawaygolf.tx.web.management.disablessl.axd'>Disable SSL(per cookie)</a></td></tr></table><br/>");
			Write(output.ToString());
		}

		protected void WriteCloseTags()
		{
			System.Text.StringBuilder output = new System.Text.StringBuilder();
			output.Append("</body></html>");
			m_closeHtmlWritten = true;
			Write(output.ToString());
		}

		protected void WriteCSS()
		{
			System.Text.StringBuilder output = new System.Text.StringBuilder();
			output.Append("<html><head>");
			output.Append("<style type='text/css'>");

			output.Append("body { ");
			output.Append("margin:0 0 0 0;");
			output.Append("}");

			output.Append("table { ");
			output.Append("background-color:#FFF;");
			output.Append("border-collapse:collapse;");
			output.Append("padding:5px;");
			output.Append("width:100%;");
			output.Append("}");

			output.Append("td { ");
			output.Append("border:1px solid black;");
			output.Append("font-family: verdana;");
			output.Append("font-size: 12;");
			output.Append("text-decoration: none; }");
			output.Append("background-color:#FFF;");
			output.Append("padding:5px;");
			output.Append("}");

			output.Append("th { ");
			output.Append("background-color: #999999;");
			output.Append("border:1px solid #000;");
			output.Append("font-family: verdana;");
			output.Append("font-size: 14;");
			output.Append("text-weight: bold; }");
			output.Append("text-decoration: none; }");


			output.Append(".center { ");
			output.Append("text-align:center;}");

			output.Append(".over{ ");
			output.Append("background-color: #CFCFCF;");
			output.Append("}");

			output.Append(".out{ ");
			output.Append("background-color: #FFFFFF;");
			output.Append("}");

			output.Append(".Online{ ");
			output.Append("color: #FFF;");
			output.Append("text-align:center;");
			output.Append("background-color: #393;");
			output.Append("text-decoration: none; ");
			output.Append("font-family: verdana;");
			output.Append("font-size: 12;");
			output.Append("text-decoration: none; ");
			output.Append("		}");

			output.Append(".Offline,.Down{ ");
			output.Append("text-align:center;");
			output.Append("color: #C00;");
			output.Append("text-decoration: underline; }");
			output.Append("</style>");
			output.Append("</head><body>");
			m_openHtmlWriten = true;

			//output.Append("<table height=100%><tr><td  align=center class=center><table>\n");

			Write(output.ToString());
		}
		#region IHttpHandler Members

		public void Reload()
		{
			m_context.Response.Redirect(m_context.Request.Url.AbsolutePath);
		}

		public void ProcessRequest(HttpContext context)
		{
			m_context = context;
			if (m_context.Request.IsAuthenticated && (m_context.User.IsInRole(@"BUILTIN\Administrators") || m_context.User.IsInRole(@"Administrators")))
			{
				m_authenticated = true;
			}

			OnProcessRequest();
			if (!m_closeHtmlWritten)
				WriteCloseTags();
		}


		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		protected void Write(string Line)
		{
			m_context.Response.Write(Line);
		}
		protected void Write(byte[] bytes)
		{
			m_context.Response.OutputStream.Write(bytes, 0, bytes.Length);
		}

		protected string Request(string key)
		{
			if (m_context.Request[key] != null)
			{
				return m_context.Request[key].ToString();
			}
			else
			{
				return string.Empty;
			}
		}
		#endregion
	}
}
