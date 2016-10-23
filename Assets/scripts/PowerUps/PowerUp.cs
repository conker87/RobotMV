using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class PowerUp : MonoBehaviour {

	protected string powerUpName;
	public string PowerUpName { get { return powerUpName; } set { powerUpName = value; } }

	protected virtual void Start() {

	}

	public virtual void Give() {

		Destroy(gameObject);

	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Player")
		{
			
			Give ();
			Player.ErrorMessage = "You have collected: " + PowerUpName;

		}

	}

}
