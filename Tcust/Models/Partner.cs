

namespace Tcust.Models
{
	public class Partner:BaseEntity
	{
		

		public int CountryAreaId { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public bool Active { get; set; }

		public int? CoordinateId { get; set; }

		public virtual Coordinate Coordinate { get; set; }

		public virtual CountryArea CountryArea { get; set; }
	}
}
