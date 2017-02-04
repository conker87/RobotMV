using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Refactored: 02/02/2017
public class SwitchDoor : DoorBase {

	[Header("Door Switches")]
	public bool andGate = false;
	public Switch[] switches;

	int andGateCounter = 0;
	Switch lastSwitch;

	[Header("")]
	float timeUntilNextSwitchCheck;

	protected override void Update () {

		base.Update ();

		DoSwitchCheck ();

	}

	void DoSwitchCheck() {

		if (switches == null || IsDoorOpen ()) {

			return;

		}

		SwitchResetInSeconds currentSwitchReset;

		if (Time.time > timeUntilNextSwitchCheck) {

			andGateCounter = 0;

			foreach (Switch s in switches) {

				if (andGate) {

					if (s.switchState == SwitchState.ON && lastSwitch != s) {

						lastSwitch = s;
						andGateCounter++;

					}

					if (andGateCounter == switches.Length) {

						OpenDoor ();

						lastSwitch = null;
						andGateCounter = 0;

						timeToClose = ((currentSwitchReset = s.GetComponent<SwitchResetInSeconds>()) != null) ? Time.time + currentSwitchReset.resetInSeconds : Time.time + DoorOpenLength;

						break;

					}

				} else {

					if (s.switchState == SwitchState.ON) {

						OpenDoor ();

						timeToClose = ((currentSwitchReset = s.GetComponent<SwitchResetInSeconds>()) != null) ? Time.time + currentSwitchReset.resetInSeconds : Time.time + DoorOpenLength;

						break;

					}

				}

			}

			timeUntilNextSwitchCheck = Time.time + Constants.DoorCheckingTick;

		}

	}

}
