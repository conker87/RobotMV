using UnityEngine;
using System.Collections;

public class CollectItemOnColliderHit : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Use Item")
		{
			PowerUp i = other.gameObject.GetComponent<PowerUp> ();

			i.Give ();
			Player.ErrorMessage = "You have collected: " + i.PowerUpID;

		}

	}

}
