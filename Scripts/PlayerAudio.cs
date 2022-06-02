using Godot;

public class PlayerAudio : AudioStreamPlayer2D
{
    private AudioStream _soundJump;
    private AudioStream _soundHit;
    private AudioStream _soundPickUp;
    public override void _Ready()
    {
        _soundJump = ResourceLoader.Load("res://Sounds/jump.wav") as AudioStream;
        _soundHit = ResourceLoader.Load("res://Sounds/hit.wav") as AudioStream;
        _soundPickUp = ResourceLoader.Load("res://Sounds/pickup.wav") as AudioStream;

    }

    public void PlayJumpSound()
    {
        Stream = _soundJump;
        Play();
    }
    public void PlayHitSound()
    {
        Stream = _soundHit;
        Play();

    }
    public void PlayPickUpSound()
    {
        Stream = _soundPickUp;
        Play();

    }

}
