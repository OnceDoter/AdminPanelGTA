using System;
using System.ComponentModel.DataAnnotations;

namespace AdminPanelGTA.Models
{
	public class Player
	{
		public int ID { get; set; }
		[MaxLength(12)]
		public string Name { get; set; }
		[MaxLength(30)]
		public string Title { get; set; }
		[MaxLength(20)]
		public string Race { get; set; }
		[MaxLength(20)]
		public string Profession { get; set; }
		public DateTime Birthday { get; set; }
		public byte Banned { get; set; }
		[MaxLength(10)]
		public int Experience { get; set; }
		[MaxLength(3)]
		public int Level { get; set; }
		[MaxLength(10)]
		public int UntilNextLevel { get; set; }
	}
}
