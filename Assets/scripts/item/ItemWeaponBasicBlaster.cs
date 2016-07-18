using UnityEngine;
using System.Collections;

public class ItemWeaponBasicBlaster : Item
{

	protected override void Start() {

		ItemName = "Basic Blaster";

	}

	public override bool GiveItem()
	{
		
		Player.Current.BasicBlaster = true;

		return base.GiveItem ();

	}

}