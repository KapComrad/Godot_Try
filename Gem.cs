using Godot;
using System;
using Items;

public class Gem : StaticBody2D, IPickable
{
    private HUD _hud;
    private AudioStreamPlayer _audio;
    private Timer _timer;
    private AudioStreamSample _sample;
    public override void _Ready()
    {
        _hud = GetNode<HUD>("/root/BaseLevel/CanvasLayer/HUD");
        _audio = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
        _timer = new Timer();
        _sample = (AudioStreamSample)_audio.Stream;
    }
    public void PickUp()
    {
        this.AddChild(_timer);
        _audio.Play();
        _hud.AddScore(1);
        CollisionLayer = 0;
        _sample.GetLength();
        _timer.WaitTime = _sample.GetLength();
        _timer.Start();
        GD.Print(_timer.TimeLeft);
        if (_timer.TimeLeft == 0)
            GD.Print("UAOSDOASD?");
        if (!_audio.Playing)
            QueueFree();

    }
}
