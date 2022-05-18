using Godot;
using System;

public class Player : KinematicBody2D
{
	#region Private variable
	private AnimatedSprite _animator;
	private AnimatorScript _animatorScript;
	private AnimationPlayer _animationPlayer;
	private HUD _hud;
	private RayCast2D _rayCast2D;

	private Vector2 _moveDirection;
	private Vector2 _upDirection = Vector2.Up;
	private Vector2 _gravity = new Vector2(0,1);
	private Vector2 _pushPoint;
	
	[Export] private float _speed = 100;
	[Export] private float _jumpForce = -5f;
	private float _airVelocity = 0;
	[Export] private int _hp = 3;
	private int _force;
	private float _saveTime = 0f;
	[Export] private float _maxSaveTime = 1f;
	private bool _isOnFloor;
	private bool _rotateObject;
	private bool _inSafe;
	private bool _takeDamage;
	#endregion
	#region Signals
	[Signal]
	public delegate void HpChange(int Hp);
	#endregion


	public override void _Ready()
	{
		base._Ready();
		_animatorScript = new AnimatorScript();
		_animator = GetNode<AnimatedSprite>("Animation");
		_rayCast2D = GetNode<RayCast2D>("RayCastDown");
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		CallDeferred("ChangeHp",0);
	}

	bool CanPush() => _takeDamage && Position.DistanceTo(_pushPoint) > 5;



	public override void _PhysicsProcess(float delta)
	{
		if (CanPush()) 
		{
			PushTarget(delta);
			return;
		}
		else
		{
			_takeDamage = false;
			Movement();
		}

		_isOnFloor = IsOnFloor();
		
		_animatorScript.PlayMoveAnimation(_moveDirection, _isOnFloor,_animator, _airVelocity);
		_animatorScript.RotateSprite(_moveDirection,_animator);
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
	}

	private void HitEnemy()
	{
		Node2D enemy = (Node2D)_rayCast2D.GetCollider();
		_airVelocity = _jumpForce / 1.5f;
		_isOnFloor = false;
		if (enemy != null)
		{
			((Slug)enemy).PlayAnimationHurt();
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

	public void TakeDamage(int damage, Vector2 attackDirection, int force)
	{
		if (_inSafe) return;

		ChangeHp(damage);
		_saveTime = _maxSaveTime;
		_pushPoint = GlobalPosition - attackDirection - new Vector2 (attackDirection.x * 60,attackDirection.y + 50);

		_force = force;
		_inSafe = true;
		_takeDamage = true;
	}

	private void PushTarget(float delta)
	{
		MoveAndSlide(Position.DirectionTo(_pushPoint) * _force, _upDirection);
	}

	private void ChangeHp(int change)
	{
		_hp -= change;
		EmitSignal("HpChange", _hp);
	}

}
