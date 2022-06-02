using Godot;
using GlobalSpace;

public class StartButton : Button
{
    private void ButtonClicked()
    {
        if (Pressed)
        {
            GetTree().Paused = false;
            Global.IsGameStarted = true;
            GetTree().ChangeScene("res://Scene/BaseLevel.tscn");
        }

    }

}
