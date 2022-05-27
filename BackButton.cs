using Godot;
using System;

public class BackButton : Button
{
    private void ButtonClicked()
    {
        if (Pressed)
        {
            GetNode<Control>("/root/PauseMenu/OptionsMenu").Visible = false;
            GetNode<PanelContainer>("/root/PauseMenu/PauseContainer").Visible = true;
        }

    }

}
