using UnityEngine;
using System.Collections;

public class ItemPickupJumpTriple : ItemPickup
{

	protected override void Start() {

		ItemName = "Triple Jump";

	}

	public override void GiveItem()
	{
		
		Player.Current.TripleJump = true;

		base.GiveItem ();

	}

}