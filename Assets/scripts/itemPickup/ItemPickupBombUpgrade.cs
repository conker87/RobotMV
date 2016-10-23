using UnityEngine;
using System.Collections;

public class ItemPickupBombUpgrade : ItemPickup {

	int bombsGiven = 1;

	protected override void Start() {

		ItemName = "Bomb Upgrade";

	}

	public override void GiveItem()
	{

		Player.Current.BombsMaximum += bombsGiven;

		base.GiveItem ();

	}

}
