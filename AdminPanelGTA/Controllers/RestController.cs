using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AdminPanelGTA.Models;
using AdminPanelGTA.Services;
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
			players = Filter.FilterContext(request, players);
			return new JsonResult(players);
		} // Получить список игроков

		[Route("getcount")]
		public IActionResult GetCount([FromBody]PlayerRequest request)
		{
			IQueryable<Player> players = _db.Players;
			players = Filter.FilterContext(request, players);
			return new JsonResult(players.Count());
		} // Получение количества игроков

		[Route("CreatePlayer")]
		public IActionResult CreatePlayer([FromBody]PlayerCreate request)
		{
			Player player = new Player();
			try
			{
				if (string.IsNullOrEmpty(request.Name) ||
				    request.Name.Length > 12 ||
				    request.Title.Length > 30 ||
				    request.Experience > 10000000 ||
				    request.Experience < 0 ||
				    request.BirthDay < 2000 ||
				    request.BirthDay > 3000) throw new Exception();

				player = Filter.Create(request, player);
				_db.Players.Add(player);
				_db.SaveChanges();
			}
			catch
			{
				return new JsonResult("Ошибка 404");
			}

			return new JsonResult(player);
		} // Внесение нового игрока

		[Route("getplayer")]
		public IActionResult GetPlayer([FromBody]JsonElement data)
		{
			IQueryable<Player> players = _db.Players;
			int a;
			try
			{
				a = data.GetProperty("Id").GetInt32();
			}
			catch
			{
				return new JsonResult("Ошибка 400");
			}
			try
			{
				var idFromBd = players.OrderByDescending(e => e.ID).FirstOrDefault().ID;
				if (idFromBd < a) throw new Exception();
				players = players.Where(p => p.ID == a);
				return new JsonResult(players);
			}
			catch 
			{
				return new JsonResult("Ошибка 404");
			}
		} // Получение игрока по айди

		[Route("DeletePlayer")]
		public IActionResult DeletePlayer([FromBody] JsonElement data)
		{
			IQueryable<Player> players = _db.Players;
			int a;
			try
			{
				a = data.GetProperty("Id").GetInt32();
			}
			catch
			{
				return new JsonResult("Ошибка 400");
			}
			try
			{
				var player = _db.Players.Single(p => p.ID == a);
				var idFromBd = players.OrderByDescending(e => e.ID).FirstOrDefault().ID;
				if (idFromBd < a) throw new Exception();
				_db.Players.Remove(player);
				_db.SaveChanges();
				return new JsonResult("OK");
			}
			catch
			{
				return new JsonResult("Ошибка 404");
			}
		} // Удаление игрока по айди

		[Route("UpdatePlayer")]
		public IActionResult UpdatePlayer([FromBody] PlayerRequest request)
		{
			var query = _db.Players.Where(p => p.ID == request.ID);
			try
			{
				query = Filter.Update(query, request);
			}
			catch
			{
				return new JsonResult("Ошибка 400");
			}
			_db.SaveChanges();
			Player player1 = new Player();
			try
			{
				player1 = _db.Players.Single(p => p.ID == request.ID);
			}
			catch
			{
				return new JsonResult("Ошибка 404");
			}

			return new JsonResult(player1);
		} // Обновление ифнормации об игроке
	}
}