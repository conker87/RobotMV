using UnityEngine;
using System.Collections;

public class ItemPickupBombMegaUpgrade : ItemPickup {

	int bombsGiven = 1;

	protected override void Start() {

		ItemName = "Mega Bomb Upgrade";

	}

	public override void GiveItem()
	{

		Player.Current.MegaBombsMaximum += bombsGiven;

		base.GiveItem ();

	}

}
