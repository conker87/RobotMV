using UnityEngine;
using System.Collections;

public class ItemJumpTriple : Item
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