using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorSwitch : Door {

	public Switch[] switches;

	float timeInbetweenNextCheck = 0.5f, timeUntilNextCheck;

	protected override void Start() {

		disableCircleCheck = false;

	}

	// Update is called once per frame
	public override void Update () {
		
		DoCircleCheck (true);

		UpdateEnd ();

		if (switches != null) {

			for (int i = 0; i < switches.Length; i++) {

				if (switches[i].switchState == SwitchState.ON && doorState == DoorState.CLOSED) {

					doorState = DoorState.OPEN_BEGIN;
	
					return;

				}

				timeUntilNextCheck = Time.time + timeInbetweenNextCheck;

			}

		}

	}

}
