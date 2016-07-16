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
	
		if (Input.GetKey (KeyCode.Alpha1)) {

			if (PlayerAbilities.Current.BasicBlaster) {
			
				PlayerAbilities.Current.CurrentWeapon = weapons [0];

			}

		} else if (Input.GetKey (KeyCode.Alpha2)) {

			if (PlayerAbilities.Current.MissileLauncher) {

				PlayerAbilities.Current.CurrentWeapon = weapons [1];

			}

		}

	}

}
