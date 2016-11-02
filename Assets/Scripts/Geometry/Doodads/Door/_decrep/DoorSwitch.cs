using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorSwitch : Door {

	[Header("Door Switches")]
	public Switch[] switches;

	[Header("Switch Settings")]
	public bool andGate = false;
	int j = 0;
	Switch lastSwitch;

	[Header("_DEBUG_")]
	[SerializeField] float timeUntilNextCheck;
	[SerializeField] float timeUntilSwitchesReset;

	SwitchResetInSeconds sR;
	bool addToReset = false;

	protected override void Start() {

		disableCircleCheck = false;

	}

	//Perhaps add the ability to have an 'and' gate that requires the ON state of 2 or more switches?

	// Update is called once per frame
	public override void Update () {
		
		DoCircleCheck (true);

		UpdateEnd ();

		if (switches != null && Time.time > timeUntilNextCheck) {

			for (int i = 0; i < switches.Length; i++) {

				if (Time.time > timeUntilSwitchesReset) {

					j = 0;

				}

				if (switches [i] != null) {

					if (switches [i].switchState == SwitchState.ON && doorState == DoorState.CLOSED) {

						if (andGate) {

							if (lastSwitch != switches [i]) {

								lastSwitch = switches [i];
								j++;

								addToReset = true;

							}

							if ((sR = switches [i].GetComponent<SwitchResetInSeconds> ()) != null && addToReset) {

								timeUntilSwitchesReset = Time.time + sR.resetInSeconds;

								addToReset = false;

							}

							if (j > switches.Length - 1) {

								doorState = DoorState.OPEN_BEGIN;
								lastSwitch = null;
								j = 0;

								return;

							}

						} else {

							doorState = DoorState.OPEN_BEGIN;
							return;

						}

					}

					timeUntilNextCheck = Time.time + tick;

				}

			}

		}

	}

}
