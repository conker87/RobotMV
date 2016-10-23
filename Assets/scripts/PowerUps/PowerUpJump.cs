using UnityEngine;
using System.Collections;

public class PowerUpJump : PowerUp
{

	protected override void Start() {
		
		PowerUpName = "Jump";

	}

	public override void Give()
	{
		
		Player.Current.Jump = true;

		base.Give ();

	}

}