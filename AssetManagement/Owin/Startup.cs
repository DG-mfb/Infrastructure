﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;

namespace AssetManagement
{
	public partial class Startup
	{
		/// <summary>
		/// Configurations the OWIN application.
		/// </summary>
		/// <param name="app">The application.</param>
		public void Configuration(IAppBuilder app)
		{
			this.ConfigureAuth(app);
		}
	}
}