using Godot;
using System;

public class HUD : Control
{
    private HBoxContainer _hpContainer;
    private Texture _hpTexture;
    public override void _Ready()
    {
        _hpTexture = ResourceLoader.Load("res://Assets/Hearts/PNG/basic/heart.png") as Texture;
        _hpContainer = GetNode<HBoxContainer>("/MarginContainer/VBoxContainer/HBoxContainer/HPContainer");

    }

public override void _Process(float delta)
    {
      
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
                
            }
        }

    }
}
