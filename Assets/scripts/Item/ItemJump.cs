using UnityEngine;
using System.Collections;

public class ItemJump : Item
{

	protected override void Start() {
		
		ItemName = "Jump";

	}

	public override bool GiveItem()
	{
		
		Player.Current.Jump = true;

		return base.GiveItem ();

	}

}