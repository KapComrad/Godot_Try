using Godot;

namespace UI
{
    public class DeadMenu : Control
    {
        private Button _restartButton;
        private Button _exitToMainMenuButton;

        public override void _Ready()
        {
            _restartButton = GetNode<Button>("HBoxButtonContainer/RestartButton");
            _exitToMainMenuButton = GetNode<Button>("HBoxButtonContainer/ExitToMainMenuButton");
        }

        private void RestartButtonClicked()
        {
            if (_restartButton.Pressed)
            {
                GetTree().ChangeScene("res://Scene/BaseLevel.tscn");
                GD.Print("Scene change");
            }
        }

        private void ExitToMainMenuButtonClicked()
        {
            if (_exitToMainMenuButton.Pressed)
            {
                GetTree().ChangeScene("res://UI/MainMenu/MainScreen.tscn");
            }
        }
    }
}
