using UnityEngine;
using System.Collections;

public class ItemPickupBombMega : ItemPickup {

	int megaBombsGiven = 1;

	protected override void Start() {

		ItemName = "Mega Bomb";

	}

	public override void GiveItem()
	{

		Player.Current.MegaBombs = Player.Current.MegaBombsMaximum = megaBombsGiven;

		base.GiveItem ();

	}

}
