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
using Microsoft.EntityFrameworkCore.Internal;
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

		[Route("getplayers")]
		public IActionResult GetPlayers([FromBody]PlayerRequest request)
		{
			IQueryable<Player> players = _db.Players;
			if (request.Name != null) players = players.Where(p => p.Name.Contains(request.Name));
			return new JsonResult(players);
		}

		[Route("getcount")]
		public IActionResult GetCount([FromBody] PlayerRequest request)
		{
			IQueryable<Player> players = _db.Players;
			if (request.Name != null) players = players.Where(p => p.Name.Contains(request.Name));
			if (request.Title != null) players = players.Where(p => p.Title.Contains(request.Title));
			if (request.Race != null) players = players.Where(p => p.Race.Contains(request.Race));
			if (request.Profession != null) players = players.Where(p => p.Profession.Contains(request.Profession));
			if (request.MinExperience > 0) players = players.Where(p => p.Experience >= request.MinExperience);
			if (request.MaxExperience > 0) players = players.Where(p => p.Experience <= request.MaxExperience);
			if (request.MinLevel > 0) players = players.Where(p => p.Level >= request.MinLevel);
			if (request.MaxLevel > 0) players = players.Where(p => p.Level <= request.MaxLevel);
			if (request.Banned == true) players = players.Where(p => p.Banned == true);
			else players = players.Where(p => p.Banned == false);
			if (request.After > 0) players = players.Where(p => p.Birthday.Year >= request.After);
			if (request.Before > 0) players = players.Where(p => p.Birthday.Year < request.Before);
			return new JsonResult(players.Count());
		}
	}
}
