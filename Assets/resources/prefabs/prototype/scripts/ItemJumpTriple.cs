using UnityEngine;
using System.Collections;

public class ItemJumpTriple : Item
{

	protected override void Start() {

		ItemName = "Triple Jump";

	}

	public override bool GiveItem()
	{
		
		PlayerAbilities.Current.TripleJump = true;

		return base.GiveItem ();

	}

}