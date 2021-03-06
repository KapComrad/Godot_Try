using Godot;

namespace Items
{
    public class Cherry : StaticBody2D, IPickable
    {
        private AudioStreamPlayer _audio;
        private AudioStreamSample _sample;
        private AnimatedSprite _animatedSprite;

        public override void _Ready()
        {
            _audio = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
            _animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
            _sample = (AudioStreamSample)_audio.Stream;
        }

        public async void PickUp()
        {
            _audio.Play();
            Player.MaxJumpNumber++;
            CollisionLayer = 0;
            _animatedSprite.Visible = false;
            await ToSignal(GetTree().CreateTimer(_sample.GetLength()), "timeout");
            QueueFree();
        }
    }

}

