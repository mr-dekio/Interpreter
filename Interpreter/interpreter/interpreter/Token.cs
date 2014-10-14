using System;
using Interpreter;

namespace Interpreter
{
	public class Token
	{
		public string Text { set; get; }
		public Constants.TokenType Type { set; get; }
	}
}

