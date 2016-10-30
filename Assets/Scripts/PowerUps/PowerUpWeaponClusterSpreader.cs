using UnityEngine;
using System.Collections;

public class PowerUpWeaponClusterSpreader : PowerUp
{

	protected override void Start() {

		PowerUpName = "Cluster Spreader";

	}

	public override void Give()
	{
		
		Player.Current.ClusterSpreader = true;

		base.Give ();

	}

}