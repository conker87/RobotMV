using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GiveWeapon : MonoBehaviour {

	public List<Weapon> weapons = new List<Weapon>();
	public List<Item> items = new List<Item>();

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		DoItemsDebug ();
		DoWeaponsDebug ();

	}

	void DoItemsDebug() {

		if ((Input.GetKey (KeyCode.Equals) && Player.Current.BombsD["BOMBS_MAX"] > 0) ||
			(Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.Equals))) {

			Player.Current.CurrentItem = items [0];

		} else if ((Input.GetKey (KeyCode.Minus) && Player.Current.BombsD["BOMBS_MEGA_MAX"] > 0) ||
			(Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.Minus))) {

			Player.Current.CurrentItem = items [1];

		} else if ((Input.GetKey (KeyCode.Alpha0) && Player.Current.CollectablesD["SHURIKEN_SHIELD"]) ||
			(Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.Alpha0))) {

			Player.Current.CurrentItem = items [2];

		} else if ((Input.GetKey (KeyCode.Alpha9) && Player.Current.CollectablesD["ENERGY_SHIELD"])  ||
			(Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.Alpha9))) {

			Player.Current.CurrentItem = items [3];

		}

	}

	void DoWeaponsDebug() {

		if (Player.Current.CanChangeWeapon) {

			if ((Input.GetKey (KeyCode.Alpha1) && Player.Current.CollectablesD["ENERGY_SHIELD"]) ||
				(Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.Alpha1))) {

				Player.Current.CurrentWeapon = weapons [0];

			} else if ((Input.GetKey (KeyCode.Alpha2) && Player.Current.CollectablesD["MISSILE_LAUNCHER"]) ||
				(Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.Alpha2))) {

				Player.Current.CurrentWeapon = weapons [1];

			} else if ((Input.GetKey (KeyCode.Alpha3) && Player.Current.CollectablesD["LASER"]) ||
				(Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.Alpha3))) {

				Player.Current.CurrentWeapon = weapons [2];

			} else if ((Input.GetKey (KeyCode.Alpha4) && Player.Current.CollectablesD["SPINNER"]) ||
				(Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.Alpha4))) {

				Player.Current.CurrentWeapon = weapons [3];

			} else if ((Input.GetKey (KeyCode.Alpha5) && Player.Current.CollectablesD["CLUSTER_SPREADER"]) ||
				(Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.Alpha5))) {

				Player.Current.CurrentWeapon = weapons [4];
			}

		}

	}

}
