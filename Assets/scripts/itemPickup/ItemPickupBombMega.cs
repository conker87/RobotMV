using UnityEngine;
using System.Collections;

public class ItemPickupBombMega : ItemPickup {

	protected override void Start() {

		ItemName = "Mega Bomb";

	}

	public override void GiveItem()
	{

		Player.Current.MegaBombsMaximum = 1;
		Player.Current.MegaBombs = 1;

		base.GiveItem ();

	}

}
