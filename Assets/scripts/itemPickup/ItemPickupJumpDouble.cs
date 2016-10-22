using UnityEngine;
using System.Collections;

public class ItemPickupJumpDouble : ItemPickup
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