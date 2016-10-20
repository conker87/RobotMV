using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class Item : MonoBehaviour {

	protected string itemName;
	public string ItemName { get { return itemName; } set { itemName = value; } }

	protected virtual void Start() {

	}

	public virtual void GiveItem() {

		Destroy(gameObject);

	}

}
