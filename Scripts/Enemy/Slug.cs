using Godot;
using System;
using Animator;

public class Slug : KinematicBody2D, IDamagable
{
	private AnimatedSprite _animated;
	private CollisionShape2D _collision;
	private Particles2D _patricles;
	private Area2D _attackArea;
	private Path2D _path2D; 
	private Vector2 _gravity = new Vector2(0,1);
	[Export] private float _gravityScale = 98.1f;
	private Vector2 _upDirection = Vector2.Up;
	private Vector2 _moveDirection;
	private RayCast2D _earthDetectorRay;
	private RayCast2D _wallDetectorRay;
	private float _pathLength;
	private float _currentPosition;
	private bool _patrolBackward;
	private bool _dead = false;
	[Export] private bool _enabledPath = true;
	[Export] private float _speed = 10;
	[Export] private int _damage = 1;
	[Export] private int _force = 5;
	[Export] private int _firstPoint = 0;
	private Vector2[] _pathPoints;

	private int NumberOfPoints() => (int)(_pathLength / _path2D.Curve.BakeInterval);
	public override void _Ready()
	{
		_animated = GetNode<AnimatedSprite>("Animation");
		_collision = GetNode<CollisionShape2D>("Collision");
		_attackArea = GetNode<Area2D>("AttackArea");
		_patricles = GetNode<Particles2D>("Animation/Particles2D");
		_earthDetectorRay = GetNode<RayCast2D>("Rays/EarthDetector");
		_wallDetectorRay = GetNode<RayCast2D>("Rays/WallDetector");
/*
		if (HasNode("Node/Path2D"))
		{
			_path2D = GetNode<Path2D>("Node/Path2D");
			_pathLength = _path2D.Curve.GetBakedLength();
			_pathPoints = new Vector2[(int)_pathLength];
			for (int i = 0; _pathLength / _path2D.Curve.BakeInterval > i ; i ++)
			{
				_pathPoints[i] = _path2D.Curve.GetBakedPoints()[i];
			}
		}  
		*/  
	}

	public override void _PhysicsProcess(float delta)
	{
		Move();
	}

	public async void Hit()
	{
		_dead = true;
		_animated.Play("Hurt");
		_enabledPath = false;
		_patricles.QueueFree();
		_collision.QueueFree();
		_attackArea.QueueFree();
		_gravityScale = 0;
		await ToSignal(_animated , "animation_finished");
		_animated.Play("Destroy");
		await ToSignal(_animated, "animation_finished");
		QueueFree();
	}

	public void Attack(Node2D player)
	{
		if (player is Player)
		{
			((Player)player).TakeDamage(_damage, player.GlobalPosition.DirectionTo(GlobalPosition), _force);
		}
	}

	private void Move()
	{
		if (_dead) return;
		_moveDirection = _gravity * _gravityScale;

		if ((!_earthDetectorRay.IsColliding() && IsOnFloor()) || (_wallDetectorRay.IsColliding() && IsOnFloor()))
		{
			Node2D RaysNode = GetNode<Node2D>("Rays");
			if (!_patrolBackward)
			{
				RaysNode.Scale = new Vector2(-RaysNode.Scale.x,RaysNode.Scale.y);
				_patrolBackward = true;
			}
			else if (_patrolBackward)
			{
				RaysNode.Scale = new Vector2(-RaysNode.Scale.x,RaysNode.Scale.y);
				_patrolBackward = false;
			}

		}
		if (!IsOnFloor())
		{
			_moveDirection.x = 0;
			_moveDirection.y = 150f;
			MoveAndSlide(new Vector2(0f,1f * _gravityScale), _upDirection);
		}
		else
		{
			if (!_patrolBackward)
			{
				_moveDirection.x = _wallDetectorRay.CastTo.x * _speed;
				MoveAndSlide(_moveDirection, _upDirection);
				AnimatorScript.RotateSprite(-_moveDirection,_animated);
				_animated.Play("Run");
			}
			if (_patrolBackward)
			{
				_moveDirection.x = -_wallDetectorRay.CastTo.x * _speed;
				MoveAndSlide(_moveDirection, _upDirection);
				AnimatorScript.RotateSprite(-_moveDirection,_animated);
				_animated.Play("Run");
			}
		}
	}
/*
	private void MovementToPath(float delta)
	{
		if (!_enabledPath) return;
		if (_path2D == null) return;
		if (_firstPoint == 0) _patrolBackward = false;
		if (_firstPoint >=  NumberOfPoints()) _patrolBackward = true;

		if (Position.DistanceTo(_pathPoints[_firstPoint]) > 5 && (!_patrolBackward || _patrolBackward))
		{
			_moveDirection = Position.DirectionTo(_pathPoints[_firstPoint]) * _speed;
			MoveAndSlide(_moveDirection, _upDirection);
		}
		else if (!_patrolBackward && _firstPoint <  NumberOfPoints()) _firstPoint++;
		else if (_patrolBackward) _firstPoint--;
	
		if (Position.DistanceTo(_pathPoints[_firstPoint]) > 5 && _patrolBackward)
		{
			_moveDirection = Position.DirectionTo(_pathPoints[_firstPoint]) * _speed;
			MoveAndSlide(_moveDirection, _upDirection);
		}
		else if (_patrolBackward) _firstPoint--;
		



	}
	*/
}
