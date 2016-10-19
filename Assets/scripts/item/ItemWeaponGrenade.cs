using UnityEngine;
using System.Collections;

public class ItemWeaponGrenade : Item
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