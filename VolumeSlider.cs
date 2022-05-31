using Godot;
using System;

public class VolumeSlider : HSlider
{

    public override void _Ready()
    {
        
    }

  public override void _Process(float delta)
  {
      AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"),(float)Value);
  }
}
