using UnityEngine;
using System.Collections;

public class ItemPickupBomb : ItemPickup {

	protected override void Start() {

		ItemName = "Bomb";

	}

	public override void GiveItem()
	{

		Player.Current.Bomb = true;

		base.GiveItem ();

	}

}
