using UnityEngine;
using System.Collections;

public class ItemWeaponMissileLauncher : Item
{

	protected override void Start() {

		ItemName = "Missile Launcher";

	}

	public override void GiveItem()
	{
		
		Player.Current.MissileLauncher = true;

		base.GiveItem ();

	}

}