using UnityEngine;
using System.Collections;

public class ItemBombMega : Item {

	protected override void Start() {

		ItemName = "Mega Bomb";

	}

	public override bool GiveItem()
	{

		Player.Current.MegaBomb = true;

		return base.GiveItem ();

	}

}
