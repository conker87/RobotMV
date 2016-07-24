using UnityEngine;
using System.Collections;

public class ItemWeaponBasicBlaster : Item
{

	protected override void Start() {

		ItemName = "Basic Blaster";

	}

	public override void GiveItem()
	{
		
		Player.Current.BasicBlaster = true;

		base.GiveItem ();

	}

}