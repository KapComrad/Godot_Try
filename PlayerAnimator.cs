using Godot;
using System;

public class PlayerAnimator : AnimatedSprite
{
	private AnimatedSprite _anim;
	public override void _Ready()
	{
		base._Ready();
		_anim = GetNode<AnimatedSprite>("/root/BaseLevel/Player/Animation");
	}
	public void PlayMoveAnimation(Vector2 moveDirection, bool isOnFloor, float airVelocity)
	{
		if (moveDirection.x == 0 && isOnFloor)
		{
			Play("Idle");
		}
		else if (moveDirection.x !=0 && isOnFloor)
		{
			Play("Run");
		}

		if (airVelocity < 0 && !isOnFloor)
		{
			Play("JumpUp");
		}
		else if (airVelocity > 0 && !isOnFloor)
		{
			Play("JumpDown");
		}
	}

	public void RotateSprite(Vector2 moveDirection)
	{
		if (moveDirection.x > 0)
			FlipH = false;
		else if (moveDirection.x <0)
			FlipH = true;
	}
}
