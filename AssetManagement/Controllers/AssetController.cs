﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using DataModels;
using Shared.Services.Query.Assets;

namespace AssetManagement.Controllers
{
    [Authorize]
	[RoutePrefix("odata/Assets")]
	public class AssetController : BaseController<IAssetQueryService>
    {
		public AssetController(IAssetQueryService service) : base(service) { }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
			//search criteria to be passed. TBD
			//validation goes here
			//ideally a projection should be returned
			var assets = await base.Service.GetAllAsset();
			return base.Ok(assets);
        }

        [HttpGet]
		public Task<IHttpActionResult> Get(Guid id)
        {
			throw new NotImplementedException();
        }
    }
}