using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace TzuChiBackend.Helpers
{
	public static class Extensions
	{
		public static bool CaseInsensitiveContains(this string text, string value)
		{
			StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase;
			return text.IndexOf(value, stringComparison) >= 0;
		}

		public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
		{
			if (enumerable == null)
			{
				return true;
			}
			/* If this is a list, use the Count property for efficiency. 
             * The Count property is O(1) while IEnumerable.Count() is O(N). */
			var collection = enumerable as ICollection<T>;
			if (collection != null)
			{
				return collection.Count < 1;
			}
			return !enumerable.Any();
		}

		public static IList<string> GetKeywords(this string input)
		{
			if (String.IsNullOrWhiteSpace(input) || String.IsNullOrEmpty(input)) return null;

			return input.Split(new string[] { "-", " " }, StringSplitOptions.RemoveEmptyEntries);

		}
		public static string RemoveScriptTags(this string htmlString)
		{
			if (String.IsNullOrEmpty(htmlString)) return "";
			//移除  javascript code.
			htmlString = Regex.Replace(htmlString, @"<script[\d\D]*?>[\d\D]*?</script>", String.Empty);

			return htmlString;


		}
		public static string RemoveHtmlTags(this string htmlString)
		{
			//移除html tag.
			if (String.IsNullOrEmpty(htmlString)) return "";
			var regexCss = new Regex("(\\<script(.+?)\\</script\\>)|(\\<style(.+?)\\</style\\>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
			htmlString = regexCss.Replace(htmlString, string.Empty);

			if (String.IsNullOrEmpty(htmlString)) return htmlString;
			string htmlTagPattern = @"<[^>]+>|&nbsp;";
			htmlString = Regex.Replace(htmlString, htmlTagPattern, string.Empty);

			if (String.IsNullOrEmpty(htmlString)) return htmlString;
			htmlString = Regex.Replace(htmlString, @"\s{2,}", " ");

			if (String.IsNullOrEmpty(htmlString)) return htmlString;
			htmlString = Regex.Replace(htmlString, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);


			return htmlString;



			//htmlString = Regex.Replace(htmlString, @"<[^>]*>", String.Empty);

			//return htmlString;
		}
		public static string RemoveSciptAndHtmlTags(this string htmlString)
		{
			if (String.IsNullOrEmpty(htmlString)) return "";
			htmlString = RemoveScriptTags(htmlString);

			return RemoveHtmlTags(htmlString).Trim();
		}
		public static string ReverseString(this string str)
		{
			return String.IsNullOrEmpty(str) ? string.Empty : new string(str.ToCharArray().Reverse().ToArray());
		}
		public static int ToInt(this string str)
		{
			int value = 0;
			if (!int.TryParse(str, out value)) value = 0;

			return value;
		}
		public static bool ToBoolean(this string str)
		{
			if (String.IsNullOrEmpty(str)) return false;

			return (str.ToLower() == "true");
		}
		public static DateTime ConvertToTaipeiTime(this DateTime input)
		{
			string taipeiTimeZoneId = "Taipei Standard Time";
			TimeZoneInfo taipeiTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(taipeiTimeZoneId);
			return TimeZoneInfo.ConvertTimeFromUtc(input.ToUniversalTime(), taipeiTimeZoneInfo);

		}
		public static int ToDateNumber(this DateTime input)
		{
			return Convert.ToInt32(GetDateString(input.Date));
		}
		public static int ToTimeNumber(this DateTime input)
		{
			return Convert.ToInt32(GetTimeString(input));
		}

		public static string GetDateString(DateTime dateTime)
		{
			string year = dateTime.Year.ToString();
			string month = dateTime.Month.ToString();
			string day = dateTime.Day.ToString();

			if (dateTime.Month < 10) month = "0" + month;
			if (dateTime.Day < 10) day = day = "0" + day;


			return year + month + day;

		}
		static string GetTimeString(DateTime dateTime)
		{
			string hour = dateTime.Hour.ToString();
			string minute = dateTime.Minute.ToString();
			string second = dateTime.Second.ToString();
			string mileSecond = dateTime.Millisecond.ToString();

			if (dateTime.Hour < 10) hour = "0" + hour;
			if (dateTime.Minute < 10) minute = "0" + minute;
			if (dateTime.Second < 10) second = "0" + second;

			if (dateTime.Millisecond < 10)
			{
				mileSecond = "00" + mileSecond;
			}
			else if (dateTime.Millisecond < 100)
			{
				mileSecond = "0" + mileSecond;
			}

			return hour + minute + second + mileSecond;

		}
	}
}