﻿using UnityEngine;
using System.Collections;

public class PowerUpItemBombMega_MaxUpgrade : PowerUp {

	int bombsGiven = 1;

	public override void Give() {

		Player.Current.Bombs_Mega_Max += bombsGiven;

		base.Give ();

	}

}