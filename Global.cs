using Godot;
using System;

namespace GlobalSpace
{
public class Global : Node
{
    public static bool IsGameStarted = false;
    private bool _showPauseScreen = false;
    public static bool ShowMainScreen = false;

    public override void _Ready()
    {
        PauseMode = PauseModeEnum.Process;
    }
  public override void _Process(float delta)
  {
      if (IsGameStarted && Input.IsActionJustPressed("Cancel"))
      {
        TogglePause();
      }
  }

  private void TogglePause()
   {
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

