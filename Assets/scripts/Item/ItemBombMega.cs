using UnityEngine;
using System.Collections;

public class ItemBombMega : Item {

	protected override void Start() {

		ItemName = "Mega Bomb";

	}

	public override void GiveItem()
	{

		Player.Current.MegaBomb = true;

		base.GiveItem ();

	}

}
