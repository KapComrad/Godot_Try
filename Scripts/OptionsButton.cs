using Godot;

public class OptionsButton : Button
{
    private void ButtonClicked()
    {
        if (Pressed)
        {
            GetNode<AnimationPlayer>("AnimationPlayer").Play("Clicked");
        }
    }

}
