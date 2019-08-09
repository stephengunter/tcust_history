using System;
using System.Collections.Generic;

namespace Tcust.Models
{
	public class CountryArea:BaseEntity
	{

		public int Parent { get; set; }

		public int PartitionId { get; set; }

		public string Name { get; set; }

		public bool Active { get; set; }

		public bool IsPartition { get; set; }

		public int? CoordinateId { get; set; }

		public virtual Coordinate Coordinate { get; set; }

	}
}
