using UnityEngine;
using System.Collections;

// Created: 11/02/2017
public class KeyDoor : DoorBase {

	[Range(1, 20)]
	public int keysRequired = 1;

	protected override void Start () {

		base.Start ();

		// This kind of door will always stay open, there is not an infinite number of keys on the field.
		WillDoorStayOpen = true;

	}

	protected override void CheckForPlayerInSurrounding(bool doCheck = true) {

		if (!doCheck) {

			return;

		}

		if (Time.time > timeToNextCheck) {

			playerOverlapCircle = Physics2D.OverlapCircle (transform.position, DoorCheckDistance, PlayerLayerMask);

			if (playerOverlapCircle != null && Player.Current.Keys >= keysRequired) {

				OpenDoor ();

				Player.Current.Keys -= keysRequired;

			}

		}

	}

}
