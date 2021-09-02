using System;

namespace AdminPanelGTA.Models
{
	public class PlayerRequest
	{
		public string Name { get; set; }
		public string Title;
		public string Race { get; set; }
		public string Profession { get; set; }
		public DateTime After { get; set; }
		public DateTime Before { get; set; }
		public bool Banned { get; set; }
		public int MinExperience { get; set; }
		public int MaxExperience { get; set; }
		public int MinLvel { get; set; }
		public int MaxLevel { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }

	}
}
