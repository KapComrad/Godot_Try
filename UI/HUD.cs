using Godot;

public class HUD : Control
{
    private HBoxContainer _hpContainer;
    private Player _player;
    private Texture _hpTexture;
    public Label _labelNode;
    private int _currentScore;
    private int _currentHP;

    public override void _Ready()
    {
        _hpTexture = ResourceLoader.Load("res://Assets/Hearts/PNG/basic/heart.png") as Texture;
        _hpContainer = GetNode<HBoxContainer>(
            "/root/BaseLevel/CanvasLayer/HUD/MarginContainer/VBoxContainer/HBoxContainer/HPContainer"
        );
        _labelNode = GetNode<Label>("MarginContainer/VBoxContainer/HBoxContainer/Score");
    }

    private void UpdateHP(int hp)
    {
        if (hp > _currentHP)
        {
            for (int i = 0; i < (hp - _currentHP); i++)
            {
                TextureRect textureRect = new TextureRect();
                textureRect.Texture = _hpTexture;
                _hpContainer.AddChild(textureRect);
            }
        }
        else if (hp < _currentHP)
        {
            for (int i = 0; i < (_currentHP - hp); i++)
            {
                _hpContainer.RemoveChild(_hpContainer.GetChild(0));
            }
        }
        _currentHP = _hpContainer.GetChildCount();
        if (_currentHP == 0)
        {
            GetTree().ChangeScene("res://Scene/DeadMenu.tscn");
        }
    }

    public void PlayerHpChange(int hp)
    {
        UpdateHP(hp);
    }

    private void ScoreUpdate(int score)
    {
        _currentScore += score;
        string textScore = _currentScore.ToString();
        _labelNode.Text = textScore;
        if (_currentScore % 10 == 0)
        {
            Player.Hp++;
            UpdateHP(Player.Hp);
        }
    }

    public void AddScore(int score)
    {
        ScoreUpdate(score);
    }
}
