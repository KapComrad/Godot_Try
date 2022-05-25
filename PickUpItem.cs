using Godot;
using System;
using Items;

public class PickUpItem : Area2D
{
	public void PlayerPickUpItem(Node2D item)
	{
		if (item is IPickable)
		{
			((IPickable)item).PickUp();
		}

	}
}
