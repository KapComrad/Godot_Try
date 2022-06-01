using Godot;
using System;

namespace Animator
{
	public static class AnimatorObject
	{
		public static void PlayMoveAnimation(Vector2 moveDirection, bool isOnFloor,AnimatedSprite animatedSprite, float airVelocity = 0)
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

		public static void RotateSprite(Vector2 moveDirection, AnimatedSprite animatedSprite)
		{
			if (moveDirection.x > 0)
				animatedSprite.Scale = new Vector2(1,1);
			else if (moveDirection.x <0)
				animatedSprite.Scale = new Vector2(-1,1);
		}
	}

}

