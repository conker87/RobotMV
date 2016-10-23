﻿using UnityEngine;
using System.Collections;

public class ItemPickupBomb : ItemPickup {

	int bombsGiven = 3;

	protected override void Start() {

		ItemName = "Bomb";

	}

	public override void GiveItem()
	{

		Player.Current.Bombs = Player.Current.BombsMaximum = bombsGiven;

		base.GiveItem ();

	}

}
