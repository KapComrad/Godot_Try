using Godot;
using System;

public class ExitButton : Button
{
    private void ButtonClicked()
    {
        if (Pressed)
        {
            GetTree().Quit();
        }

    }
}
