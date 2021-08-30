using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AdminPanelGTA.Models
{
	public class RpgContext : DbContext
	{
		public DbSet<Player> Players { get; set; }

		public RpgContext(DbContextOptions<RpgContext> options)
			: base(options)
			{
				Database.EnsureCreated();
			}
	}
}
