using UnityEngine;
using System.Collections;

public class ItemPickupWeaponGrenade : ItemPickup
{

	protected override void Start() {

		ItemName = "Grenades";

	}

	public override void GiveItem()
	{
		
		Player.Current.DNU_Grenade = true;

		base.GiveItem ();

	}

}