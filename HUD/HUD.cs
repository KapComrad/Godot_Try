using Godot;
using System;

public class HUD : Control
{
	private HBoxContainer _hpContainer;
	private Player _player;
	private Texture _hpTexture;
	public override void _Ready()
	{

		_hpTexture = ResourceLoader.Load("res://Assets/Hearts/PNG/basic/heart.png") as Texture;
		_hpContainer = GetNode<HBoxContainer>("/root/BaseLevel/CanvasLayer/HUD/MarginContainer/VBoxContainer/HBoxContainer/HPContainer");
	}

	public void UpdateHP(int hp)
	{
		int currentHP = _hpContainer.GetChildCount();
		if (hp > currentHP)
		{
			for (int i = 0 ; i < (hp - currentHP); i++)
			{
				var textureRect = new TextureRect();
				textureRect.Texture = _hpTexture;
				_hpContainer.AddChild(textureRect);
			}
		}
		else if (hp < currentHP)
		{
			for (int i = 0; i < (currentHP - hp); i++)
			{
				_hpContainer.RemoveChild(_hpContainer.GetChild(0));
				
			}
		}
	}
	public void PlayerHpChange(int hp)
	{
		UpdateHP(hp);
	}
}
