﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorSwitch : Door {

	[Header("Door Switches")]
	public bool andGate = false;
	public Switch[] switches;

	int andGateCounter = 0;
	Switch lastSwitch;

	[Header("_DEBUG_")]
	float timeUntilNextSwitchCheck;
	float timeUntilSwitchesReset;

	bool addToReset = false;

	protected override void Update () {

		DoCircleCheck (willDoorStayOpen);

		DoSwitchCheck ();

	}

	void DoSwitchCheck() {

		if (switches != null && Time.time > timeUntilNextSwitchCheck) {

			if (Time.time > timeUntilSwitchesReset) {

				andGateCounter = 0;

			}

			foreach (Switch currentSwitch in switches) {

				if (andGate) {

					if (currentSwitch.switchState == SwitchState.ON && lastSwitch != currentSwitch) {

						lastSwitch = currentSwitch;
						andGateCounter++;

						addToReset = true;

					}

					if (addToReset) {

						timeUntilSwitchesReset = Time.time + currentSwitch.GetComponent<SwitchResetInSeconds> ().resetInSeconds;

						addToReset = false;

					}

					if (andGateCounter == switches.Length) {

						anim.SetBool ("open", true);
						lastSwitch = null;
						andGateCounter = 0;
						timeToClose = Time.time + doorOpenLength;

						return;

					}

				} else {

					if (currentSwitch.switchState == SwitchState.ON) {

						anim.SetBool ("open", true);
						timeToClose = Time.time + doorOpenLength;

						return;

					}

				}

			}

			timeUntilNextSwitchCheck = Time.time + Constants.Tick;

		}

	}

}