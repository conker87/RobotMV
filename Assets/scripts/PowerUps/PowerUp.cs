using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class PowerUp : MonoBehaviour {

	[Header("Power Up ID")]
	public string PowerUpID;

	[Header("Power Up To")]
	public PowerUpDictionary PowerUpD;
	public PowerUpGiveType PowerUpT;

	[Header("Power Up Count Value")]
	public int AddItem;

	[Header("Power Up Bool Value")]
	public bool GiveItem;

	public virtual void Give() {

		if (PowerUpD == PowerUpDictionary.BOMB) {

			if (!Player.Current.BombsD.ContainsKey (PowerUpID) || PowerUpT == PowerUpGiveType.BOOL) {

				Debug.Log ("PowerUpID: " + PowerUpID + " does not exist or is not a type " + PowerUpT.ToString() + " and cannot be set as one.");
				return;

			}

			Player.Current.BombsD [PowerUpID] = GiveItem;

		}

		if (PowerUpD == PowerUpDictionary.COLLECTABLE) {

			if (!Player.Current.CollectablesD.ContainsKey (PowerUpID) || PowerUpT == PowerUpGiveType.COUNT) {

				Debug.Log ("PowerUpID: " + PowerUpID + " does not exist or is not a type " + PowerUpT.ToString() + " and cannot be set as one.");
				return;

			}

			Player.Current.CollectablesD [PowerUpID] = GiveItem;

		}

		if (PowerUpD == PowerUpDictionary.VITAL) {

			if (!Player.Current.VitalsD.ContainsKey (PowerUpID) || PowerUpT == PowerUpGiveType.BOOL) {

				Debug.Log ("PowerUpID: " + PowerUpID + " does not exist or is not a type " + PowerUpT.ToString() + " and cannot be set as one.");
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
public enum PowerUpGiveType { COUNT, BOOL };