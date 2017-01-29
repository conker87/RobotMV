using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class PowerUp : MonoBehaviour {

	[Header("Power Up")]
	public string PowerUpNameLocalisationID = "LocalisationID <FIXME>";
	public string PowerUpDescriptionLocalisationID = "LocalisationID <FIXME>";

	[Header("Power Up Settings")]
	public string PowerUpID;
	public PowerUpDictionary PowerUpD;
	public int AddItem;
	public CollectionItemType IncreaseMultiplierOf;

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

		if (PowerUpD == PowerUpDictionary.COLLECTION_INFO) {

			if (!Player.Current.CollectablesInfoD.ContainsKey (PowerUpID)) {

				Debug.Log ("PowerUpID: " + PowerUpID + " does not exist.");
				return;

			}

			CollectablesInfo newCollectablesInfo = new CollectablesInfo (1f, 1f, 1f);
			float attackLengthMod = Player.Current.CollectablesInfoD [PowerUpID].AttackLengthMultiplier,
					cooldownMod = Player.Current.CollectablesInfoD [PowerUpID].CooldownMultiplier,
					damageMod = Player.Current.CollectablesInfoD [PowerUpID].DamageMultiplier;

			if (IncreaseMultiplierOf == CollectionItemType.ATTACK_LENGTH) {

				newCollectablesInfo = new CollectablesInfo (
					attackLengthMod += AddItem,
					cooldownMod,
					damageMod);

			}

			if (IncreaseMultiplierOf == CollectionItemType.COOLDOWN) {

				newCollectablesInfo = new CollectablesInfo (
					attackLengthMod,
					cooldownMod += AddItem,
					damageMod);

			}

			if (IncreaseMultiplierOf == CollectionItemType.DAMAGE) {
				
				newCollectablesInfo = new CollectablesInfo (
					attackLengthMod,
					cooldownMod,
					damageMod += AddItem);

			}

			Player.Current.CollectablesInfoD [PowerUpID] = newCollectablesInfo;

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

public enum PowerUpDictionary { BOMB, COLLECTABLE, VITAL, COLLECTION_INFO };
public enum CollectionItemType { ATTACK_LENGTH, COOLDOWN, DAMAGE };