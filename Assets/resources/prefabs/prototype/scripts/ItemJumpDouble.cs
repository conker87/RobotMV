using UnityEngine;
using System.Collections;

public class ItemJumpDouble : Item
{

	protected override void Start() {

		ItemName = "Double Jump";

	}

	public override bool GiveItem()
	{
		
		PlayerAbilities.Current.DoubleJump = true;

		return base.GiveItem ();

	}

}