using UnityEngine;
using System.Collections;

public class ItemWeaponChargedShot : Item
{

	protected override void Start() {

		ItemName = "Charged Shot";

	}

	public override void GiveItem()
	{
		
		Player.Current.BasicBlasterChargeShot = true;

		base.GiveItem ();

	}

}