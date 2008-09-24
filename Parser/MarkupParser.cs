using System;
using System.Collections.Generic;
using System.Web;
using System.Text.RegularExpressions;

namespace ClinPhone.Wiki.Parser
{

	public class MarkupParser : ParserBase
	{
		private StateCollection _States;

		public override void Initialise()
		{
			_States = new StateCollection();

			LoadStates();	
		}

		private void LoadStates()
		{
			State TheMode = new State("<mode name=\"bold\" pattern=\"\\*\\*(.+?)\\*\\*\" hasContent=\"true\"/>", _States);
			TheMode = new State("<mode name=\"italic\" pattern=\"//(.+?)//\" hasContent=\"true\"/>", _States);
			TheMode = new State("<mode name=\"underline\" pattern=\"__(.+?)__\" hasContent=\"true\"/>", _States);
			TheMode = new State("<mode name=\"smile\" pattern=\"(:-\\))\" hasContent=\"false\"/>", _States);

			TheMode = new State("<mode name=\"root\" pattern=\"\" hasContent=\"true\"/>", _States);
		}

		protected override void Parse(string text, string preText, string state)
		{
			OnEnterState(state, preText);

			int index = 0;
			Match m = Regex.Match(text, _States[state].MatchPattern);
			while (m.Success)
			{
				string newMode = null;
				string newText = "";
				for (int i = 1; i < m.Groups.Count; i++)
				{
					newText = m.Groups[i].Value;

					if (newText != "")
					{
						newMode = _States[state].ChildStates[i - 1];
						break;
					}
				}

				if (_States[state].HasContent)
					Parse(newText, text.Substring(index, m.Index - index), newMode);

				index = m.Index + m.Length;
				m = m.NextMatch();
			}

			OnExitState(state, text.Substring(index));
		}

	}
}
