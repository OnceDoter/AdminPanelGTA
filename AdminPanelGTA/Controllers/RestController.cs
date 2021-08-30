using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminPanelGTA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AdminPanelGTA.Controllers
{
	[Route("rest")]
	public class RestController : ControllerBase
	{
		private RpgContext _db;
		public RestController(RpgContext context)
		{
			_db = context;
		}
		[Route("Index")]
		public string Index()
		{
			var db1 = _db.Players.ToList();
			return "123";
		}

		[Route("players")]
		public JsonResult CheckPlayers()
		{
			var db1 = _db.Players.ToList();
			return new JsonResult(db1);
		}
	}
}
