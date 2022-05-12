using Godot;
using System;

public class Player : KinematicBody2D
{
	#region Private variable
	private PlayerAnimator _animator;
	private Vector2 _moveDirection;
	private Transform2D _transform;
	private Vector2 _upDirection = Vector2.Up;
	private Vector2 _gravity = new Vector2(0,2);
	private float _speed = 100;
	private float _jumpForce = 25;
    private bool _isOnFloor;
	private bool _rotateObject;
	#endregion


    public override void _Ready()
    {
		base._Ready();
        _animator = GetNode<PlayerAnimator>("/root/BaseLevel/Player/Animation");
		_transform = new Transform2D(); 
    }
	public override void _PhysicsProcess(float delta)
	{
		Movement();
		Jump();
        _isOnFloor = IsOnFloor();
		_animator.PlayMoveAnimation(_moveDirection, _isOnFloor);
		_animator.RotateSprite(_moveDirection);
	}

	private void Movement()
	{
		_moveDirection = _gravity;

		if (Input.IsActionPressed("MoveRight"))
		{
			_moveDirection.x = 1;
		}
		else if (Input.IsActionPressed("MoveLeft"))
		{
			_moveDirection.x = -1;
		}

		if (Input.IsActionPressed("Jump") && _isOnFloor)
		{
			_moveDirection.y -= _jumpForce;
		}
		MoveAndSlide(_moveDirection * _speed, _upDirection);

	}


	private void Jump()
	{


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
