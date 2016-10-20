using UnityEngine;
using System.Collections;

public class CollectPowerUpOnColliderHit : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "PowerUp")
		{
			PowerUp p = other.gameObject.GetComponent<PowerUp> ();

			p.GivePowerUp ();

		}

	}

}
