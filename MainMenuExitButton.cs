using Godot;
using System;

public class MainMenuExitButton : Button
{
    private void ButtonClicked()
    {
        if (Pressed)
        {
            GetTree().ChangeScene("res://UI/MainMenu/MainScreen.tscn");
        }

    }

}
