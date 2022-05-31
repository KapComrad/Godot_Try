using Godot;
using System;
using GlobalSpace;

namespace UI
{
	public class MainScreen : Control
	{
		private Button _startButton;
		private Button _optionsButton;
		private Button _exitButton;
		public override void _Ready()
		{
			_startButton = GetNode<Button>("HMainContainter/StartButton");
			_optionsButton = GetNode<Button>("HMainContainter/OptionsButton");
			_exitButton = GetNode<Button>("HMainContainter/ExitButton");
		}

		private void StartButtonClicked()
		{
			if (_startButton.Pressed)
			{
				GetTree().Paused = false;
				Global.IsGameStarted = true;
				GetTree().ChangeScene("res://Scene/BaseLevel.tscn");
			}
		}

		private void OptionsButtonClicked()
		{
			if (_optionsButton.Pressed)
			{
				GetNode<Control>("OptionsMenu").Visible = true;
			}
		}

		private void ExitButtonClicked()
		{
			if (_exitButton.Pressed)
			{
				GetTree().Quit();
			}
		}
	}

}

