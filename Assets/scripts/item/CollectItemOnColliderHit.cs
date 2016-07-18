using UnityEngine;
using System.Collections;

public class CollectItemOnColliderHit : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll) {

		if (coll.gameObject.tag == "Item")
		{
			Item colliderItem = coll.gameObject.GetComponent<Item> ();

			colliderItem.GiveItem ();
			Player.ErrorMessage = "You have collected: " + colliderItem.ItemName;
			return;

		}

	}

}
