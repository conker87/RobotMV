using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class _DEBUG_GiveWeapon : MonoBehaviour {

	public List<Weapon> weapons = new List<Weapon>();

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
		if (Player.Current.CanChangeWeapon) {

			if ((Input.GetKey (KeyCode.Alpha1) && Player.Current.BasicBlaster) ||
			    (Input.GetKey (KeyCode.Alpha1) && Input.GetKey (KeyCode.LeftShift))) {

				Player.Current.CurrentWeapon = weapons [0];

			} else if ((Input.GetKey (KeyCode.Alpha2) && Player.Current.MissileLauncher) ||
			    (Input.GetKey (KeyCode.Alpha2) && Input.GetKey (KeyCode.LeftShift))) {

				Player.Current.CurrentWeapon = weapons [1];

			} else if ((Input.GetKey (KeyCode.Alpha3) && Player.Current.Laser) ||
			    (Input.GetKey (KeyCode.Alpha3) && Input.GetKey (KeyCode.LeftShift))) {
			
				Player.Current.CurrentWeapon = weapons [2];

			} else if ((Input.GetKey (KeyCode.Alpha4) && Player.Current.Spinner) ||
			    (Input.GetKey (KeyCode.Alpha4) && Input.GetKey (KeyCode.LeftShift))) {

				Player.Current.CurrentWeapon = weapons [3];

			} else if ((Input.GetKey (KeyCode.Alpha5) && Player.Current.ClusterSpreader) ||
			     (Input.GetKey (KeyCode.Alpha5) && Input.GetKey (KeyCode.LeftShift))) {

				Player.Current.CurrentWeapon = weapons [4];
			}


		}

	}

}
