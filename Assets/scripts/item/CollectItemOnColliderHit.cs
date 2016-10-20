using UnityEngine;
using System.Collections;

public class CollectItemOnColliderHit : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Item")
		{
			Item i = other.gameObject.GetComponent<Item> ();

			i.GiveItem ();
			Player.ErrorMessage = "You have collected: " + i.ItemName;

		}

	}

}
