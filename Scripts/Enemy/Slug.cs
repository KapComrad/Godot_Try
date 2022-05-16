using Godot;
using System;

public class Slug : KinematicBody2D
{
    private AnimatedSprite _animated;
    private CollisionShape2D _collision;
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _animated = GetNode<AnimatedSprite>("Animator");
        _collision = GetNode<CollisionShape2D>("Collision");
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
    {

    }
    public void PlayAnimationHurt()
    {
        _animated.Play("Hurt");
        _collision.SetDeferred("disabled",true);
    }
}
