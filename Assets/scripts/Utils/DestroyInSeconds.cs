using UnityEngine;
using System.Collections;

public class DestroyInSeconds : MonoBehaviour
{
	public float seconds;

	void Start ()
	{
		Invoke ("DestroyGameObject", seconds);
	}
	
	// Update is called once per frame
	void DestroyGameObject()
	{
		Destroy(gameObject);
	}
}
