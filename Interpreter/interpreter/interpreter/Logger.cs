using System;
using System.IO;

namespace Interpreter
{
	public class Logger
	{
		private static StreamWriter writer;
		private static Logger log;

		protected Logger ()
		{
		}

		public static Logger Instance()
		{
			if (log == null) {
				log = new Logger ();
				writer = new StreamWriter("log.txt", false);
				writer.AutoFlush = true;
			}
			return log;
		}

		public void Write(Object obj = null)
		{
			writer.Write (obj);
		}

		public void WriteLine(Object obj = null)
		{
			writer.WriteLine (obj);
		}
	}
}

