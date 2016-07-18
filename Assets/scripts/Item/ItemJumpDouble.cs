using UnityEngine;
using System.Collections;

public class ItemJumpDouble : Item
{

	protected override void Start() {

		ItemName = "Double Jump";

	}

	public override bool GiveItem()
	{
		
		Player.Current.DoubleJump = true;

		return base.GiveItem ();

	}

}