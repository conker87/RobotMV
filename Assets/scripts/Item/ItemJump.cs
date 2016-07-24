using UnityEngine;
using System.Collections;

public class ItemJump : Item
{

	protected override void Start() {
		
		ItemName = "Jump";

	}

	public override void GiveItem()
	{
		
		Player.Current.Jump = true;

		base.GiveItem ();

	}

}