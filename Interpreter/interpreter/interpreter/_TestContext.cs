using NUnit.Framework;
using System;

namespace Interpreter
{
	[TestFixture ()]
	public class TestContext
	{
		[Test ()]
		public void TestNotExistsVariable ()
		{
			Context context = new Context ();
			Assert.AreEqual (false, context.hasVariable ("hello"));
		}

		[Test ()]
		public void TestFoundingVariable ()
		{
			Context context = new Context ();
			string name = "myvalue";
			context.SetValue (name, 4);
			Assert.AreEqual (true, context.hasVariable (name));
		}

		[Test ()]
		public void TestExistsVariable ()
		{
			Context context = new Context ();
			string name = "myvalue";
			context.SetValue (name, 4);
			Assert.AreEqual (4, context.GetValue(name));
		}


		/*
		[Test()]
		public void TestCreatedMatrixInLog()
		{
			Logger log = Logger.Instance ();

			GameMatrix matrix = new GameMatrix ();

			Context context = new Context (matrix);
			context.PrepareContext ();

			char[,] m = matrix.GetMatrix ();

			int rows = m.GetLength (0);
			int cols = m.GetLength (1);

			log.WriteLine (rows);
			log.WriteLine (cols);

			for (int i = 0; i < rows; i++) {
				for (int j = 0; j < rows; j++)
					log.Write (m [i, j] + " ");
				log.WriteLine ();
			}

			context.Move ();
			context.Move ();
			context.Left ();
			context.Move ();
				
			log.WriteLine ();
			for (int i = 0; i < rows; i++) {
				for (int j = 0; j < rows; j++)
					log.Write (m [i, j] + " ");
				log.WriteLine ();
			}

		}
		*/


	}
}

