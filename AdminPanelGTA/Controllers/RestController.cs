using System;
using System.Linq;
using System.Text.Json;
using AdminPanelGTA.Models;
using AdminPanelGTA.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanelGTA.Controllers
{
	[Route("rest")]
	public class RestController : ControllerBase
	{
		private const string Error404 = "Error 404";
		private const string Error400 = "Error 400";
		private RpgContext _db;
		public RestController(RpgContext context)
		{
			_db = context;
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
			players = Filter.GetPlayers(request, players);
			return new JsonResult(players);
		} // Получить список игроков

		[Route("getcount")]
		public IActionResult GetCount([FromBody]PlayerRequest request)
		{
			IQueryable<Player> players = _db.Players;
			players = Filter.GetPlayers(request, players);
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
				IQueryable<Player> players = _db.Players;
				var idFromBd = players.OrderByDescending(e => e.ID).FirstOrDefault()?.ID;
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
				var idFromBd = players.OrderByDescending(e => e.ID).FirstOrDefault()?.ID;
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
			Player player1;
			string playerError = Error400;
			try
			{
				player1 = _db.Players.Single(p => p.ID == request.ID);
				if (player1 == null)
				{
					playerError = Error404;
					throw new Exception();
				}
				Filter.Update(player1, request);
				_db.SaveChanges();
			}
			catch
			{
				return new JsonResult(playerError);
			}
			return new JsonResult(player1);
		} // Обновление ифнормации об игроке
	}
}