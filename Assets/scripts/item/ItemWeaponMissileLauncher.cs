using UnityEngine;
using System.Collections;

public class ItemWeaponMissileLauncher : Item
{

	protected override void Start() {

		ItemName = "Missile Launcher";

	}

	public override bool GiveItem()
	{
		
		Player.Current.MissileLauncher = true;

		return base.GiveItem ();

	}

}