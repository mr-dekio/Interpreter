using System;
using System.Collections;
using System.Collections.Generic;

namespace Interpreter
{
	public static class Constants
	{
		public static string MoveCommand = "move";
		public static string TurnLeftCommand = "left";
		public static string TurnRightCommand = "right";
		public static string LightCommand = "light";

		public static string MainFunction = "main";
		public static string FirstFunction = "first";
		public static string SecondFunction = "second";
		public static string EndBlock = "end";
		public static string CallCommand = "call";
		public static string Variable = "var";

		public static string MakeCycle = "make";
		public static string IfCondition = "if";

		public static ArrayList CommandsList = new ArrayList () {
			MoveCommand,
			TurnLeftCommand,
			TurnRightCommand,
			LightCommand
		};
		public static ArrayList KeywordsList = new ArrayList () {
			MainFunction,
			FirstFunction,
			SecondFunction,
			EndBlock,
			CallCommand,
			Variable
		};
		static ArrayList ConditionsList = new ArrayList () {
			MakeCycle,
			IfCondition
		};

		public enum TokenType 
		{
			Command,
			Condition,
			Keyword,
			Operator,
			Unknow
		}

		public static Dictionary<TokenType, ArrayList> TokenLiterals = new Dictionary<TokenType, ArrayList> () {
			{TokenType.Command, CommandsList},
			{TokenType.Condition, ConditionsList},
			{TokenType.Keyword, KeywordsList}
		};
	}
}

