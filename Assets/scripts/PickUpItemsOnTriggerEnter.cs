using UnityEngine;
using System.Collections;

public class PickUpItemsOnTriggerEnter : MonoBehaviour
{
	PlayerController instance;

	void Update()
	{
//		Collider2D col = Physics2D.OverlapCircle(transform.position, 1.0f);
//		if (col != null && col.tag != "Untagged" && col.tag != "Player")
//		{
//			Debug.Log(col.tag);
//		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log(collision.gameObject.tag);
	}
}
