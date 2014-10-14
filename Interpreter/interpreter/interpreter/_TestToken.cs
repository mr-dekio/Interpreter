using NUnit.Framework;
using System;

namespace Interpreter
{
	[TestFixture ()]
	public class TestToken
	{
		[Test ()]
		public void TestCapturingText ()
		{
			Token token = new Token ();
			String testString = "Make";
			token.Text = testString;
			Assert.AreEqual (true, token.Text.Equals (testString));
		}

		[Test ()]
		public void TestCapturingType ()
		{
			Token token = new Token ();

			token.Type = Constants.TokenType.Command;
			Assert.AreEqual (Constants.TokenType.Command, token.Type);
		}
	}
}

