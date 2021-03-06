using Godot;

namespace GlobalSpace
{
	public class Global : Node
	{
        public static float GravityScale = 98.1f;
        public static Vector2 Gravity = new Vector2(0, 1);
        public static bool IsGameStarted = false;
		private bool _showPauseScreen = false;
		public static bool ShowMainScreen = false;
        private bool _fistTimeLaunch = true;
		private bool _notused;

		public override void _Ready()
		{
			PauseMode = PauseModeEnum.Process;
            //Берется нода откуда идет сигнал -> коннект к Имени сигнала , этому ноду , вызов метода
		}

		public override void _Process(float delta)
		{

			if (IsGameStarted && Input.IsActionJustPressed("Cancel"))
			{
                if (_fistTimeLaunch)
                    GetNode<Control>("/root/BaseLevel/CanvasLayer/PauseMenu").Connect("ContinueButtonClickedSignal",this,nameof(TogglePause));
				TogglePause();
			}
		}

		public void TogglePause()
		{
            _fistTimeLaunch = false;
			_showPauseScreen = !_showPauseScreen;
			if (!ShowMainScreen)
				if (_showPauseScreen)
				{
					GetNode<Control>("/root/BaseLevel/CanvasLayer/PauseMenu").Visible = true;
				}
				else
				{
					GetNode<Control>("/root/BaseLevel/CanvasLayer/PauseMenu").Visible = false;
				}
			GetTree().Paused = _showPauseScreen;
		}
	}

}

