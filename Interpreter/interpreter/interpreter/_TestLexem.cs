using NUnit.Framework;
using System;

namespace Interpreter
{
	[TestFixture ()]
	public class TestLexem
	{
		[Test ()]
		public void TestActionLexems ()
		{
			string text = "main move left right end";
			Constants.TokenType[] types = new Constants.TokenType[] {
				Constants.TokenType.Keyword, 
				Constants.TokenType.Command,
				Constants.TokenType.Command, 	
				Constants.TokenType.Command,     
				Constants.TokenType.Keyword 		
			};
			Lexem lexem = new Lexem (text);
			var tokens = lexem.GetTokens ();

			int index = 0;
			foreach (Token token in tokens) {
				Assert.AreEqual (types [index++], token.Type);
			}
		}

		[Test ()]
		public void TestVariableLexem ()
		{
			string text = "var a";
			Constants.TokenType[] types = new Constants.TokenType[] {
				Constants.TokenType.Keyword, 
				Constants.TokenType.Unknow	
			};
			Lexem lexem = new Lexem (text);
			var tokens = lexem.GetTokens ();

			int index = 0;
			foreach (Token token in tokens) {
				Assert.AreEqual (types [index++], token.Type);
			}
		}

		[Test ()]
		public void TestFunctionLexem ()
		{
			string text = "call first";
			Constants.TokenType[] types = new Constants.TokenType[] {
				Constants.TokenType.Keyword, 
				Constants.TokenType.Keyword		
			};
			Lexem lexem = new Lexem (text);
			var tokens = lexem.GetTokens ();

			int index = 0;
			foreach (Token token in tokens) {
				Assert.AreEqual (types [index++], token.Type);
			}
		}

		[Test ()]
		public void TestFunctionLexemType ()
		{
			string text = "call first";
			Lexem lexem = new Lexem (text);
			Assert.AreEqual (Constants.TokenType.Keyword, lexem.GetLexemAnalyzedType ());
		}

		[Test ()]
		public void TestVarLexemType ()
		{
			string text = "var a";
			Lexem lexem = new Lexem (text);
			Assert.AreEqual (Constants.TokenType.Keyword, lexem.GetLexemAnalyzedType ());
		}

		[Test ()]
		public void TestCommandLexemType ()
		{
			string text = "move";
			Lexem lexem = new Lexem (text);
			Assert.AreEqual (Constants.TokenType.Command, lexem.GetLexemAnalyzedType ());
		}
	}
}

