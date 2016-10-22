using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class ItemPickup : MonoBehaviour {

	protected string itemName;
	public string ItemName { get { return itemName; } set { itemName = value; } }

	protected virtual void Start() {

	}

	public virtual void GiveItem() {

		Destroy(gameObject);

	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Player")
		{
			
			GiveItem ();
			Player.ErrorMessage = "You have collected: " + ItemName;

		}

	}

}
