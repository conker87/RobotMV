using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class PowerUp : MonoBehaviour {

	[Header("Power Up")]
	public string PowerUpName;

	[Header("Power Up Settings")]
	public string PowerUpID;
	public PowerUpDictionary PowerUpD;
	public int AddItem;

	public virtual void Give() {

		if (PowerUpD == PowerUpDictionary.BOMB) {

			if (!Player.Current.BombsD.ContainsKey (PowerUpID)) {

				Debug.Log ("PowerUpID: " + PowerUpID + " does not exist.");
				return;

			}

			Player.Current.BombsD [PowerUpID] += AddItem;

		}

		if (PowerUpD == PowerUpDictionary.COLLECTABLE) {

			if (!Player.Current.CollectablesD.ContainsKey (PowerUpID)) {

				Debug.Log ("PowerUpID: " + PowerUpID + " does not exist.");
				return;

			}

			Player.Current.CollectablesD [PowerUpID] = true;

		}

		if (PowerUpD == PowerUpDictionary.VITAL) {

			if (!Player.Current.VitalsD.ContainsKey (PowerUpID)) {

				Debug.Log ("PowerUpID: " + PowerUpID + " does not exist.");
				return;

			}

			Player.Current.VitalsD [PowerUpID] += AddItem;

		}

		Destroy(gameObject);

	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Player") {
			
			Give ();
			Player.ErrorMessage = "You have collected: " + PowerUpID;

		}

	}

}

public enum PowerUpDictionary { BOMB, COLLECTABLE, VITAL };