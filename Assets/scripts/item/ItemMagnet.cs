using UnityEngine;
using System.Collections;

public class ItemMagnet : Item {

	protected override void Start() {

		ItemName = "Magnet";

	}

	public override void GiveItem()
	{

		Player.Current.Magnet = true;

		base.GiveItem ();

	}

}
