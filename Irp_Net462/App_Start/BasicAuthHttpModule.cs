﻿using System;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;

namespace Irp_Net462.App_Start
{
	public class BasicAuthHttpModule : IHttpModule
	{
		private const string Realm = "My Realm";

		public void Init(HttpApplication context)
		{
			context.AuthenticateRequest += new EventHandler(BasicAuthHttpModule.OnApplicationAuthenticateRequest);
			context.EndRequest += new EventHandler(BasicAuthHttpModule.OnApplicationEndRequest);
		}

		private static void SetPrincipal(IPrincipal principal)
		{
			Thread.CurrentPrincipal = principal;
			if (HttpContext.Current != null)
			{
				HttpContext.Current.User = principal;
			}
		}

		private static bool CheckPassword(string username, string password)
		{
			bool result;
			using (SHA1Managed sha = new SHA1Managed())
			{
				byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(username + password));
				result = (Convert.ToBase64String(hash) == "fTs/Ie62TlDXc3tdvbFxtMYb/HA=");
			}
			return result;
		}

		private static void AuthenticateUser(string credentials)
		{
			try
			{
				Encoding encoding = Encoding.GetEncoding("iso-8859-1");
				credentials = encoding.GetString(Convert.FromBase64String(credentials));
				int separator = credentials.IndexOf(':');
				string name = credentials.Substring(0, separator);
				string password = credentials.Substring(separator + 1);
				if (BasicAuthHttpModule.CheckPassword(name, password))
				{
					GenericIdentity identity = new GenericIdentity(name);
					BasicAuthHttpModule.SetPrincipal(new GenericPrincipal(identity, null));
				}
				else
				{
					HttpContext.Current.Response.StatusCode = 401;
				}
			}
			catch (FormatException)
			{
				HttpContext.Current.Response.StatusCode = 401;
			}
		}

		private static void OnApplicationAuthenticateRequest(object sender, EventArgs e)
		{
			HttpRequest request = HttpContext.Current.Request;
			string authHeader = request.Headers["Authorization"];
			if (authHeader != null)
			{
				AuthenticationHeaderValue authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);
				if (authHeaderVal.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) && authHeaderVal.Parameter != null)
				{
					BasicAuthHttpModule.AuthenticateUser(authHeaderVal.Parameter);
				}
			}
		}

		private static void OnApplicationEndRequest(object sender, EventArgs e)
		{
			HttpResponse response = HttpContext.Current.Response;
			if (response.StatusCode == 401)
			{
				response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", "My Realm"));
			}
		}

		public void Dispose()
		{
		}
	}
}
