using Godot;
using System;

public class Player : KinematicBody2D
{
	#region Private variable
	private PlayerAnimator _animator;
	private Vector2 _moveDirection;
	private Vector2 _upDirection = Vector2.Up;
	private Vector2 _gravity = new Vector2(0,1);
	private float _speed = 100;
	private float _jumpForce = -5f;
	private float _airVelocity = 0;
    private bool _isOnFloor;
	private bool _rotateObject;
	#endregion


    public override void _Ready()
    {
		base._Ready();
        _animator = GetNode<PlayerAnimator>("/root/BaseLevel/Player/Animation");
    }
	public override void _PhysicsProcess(float delta)
	{
		Movement();

        _isOnFloor = IsOnFloor();
		_animator.PlayMoveAnimation(_moveDirection, _isOnFloor, _airVelocity);
		_animator.RotateSprite(_moveDirection);
	}

	private void Movement()
	{
		_moveDirection += _gravity / 4;

		if (Input.IsActionPressed("MoveRight"))
		{
			_moveDirection.x = 1;
		}
		else if (Input.IsActionPressed("MoveLeft"))
		{
			_moveDirection.x = -1;
		}
		else
		{
			_moveDirection.x = 0;
		}
		Jump();
		MoveAndSlide(_moveDirection * _speed, _upDirection);
	}


	private void Jump()
	{
		if (Input.IsActionJustPressed("Jump") && _isOnFloor)
		{
			_airVelocity = _jumpForce;
		}
		if (_airVelocity <= 0)
		{
			_airVelocity += _gravity.y / 4;
			_moveDirection.y = _airVelocity;
		}
		//Console.WriteLine(_airVelocity);
	}

	private void OnEnemyContact(Node2D enemy)
	{
		GD.Print(enemy);
		if (enemy.HasMethod("PlayAnimationHurt"))
		{
			GD.Print("PlayAnimationHurt");
			enemy.Call("PlayAnimationHurt");
		}
	}

	/*
	private void RotatePlayer(Vector2 moveDirection)
	{
		float x = 1f;
		if (moveDirection.x > 0)
			Scale = new Vector2(x,Scale.y);
		else if (moveDirection.x < 0)
			Scale = new Vector2(-x,Scale.y);
		Console.WriteLine(Scale);

	}
	*/


}
