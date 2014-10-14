using System;

namespace Interpreter
{
	public interface ICommand
	{
		void Execute(Context context);
	}

	public abstract class Command : ICommand
	{
		public Command NextCommand { set; get; }

		public abstract void Execute (Context context);
	}

	public class MoveForwardCommand : Command
	{
		override public void Execute(Context context)
		{
			Logger.Instance ().WriteLine ("move forward");

			context.Move ();
			NextCommand.Execute (context);
		}
	}

	public class TurnLeftCommand : Command
	{
		override public void Execute(Context context)
		{
			Logger.Instance ().WriteLine ("turn left");

			context.Left ();
			NextCommand.Execute (context);
		}
	}

	public class TurnRightCommand : Command
	{
		override public void Execute(Context context)
		{
			Logger.Instance ().WriteLine ("turn right");


			context.Right ();
			NextCommand.Execute (context);
		}
	}

	public class LightCommand : Command
	{
		override public void Execute(Context context)
		{
			Logger.Instance ().WriteLine ("light");


			context.Light ();
			NextCommand.Execute (context);
		}
	}

	public class EmptyCommand : Command
	{
		override public void Execute(Context context)
		{
			Logger.Instance ().WriteLine ("empty");


			if (NextCommand != null)
				NextCommand.Execute (context);
		}
	}

	public class NoCommand : Command
	{
		override public void Execute(Context context)
		{
		}
	}

	public class DeclareVariableCommand : Command
	{
		public string Name { set; get; }

		override public void Execute(Context context)
		{
			Logger.Instance ().WriteLine ("declare " + Name);


			context.SetValue (Name, 0);
			NextCommand.Execute (context);
		}
	}

	public class ComposedCommand : Command
	{
		public Command FollowingCommand { set; get; }

		public override void Execute (Context context)
		{
		}
	}

	public class CallCommand : ComposedCommand
	{
		public override void Execute (Context context)
		{
			Logger.Instance ().WriteLine ("call command");


			NextCommand.Execute (context);
			FollowingCommand.Execute (context);
		}
	}

	public class CycleCommand : ComposedCommand
	{
		public string RepeatsLexem { set; get; }

		int GetRepeats(Context context)
		{
			return ParseRepeats (context);
		}

		public override void Execute (Context context)
		{
			Logger.Instance ().WriteLine ("cycle command " + ParseRepeats (context));


			int repeats = ParseRepeats (context);
			for (int i = 0; i < repeats; i++) {
				NextCommand.Execute (context);
			}
			FollowingCommand.Execute (context);
		}

		private int ParseRepeats(Context context)
		{
			int repeats = 0;
			if (context.hasVariable (RepeatsLexem)) {
				repeats = context.GetValue (RepeatsLexem);
			} else {
				try {
					repeats = Convert.ToInt32 (RepeatsLexem);
				} catch (FormatException) {
				}
			}
			return repeats;
		}
	}

}

