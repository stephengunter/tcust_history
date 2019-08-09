using System;
using System.Collections.Generic;
using System.Text;

namespace Tcust.Models
{
	public abstract class BasePlan:BaseEntity
	{
		public int Year { get; set; }

		public string Name { get; set; }

		public string DepartmentIds { get; set; }

		public string PartnerIds { get; set; }

		public string Manager { get; set; }

		public string Helper { get; set; }

		public DateTime? StartDate { get; set; }

		public DateTime? EndDate { get; set; }

		public string Type { get; set; }

		public int CountryAreaId { get; set; }

		public virtual CountryArea CountryArea { get; set; }

		public bool Active { get; set; }

		public DateTime CreateDate { get; set; }

		public DateTime LastUpdated { get; set; }
	}

	public class Plan : BasePlan
	{
	}
}
