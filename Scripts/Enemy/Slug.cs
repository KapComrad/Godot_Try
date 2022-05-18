using Godot;
using System;

public class Slug : KinematicBody2D
{
	private AnimatedSprite _animated;
	private AnimatorScript _animatorScript;
	private CollisionShape2D _collision;
	private Area2D _attackArea;
	private Path2D _path2D; 
	private Vector2 _gravity = new Vector2(0,1);
	private Vector2 _upDirection = Vector2.Up;
	private Vector2 _moveDirection = new Vector2();
	private float _pathLength;
	private float _currentPosition;
	private bool _patrolBackward;
	[Export] private bool _enabledPath = true;
	[Export] private float _speed = 100;
	[Export] private int _damage = 1;
	[Export] private int _force = 300;
	[Export] private int _firstPoint = 0;
	private Vector2[] _pathPoints;

	private int NumberOfPoints() => (int)(_pathLength / _path2D.Curve.BakeInterval);
	public override void _Ready()
	{
        _animatorScript = new AnimatorScript();
		_animated = GetNode<AnimatedSprite>("Animation");
		_collision = GetNode<CollisionShape2D>("Collision");
		_attackArea = GetNode<Area2D>("AttackArea");
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
	}

	public override void _Process(float delta)
	{
		base._Process(delta);
		_moveDirection += _gravity;
		MovementToPath(delta);
	}

	public void PlayAnimationHurt()
	{
		_animated.Play("Hurt");
		//_collision.SetDeferred("disabled",true);
		//_attackArea.SetDeferred("monitoring",false);
		//_attackArea.SetDeferred("monitorable",false);
		_enabledPath = false;
		_collision.QueueFree();
		_attackArea.QueueFree();
	}

	public void Attack(Node2D player)
	{
		if (player is Player)
		{
			((Player)player).TakeDamage(_damage, player.GlobalPosition.DirectionTo(GlobalPosition), _force);
		}
	}

	private void MovementToPath(float delta)
	{
		if (!_enabledPath) return;
		if (_path2D == null) return;
		if (_firstPoint == 0) _patrolBackward = false;
		if (_firstPoint >=  NumberOfPoints()) _patrolBackward = true;



		if (Position.DistanceTo(_pathPoints[_firstPoint]) > 5 && !_patrolBackward)
		{
			_moveDirection = Position.DirectionTo(_pathPoints[_firstPoint]) * _speed;
			MoveAndSlide(_moveDirection, _upDirection);
		}
		else if (!_patrolBackward && _firstPoint <  NumberOfPoints()) _firstPoint++;

		if (Position.DistanceTo(_pathPoints[_firstPoint]) > 5 && _patrolBackward)
		{
			_moveDirection = Position.DirectionTo(_pathPoints[_firstPoint]) * _speed;
			MoveAndSlide(_moveDirection, _upDirection);
		}
		else if (_patrolBackward) _firstPoint--;

		_animated.Play("Run");
        _animatorScript.RotateSprite(-_moveDirection,_animated);

	}
}
