using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GiveWeapon : MonoBehaviour {

	public List<Weapon> weapons = new List<Weapon>();
	public List<Item> items = new List<Item>();

	int weaponsCurrent;

	// Use this for initialization
	void Start () {

		weaponsCurrent = 0;

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.anyKeyDown) {

			foreach (KeyCode code in Enum.GetValues (typeof(KeyCode))) {

				if (Input.GetKeyDown (code)) {

					int check = 0;

					if (code.ToString().Contains("Alpha") && int.TryParse(code.ToString ().Remove (0, 5), out check)) {

						weaponsCurrent = check - 1;

					}

				}

			}

		}

		DoItemsDebug ();
		DoWeaponsDebug ();

	}

	void DoItemsDebug() {

		if ((Input.GetKey (KeyCode.Equals) && Player.Current.Bombs_Current > 0) || (Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.Equals))) {

			Player.Current.CurrentItem = items [0];

		} else if ((Input.GetKey (KeyCode.Minus) && Player.Current.Bombs_Mega_Current > 0) || (Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.Minus))) {

			Player.Current.CurrentItem = items [1];

		} else if ((Input.GetKey (KeyCode.Alpha0) && Player.Current.Item_ShurikenShield) ||	(Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.Alpha0))) {

			Player.Current.CurrentItem = items [2];

		} else if ((Input.GetKey (KeyCode.Alpha9) && Player.Current.Item_EnergyShield) || (Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.Alpha9))) {

			Player.Current.CurrentItem = items [3];

		}

	}

	void DoWeaponsDebug() {

		if (!PauseManager.Current.checkIfCurrentlyPaused() && Player.Current.CanChangeWeapon) {

			if (Input.GetAxis ("Mouse ScrollWheel") > 0) {

				weaponsCurrent++;

			}
			if (Input.GetAxis ("Mouse ScrollWheel") < 0) {

				weaponsCurrent--;

			}

			if (weaponsCurrent > weapons.Count - 1) {

				weaponsCurrent = 0;

			}

			if (weaponsCurrent < 0) {

				weaponsCurrent = weapons.Count - 1;

			}

			// TODO: Make it so that if you do not have any of the other weapons, then auto increment and 
			//	overflows to 0.

			//if (Player.Current.CollectablesD.ContainsKey(weapons[weaponsCurrent].CollectableID) &&
			//	Player.Current.CollectablesD[weapons[weaponsCurrent].CollectableID].) {

				Player.Current.CurrentWeapon = weapons [weaponsCurrent];

			//}

		}

	}

}
