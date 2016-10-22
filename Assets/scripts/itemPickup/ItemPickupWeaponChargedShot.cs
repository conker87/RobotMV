using UnityEngine;
using System.Collections;

public class ItemPickupWeaponChargedShot : ItemPickup
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