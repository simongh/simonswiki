using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;

namespace ClinPhone.Wiki.Parser
{
	internal class State
	{
		private string _MatchPattern;
		private List<string> _ChildStates;
		private bool _Parsed;

		public string Name
		{
			get;
			private set;
		}

		public string Pattern
		{
			get;
			private set;
		}

		public List<string> ChildStates
		{
			get
			{
				BuildChildElements();
				return _ChildStates;
			}
		}

		public bool HasContent
		{
			get;
			set;
		}

		public string MatchPattern
		{
			get 
			{
				BuildChildElements();
				return _MatchPattern;
			}
		}

		public StateCollection Parent
		{
			get;
			private set;
		}

		public State(XmlNode node, StateCollection parent)
			: this(node.OuterXml, parent)
		{ }
		
		/*
		 *	<state name="" pattern="" hasContent="">
		 *		<childStates>
		 *			<add name="*"/>
		 *		</childModes>
		 *	</mode>
		 */
		public State(string markup, StateCollection parent)
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(markup);

			switch (doc.DocumentElement.Name)
			{
				case "state":
					CreateState(doc, parent);
					break;
				case "extend":
					ExtendState(doc, parent);
					break;
				default:
					throw new ApplicationException("Unknown element in state configuration '" + doc.DocumentElement.Name + "'.");
			}
		}

		private void CreateState(XmlDocument node, StateCollection parent)
		{
			this.Name = node.DocumentElement.Attributes["name"].Value;
			this.Pattern = node.DocumentElement.Attributes["pattern"].Value;
			this.HasContent = node.DocumentElement.Attributes["hasContent"].Value.ToLower() == "true";

			_ChildStates = new List<string>();
			foreach (XmlNode item in node.SelectNodes("//childStates/add"))
			{
				_ChildStates.Add(item.Attributes["name"].Value);
			}

			this.Parent = parent;
			parent.Add(this.Name, this);
		}

		private void ExtendState(XmlDocument node, StateCollection parent)
		{
			throw new NotImplementedException("Extended states is not currently implemented.");
		}

		private void BuildChildElements()
		{
			if (_Parsed) return;

			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			if (_ChildStates.Count > 0 && _ChildStates[0] == "*")
			{
				_MatchPattern = Parent.GetAllPatterns();
				_ChildStates = Parent.GetAllModes();
			}
			else if (_ChildStates.Count > 0)
			{
				foreach (string item in _ChildStates)
				{
					if (!Parent.ContainsKey(item))
						throw new ApplicationException("An undefined mode was requested. Current mode [" + this.Name + "], child node [" + item + "].");

					sb.AppendFormat("|(?:{0})", Parent[item].Pattern);
				}

				if (sb.Length > 0)
					sb.Remove(0, 1);

				sb.Insert(0, "(?:");
				sb.Append(")");

				_MatchPattern = sb.ToString();
			}
			else
			{
				_MatchPattern = "";
			}

			_Parsed = true;
		}
	}
}
