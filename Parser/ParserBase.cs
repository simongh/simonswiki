using System;
using System.Collections.Generic;
using System.Web;

namespace ClinPhone.Wiki.Parser
{
	public delegate void StateChangeEventHandler(object sender, StateChangeEventArgs e);

	public abstract class ParserBase
	{
		public event StateChangeEventHandler EnterState;
		public event StateChangeEventHandler ExitState;

		public abstract void Initialise();

		public virtual void Parse(string text)
		{
			Parse(text, "", "root");
		}

		protected abstract void Parse(string text, string preText, string state);

		protected virtual void OnEnterState(string state, string initalText)
		{
			StateChangeEventArgs e = new StateChangeEventArgs(initalText, state);

			if (EnterState != null)
				EnterState(this, e);
		}

		protected virtual void OnExitState(string state, string initalText)
		{
			StateChangeEventArgs e = new StateChangeEventArgs(initalText, state);

			if (ExitState != null)
				ExitState(this, e);
		}
	}

	public class StateChangeEventArgs : EventArgs
	{
		public string StartString
		{
			get;
			private set;
		}

		public string Mode
		{
			get;
			private set;
		}

		public StateChangeEventArgs(string startString, string mode)
		{
			this.StartString = startString;
			this.Mode = mode;
		}
	}

}
