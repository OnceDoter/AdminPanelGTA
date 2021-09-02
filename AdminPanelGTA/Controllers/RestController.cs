using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AdminPanelGTA.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

		[Route("playersfilter")]
		[HttpPost]
		public IActionResult PlayersFilter(PlayerRequest request)
		{
			IQueryable<Player> players = _db.Players;
			if (request.Name != null) players = players.Where(p => p.Name.Contains(request.Name));
			return new JsonResult(players);
		}
	}
}
