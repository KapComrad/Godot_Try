using Godot;
using System;

public class Player : KinematicBody2D
{
	#region Private variable
	private AnimatorScript _animator;
	private AnimationPlayer _animationPlayer;
	private HUD _hud;
	private RayCast2D _rayCast2D;

	private Vector2 _moveDirection;
	private Vector2 _upDirection = Vector2.Up;
	private Vector2 _gravity = new Vector2(0,1);
	
	private float _speed = 100;
	private float _jumpForce = -5f;
	private float _airVelocity = 0;
	private int _hp = 3;
	private float _saveTime = 0f;
	private float _maxSaveTime = 2f;
	private bool _isOnFloor;
	private bool _rotateObject;
	private bool _inSafe;
	#endregion


	[Signal]
	public delegate void HpChange(int Hp);

	public override void _Ready()
	{
		base._Ready();
		_animator = GetNode<AnimatorScript>("Animation");
		_rayCast2D = GetNode<RayCast2D>("RayCastDown");
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		CallDeferred("ChangeHp",0);
	}

	public override void _PhysicsProcess(float delta)
	{
		Movement();
		_isOnFloor = IsOnFloor();
		
		_animator.PlayMoveAnimation(_moveDirection, _isOnFloor, _airVelocity);
		_animator.RotateSprite(_moveDirection);
		SafeTime(delta);
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
			_rayCast2D.SetDeferred("enabled", true);
			_isOnFloor = false;
		}
		if (_isOnFloor && _rayCast2D.Enabled)
			_rayCast2D.SetDeferred("enabled", false);

		if (_airVelocity <= 0)
		{
			_airVelocity += _gravity.y / 4;
			_moveDirection.y = _airVelocity;
		}
		else if (!_isOnFloor && _rayCast2D.IsColliding())
		{
			HitEnemy();
		}
		GD.Print(_rayCast2D.Enabled + " "+ _isOnFloor);
	}

	private void HitEnemy()
	{
		Node2D enemy = (Node2D)_rayCast2D.GetCollider();
		_airVelocity = _jumpForce / 1.5f;
		_isOnFloor = false;
		if (enemy.HasMethod("PlayAnimationHurt"))
		{
			enemy.Call("PlayAnimationHurt");
		}
	}

	private void SafeTime(float delta)
	{
		if (_saveTime >=0)
		{
			_saveTime -= delta;
			_animationPlayer.Play("Safe");
		}
		else
		{
			_animationPlayer.Seek(0, true);
			_animationPlayer.Stop();
			_inSafe = false;
		}
	}

	private void TakeDamage(int damage, Vector2 attackDirection)
	{
		if (!_inSafe)
		{
			ChangeHp(damage);
			_saveTime = _maxSaveTime;
			_inSafe = true;
		}
	}

	private void ChangeHp(int change)
	{
		_hp -= change;
		EmitSignal("HpChange", _hp);
	}

}
