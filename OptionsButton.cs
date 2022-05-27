using Godot;
using System;

namespace UI
{
	public class OptionsButton : Button
{
	private void ButtonClicked()
	{
		if (Pressed)
		{
			GetNode<PanelContainer>("/root/PauseMenu/PauseContainer").Visible = false;
			GetNode<Control>("/root/PauseMenu/OptionsMenu").Visible = true;
		}

	}
}
}

