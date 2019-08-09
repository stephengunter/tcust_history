using PagedList.Mvc;

namespace TzuChiBackend.ViewModels
{
	public abstract class BaseSearchForm : BaseForm
	{
		private string pageReplacement = "_page_";

		public string SearchUrl { get; set; }

		public string UrlStringParameters { get; set; }

		public string KeyWord { get; set; }

		public string SortBy { get; set; }

		public int OrderOptionId { get; set; }

		public bool OrderByDescending { get; set; }

		public int PageNumber { get; set; }

		public int PageSize { get; set; }

		public int DataCount { get; set; }

		public bool ShowCreateButton { get; set; }

		public string PageReplacement
		{
			get
			{
				return this.pageReplacement;
			}
			set
			{
				this.pageReplacement = value;
			}
		}

		public static string GetPageReplacement()
		{
			return "_page_";
		}

		public PagedListRenderOptions GetPagedListRenderOption()
		{
			return new PagedListRenderOptions()
			{
				DisplayLinkToFirstPage = PagedListDisplayMode.Always,
				DisplayLinkToLastPage = PagedListDisplayMode.Always,
				DisplayLinkToPreviousPage = PagedListDisplayMode.Always,
				DisplayLinkToNextPage = PagedListDisplayMode.Always,
				LinkToFirstPageFormat = "第一頁",
				LinkToPreviousPageFormat = "<span class='glyphicon glyphicon-chevron-left' aria-hidden='true'></span> 上一頁",
				LinkToNextPageFormat = "下一頁 <span class='glyphicon glyphicon-chevron-right' aria-hidden='true'></span>",
				LinkToLastPageFormat = "最末頁"
			};
		}
	}

}