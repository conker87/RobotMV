using UnityEngine;
using System.Collections;

public class ItemWeaponBasicBlaster : Item
{

	protected override void Start() {

		ItemName = "Basic Blaster";

	}

	public override bool GiveItem()
	{
		
		PlayerAbilities.Current.BasicBlaster = true;

		return base.GiveItem ();

	}

}