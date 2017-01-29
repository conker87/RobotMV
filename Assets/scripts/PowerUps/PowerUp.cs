using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class PowerUp : MonoBehaviour {

	[Header("Power Up")]
	public string PowerUpNameLocalisationID = "LocalisationID <FIXME>";
	public string PowerUpDescriptionLocalisationID = "LocalisationID <FIXME>";

	public virtual void Give() {

		Destroy(gameObject);

	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Player") {
			
			Give ();
			Player.ErrorMessage = "You have collected: " + PowerUpNameLocalisationID;

		}

	}

}