using Godot;
using System;

public class Eagle : KinematicBody2D
{
	private AnimatedSprite _animated;
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animated = GetNode<AnimatedSprite>("/root/BaseLevel/Enemy/Eagle/AnimatedSprite");
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public void PlayAnimation()
	{

	} 
}
