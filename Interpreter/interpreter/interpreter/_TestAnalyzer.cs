using NUnit.Framework;
using System;
using System.Collections;

namespace Interpreter
{
	[TestFixture ()]
	public class TestAnalyzer
	{

		/*
		[Test ()]
		public void TestSimpleCommands ()
		{
			ArrayList program = new ArrayList () {
				"main",
				"move",
				"left",
				"var a",
				"move",
				"end"
			};


			ArrayList lexems = new ArrayList ();
			foreach (string line in program) {
				lexems.Add (new Lexem (line));
			}
		

			Analyzer analyzer = new Analyzer ();
			analyzer.Lexems = lexems;

			analyzer.Analyze ();



			Command main = analyzer.GetMainFunction ();

			GameMatrix matrix = new GameMatrix ();
			Context context = new Context (matrix);
			context.PrepareContext ();


			main.Execute (context);


			char[,] m = matrix.GetMatrix ();

			int rows = m.GetLength (0);
			int cols = m.GetLength (1);

			Logger log = Logger.Instance ();
			log.WriteLine (rows);
			log.WriteLine (cols);

			for (int i = 0; i < rows; i++) {
				for (int j = 0; j < rows; j++)
					log.Write (m [i, j] + " ");
				log.WriteLine ();
			}
		}
		*/

	}


}

