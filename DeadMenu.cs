using Godot;
using System;

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
    }

}

