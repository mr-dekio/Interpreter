using System;
using System.Collections;

namespace Interpreter
{
	public class Lexem
	{
		public string Text { get; set; }
		private ArrayList tokensList;

		public Lexem ()
		{
		}

		public Lexem(string lexem)
		{
			Text = lexem;
		}

		public ArrayList GetTokens ()
		{
			ParseTokens ();
			ParseTokensType ();

			return tokensList;
		}

		public Constants.TokenType GetLexemAnalyzedType()
		{
			var tokens = GetTokens ();
			Constants.TokenType analyzedType = Constants.TokenType.Unknow;

			foreach (Token token in tokens) {
				if ((int)analyzedType > (int)token.Type)
					analyzedType = token.Type;
			}
			return analyzedType;
		}

		protected void ParseTokens ()
		{
			tokensList = new ArrayList ();

			string [] array = Text.Split (new Char[] { ' ', '\t', '\n' });
			foreach (var text in array) {
				Token token = new Token ();
				token.Text = text;
				token.Type = Constants.TokenType.Unknow;
				tokensList.Add (token);
			}
		}

		protected void ParseTokensType()
		{
			foreach (Token token in tokensList) {
				foreach (var tokenLiteral in Constants.TokenLiterals) {
					if (tokenLiteral.Value.Contains (token.Text)) {
						token.Type = tokenLiteral.Key;
					}
				}
			}
		}

	}
}

