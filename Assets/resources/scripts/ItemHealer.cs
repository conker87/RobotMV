using UnityEngine;
using System.Collections;

public class ItemHealer : MonoBehaviour
{
	public static PlayerStats instance;
	const string objectHealerName = "PrototypeHealer_";

	public float amountToHeal = 10f;

	void Start()
	{
		if (instance == null)
		{
			instance 	= GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerStats>();
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
//		if (other.gameObject.tag == "Player") 
//		{
//			if (instance.HealPlayer(amountToHeal))
//			{
//				Debug.Log ("Player healed by " + gameObject.name + " for amount " + amountToHeal);
//
//				Destroy(gameObject);
//			}
//			else
//			{
//				// TODO: Add a message stating you're at full health.
//
//				Debug.Log ("Full health");
//			}
//		}
	}
}
