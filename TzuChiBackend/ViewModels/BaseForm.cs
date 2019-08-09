
using System.Web.Mvc.Ajax;

namespace TzuChiBackend.ViewModels
{
	public abstract class BaseForm
	{
		public string FormId { get; set; }

		public string UpdateTargetId { get; set; }

		public bool Ajax { get; set; }

		public AjaxOptions AjaxOptions { get; set; }

		public string Action { get; set; }

		public string Controller { get; set; }

		public object RouteValues { get; set; }

		public bool Confirmed { get; set; }

		public string ErrorMsg { get; set; }

		public string RedirectUrl { get; set; }

		protected static string ErrorIconHtml
		{
			get
			{
				return "<span class='glyphicon glyphicon-exclamation-sign'></span>";
			}
		}

		public string CurrentUserId { get; set; }
	}

}