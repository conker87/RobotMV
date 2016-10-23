using UnityEngine;
using System.Collections;

public class PowerUpJumpUpgrade2 : PowerUp
{

	protected override void Start() {

		PowerUpName = "Triple Jump";

	}

	public override void Give()
	{
		
		Player.Current.TripleJump = true;

		base.Give ();

	}

}