using System;
using System.Collections.Generic;
using System.Web;

namespace ClinPhone.Wiki.Parser
{
	internal class StateCollection : Dictionary<string, State>
	{
		private List<string> _AllModes;
		private string _AllPatterns;

		public List<string> GetAllModes()
		{
			if (_AllModes == null) BuildAll();

			return _AllModes;
		}

		public string GetAllPatterns()
		{
			if (_AllModes == null) BuildAll();

			return _AllPatterns;
		}

		private void BuildAll()
		{
			_AllModes = new List<string>();
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			foreach (KeyValuePair<string, State> item in this)
			{
				if (item.Key != "root")
				{
					_AllModes.Add(item.Key);
					sb.AppendFormat("|(?:{0})", item.Value.Pattern);
				}
			}

			if (sb.Length > 0)
				sb.Remove(0, 1);

			sb.Insert(0, "(?:");
			sb.Append(")");

			_AllPatterns = sb.ToString();
		}
	}
}
