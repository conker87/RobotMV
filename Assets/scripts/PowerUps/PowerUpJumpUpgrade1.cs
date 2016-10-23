using UnityEngine;
using System.Collections;

public class PowerUpJumpUpgrade1 : PowerUp
{

	protected override void Start() {

		PowerUpName = "Double Jump";

	}

	public override void Give()
	{
		
		Player.Current.DoubleJump = true;

		base.Give ();

	}

}