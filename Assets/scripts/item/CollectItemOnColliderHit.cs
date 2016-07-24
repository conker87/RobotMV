using UnityEngine;
using System.Collections;

public class CollectItemOnColliderHit : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll) {

		if (coll.gameObject.tag == "Item")
		{
			Item i = coll.gameObject.GetComponent<Item> ();

			i.GiveItem ();
			Player.ErrorMessage = "You have collected: " + i.ItemName;

		}

	}

}
