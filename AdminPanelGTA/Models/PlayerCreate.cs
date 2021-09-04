using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanelGTA.Models
{
	public class PlayerCreate
	{
		public string Name { get; set; }
		public string Title { get; set; }
		public string Race { get; set; }
		public string Profession { get; set; }
		public int BirthDay { get; set; }
		public bool Banned { get; set; }
		public int Experience { get; set; }
	}
}
