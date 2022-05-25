using Godot;
using System;

public class StartButton : Button
{
    private void ButtonClicked()
    {
        if (Pressed)
        {
            GetTree().ChangeScene("res://Scene/BaseLevel.tscn");
        }

    }

}
