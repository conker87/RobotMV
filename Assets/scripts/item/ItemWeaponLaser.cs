using UnityEngine;
using System.Collections;

public class ItemWeaponLaser : Item {

	protected override void Start() {

		ItemName = "Laser Beam";

	}

	public override void GiveItem()
	{
		
		Player.Current.Laser = true;

		base.GiveItem ();

	}

}