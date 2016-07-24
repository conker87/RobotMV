using UnityEngine;
using System.Collections;

public class ItemJumpDouble : Item
{

	protected override void Start() {

		ItemName = "Double Jump";

	}

	public override void GiveItem()
	{
		
		Player.Current.DoubleJump = true;

		base.GiveItem ();

	}

}