using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	protected string powerUpName;
	public string PowerUpName { get { return powerUpName; } set { powerUpName = value; } }

	protected virtual void Start() {

	}

	public virtual void GivePowerUp() {

		Destroy(gameObject);

	}

}
