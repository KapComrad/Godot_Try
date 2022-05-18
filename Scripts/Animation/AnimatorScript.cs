using Godot;
using System;

public class AnimatorScript : AnimatedSprite
{
	public void PlayMoveAnimation(Vector2 moveDirection, bool isOnFloor,AnimatedSprite animatedSprite, float airVelocity = 0)
	{
		if (moveDirection.x == 0 && isOnFloor)
		{
			animatedSprite.Play("Idle");
		}
		else if (moveDirection.x !=0 && isOnFloor)
		{
			animatedSprite.Play("Run");
		}

		if (airVelocity < 0 && !isOnFloor)
		{
			animatedSprite.Play("JumpUp");
		}
		else if (airVelocity > 0 && !isOnFloor)
		{
			animatedSprite.Play("JumpDown");
		}
	}

	public void RotateSprite(Vector2 moveDirection, AnimatedSprite animatedSprite)
	{
		if (moveDirection.x > 0)
			animatedSprite.FlipH = false;
		else if (moveDirection.x <0)
			animatedSprite.FlipH = true;
	}
}
