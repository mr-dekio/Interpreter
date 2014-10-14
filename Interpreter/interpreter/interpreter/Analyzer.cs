using System;
using System.Collections;

// ToDo
// write semantic analyzer
// add functions for parsing expressions
// keep in mind to write end after end of global construction

namespace Interpreter
{
	public class Analyzer
	{
		public ArrayList Lexems { set; get; }

		protected Command mainFunction = new EmptyCommand();
		protected Command firstFunction = new EmptyCommand();
		protected Command secondFunction = new EmptyCommand();
		protected int offset;

		public Analyzer ()
		{
		}

		public Analyzer (ArrayList lexems)
		{
			Lexems = lexems;
		}

		public void Analyze()
		{
			offset = 0;
			mainFunction.NextCommand = GetCommands ();

			offset++;
			firstFunction.NextCommand = GetCommands ();

			offset++;
			secondFunction.NextCommand = GetCommands ();
		}

		private Command GetCommands()
		{
			if (offset >= Lexems.Count) {
				return new NoCommand ();
			}

			Command parsedCommand = new NoCommand ();

			Lexem currentLexem = (Lexem)Lexems [offset];
			ArrayList tokens = currentLexem.GetTokens ();
			Token firstToken = (Token)tokens [0];

			switch (currentLexem.GetLexemAnalyzedType ()) {

			case Constants.TokenType.Command:
				if (tokens.Count > 1) { // error
					return new NoCommand ();
				}

				if (firstToken.Text.Equals (Constants.MoveCommand))
					parsedCommand = new MoveForwardCommand ();
				else if (firstToken.Text.Equals (Constants.TurnLeftCommand))
					parsedCommand = new TurnLeftCommand ();
				else if (firstToken.Text.Equals (Constants.TurnRightCommand))
					parsedCommand = new TurnRightCommand ();
				else if (firstToken.Text.Equals (Constants.LightCommand))
					parsedCommand = new LightCommand ();
				else
					parsedCommand = new EmptyCommand (); // by defaults

				offset++;
				parsedCommand.NextCommand = GetCommands ();

				break;



			case Constants.TokenType.Keyword:
				if (tokens.Count > 2) {
					return new NoCommand ();
				}

				if (firstToken.Text.Equals (Constants.EndBlock)) {
					return new NoCommand ();

				} else if (firstToken.Text.Equals(Constants.Variable)) {
					DeclareVariableCommand declareCommand = new DeclareVariableCommand ();
					declareCommand.Name = ((Token)tokens [1]).Text;
					offset++;
					declareCommand.NextCommand = GetCommands ();

					parsedCommand = declareCommand;
				} else if (firstToken.Text.Equals (Constants.CallCommand)) {
					Token secondToken = (Token)tokens [1];

					CallCommand callCommand = new CallCommand ();

					if (secondToken.Text.Equals (Constants.MainFunction))
						callCommand.NextCommand = mainFunction;
					else if (secondToken.Text.Equals (Constants.FirstFunction))
						callCommand.NextCommand = firstFunction;
					else if (secondToken.Text.Equals (Constants.SecondFunction))
						callCommand.NextCommand = secondFunction;

					offset++;
					callCommand.FollowingCommand = GetCommands ();

					parsedCommand = callCommand;
				} else {
					parsedCommand = new EmptyCommand ();

					offset++;
					parsedCommand.NextCommand = GetCommands ();
				}

				break;

			case Constants.TokenType.Condition:
				if (firstToken.Text.Equals (Constants.MakeCycle)) {
					if (tokens.Count > 2) {
						return new NoCommand ();
					}
					CycleCommand cycleCommand = new CycleCommand ();
					cycleCommand.RepeatsLexem = ((Token)tokens [1]).Text;

					offset++;
					cycleCommand.NextCommand = GetCommands ();
					offset++;
					cycleCommand.FollowingCommand = GetCommands ();

					parsedCommand = cycleCommand;
				}


				break;

			} // end switch
			return parsedCommand;
		}
			

		public Command GetMainFunction()
		{
			return mainFunction;
		}

		public Command GetFirstFunction()
		{
			return firstFunction;
		}

		public Command GetSecondFunction()
		{
			return secondFunction;
		}
	}
}


/*
 * 
 * move
 * left
 * right
 * light
 * cycle
 * functions
 * declaring variables
 * 
 */
