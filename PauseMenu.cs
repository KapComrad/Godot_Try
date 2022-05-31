using Godot;
using System;

namespace UI
{
    public class PauseMenu : Control
    {
        private Button _optionsButton;
        private Button _mainMenuExitButton;
        private Button _exitButton;
        public override void _Ready()
        {
            _optionsButton = GetNode<Button>("PauseContainer/CenterContainer/VBoxContainer/OptionsButton");
            _mainMenuExitButton = GetNode<Button>("PauseContainer/CenterContainer/VBoxContainer/MainMenuExitButton");
            _exitButton = GetNode<Button>("PauseContainer/CenterContainer/VBoxContainer/ExitButton");
        }

        private void OptionsButtonClicked()
        {
            if (_optionsButton.Pressed)
            {
                GetNode<PanelContainer>("PauseContainer").Visible = false; 
                GetNode<Control>("OptionsMenu").Visible = true;
            }

        }

        private void MainMenuExitButtonClicked()
        {
            if (_mainMenuExitButton.Pressed)
            {
                GetTree().ChangeScene("res://UI/MainMenu/MainScreen.tscn");
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

