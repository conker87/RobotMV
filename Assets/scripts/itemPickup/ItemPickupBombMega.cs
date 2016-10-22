using UnityEngine;
using System.Collections;

public class ItemPickupBombMega : ItemPickup {

	protected override void Start() {

		ItemName = "Mega Bomb";

	}

	public override void GiveItem()
	{

		Player.Current.MegaBomb = true;

		base.GiveItem ();

	}

}
