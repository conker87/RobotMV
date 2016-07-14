using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	protected string itemName;
	public string ItemName { get; set; }

	protected virtual void Start() {

	}

	public virtual bool GiveItem() {

		Destroy(gameObject);

		return true;

	}

}
