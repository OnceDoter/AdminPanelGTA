using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminPanelGTA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace AdminPanelGTA.Services
{
	public static class Filter
	{
		public static IQueryable<Player> FilterContext(PlayerRequest request, IQueryable<Player> players)
		{
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
			return players;
		}

		public static Player Create(PlayerCreate request, Player player)
		{
				double level;
				if (request.Banned) player.Banned = true;
				else player.Banned = false;
				player.Name = request.Name;
				player.Title = request.Title;
				player.Race = request.Race;
				player.Profession = request.Profession;
				player.Birthday = DateTime.Now.AddYears(-request.BirthDay);
				player.Experience = request.Experience;
				level = (Math.Sqrt(2500 + (200 * player.Experience)) - 50) / 100;
				player.Level = Convert.ToInt16(level);
				player.UntilNextLevel = 50 * (player.Level + 1) * (player.Level + 2) - player.Experience;
				return player;
		}
	}
}
