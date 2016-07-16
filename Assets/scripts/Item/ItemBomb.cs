using UnityEngine;
using System.Collections;

public class ItemBomb : Item {

	protected override void Start() {

		ItemName = "Bomb";

	}

	public override bool GiveItem()
	{

		PlayerAbilities.Current.Bomb = true;

		return base.GiveItem ();

	}

}
