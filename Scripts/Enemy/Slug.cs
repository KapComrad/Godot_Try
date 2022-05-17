using Godot;
using System;

public class Slug : KinematicBody2D
{
    private AnimatedSprite _animated;
    private CollisionShape2D _collision;
    private Area2D _attackArea;
    private int _damage = 1;
    private int _force = 350;
    public override void _Ready()
    {
        _animated = GetNode<AnimatedSprite>("Animator");
        _collision = GetNode<CollisionShape2D>("Collision");
        _attackArea = GetNode<Area2D>("AttackArea");
    }

    public void PlayAnimationHurt()
    {
        _animated.Play("Hurt");
        //_collision.SetDeferred("disabled",true);
        //_attackArea.SetDeferred("monitoring",false);
        //_attackArea.SetDeferred("monitorable",false);
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
}
