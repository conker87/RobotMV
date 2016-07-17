using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerAbilities : MonoBehaviour
{
	GUIStyle style;
	public static string ErrorMessage = "";

	// TODO: This class is the area where the abilities for the Player is stored. It is then saved to the save file via IO.
		// Get/Set or direct changing? Meh.
	// THIS MUST BE LOADED FROM THE SAVE FILE AT SaveFileLoad!!!!!!!

	public static PlayerAbilities Current { get; protected set; }

	// System
	float tick = .5f, nextTickTime;

	// Health & Energy
	public float Health = 100f,		HealthMaximum = 100f,		HealthRegenPerTick = 1f;
	public float Energy = 50f,		EnergyMaximum = 50f,		EnergyRegenPerTick = 5f;

	void HealthRegen()	{	Health += HealthRegenPerTick;						}
	void HealthClamp()	{	Health = Mathf.Clamp (Health, 0, HealthMaximum);	}

	void EnergyRegen()	{	Energy += EnergyRegenPerTick;						}
	void EnergyClamp()	{	Energy = Mathf.Clamp (Energy, 0, EnergyMaximum);	}

	public bool HealthRegenOn = false, EnergyRegenOn = true;

	// Movement
	public float MoveSpeed = 6, MaximumMoveSpeed = 6;

	// Jumping
	public bool Jump = false, DoubleJump = false, TripleJump = false;

	// Bombs
	public bool Bomb = false,	MegaBomb = false;
	public int	Bombs = 0,		MegaBombs = 0,		BombsMaximum = 0,		MegaBombsMaximum = 0;

	// Weapons
	public bool BasicBlaster = false, MissileLauncher = false;

	// Items
	public Item CurrentItem = null;

	// Weapon
	 public Weapon CurrentWeapon = null;

	void Start() {

		Current = this;

	}

	void Update() {

		if (Time.time > nextTickTime) {

			if (HealthRegenOn)	{	HealthRegen ();	}
			if (EnergyRegenOn)	{	EnergyRegen ();	}

			nextTickTime = Time.time + tick;

		}

		HealthClamp ();
		EnergyClamp ();

	}

	void OnGUI()
	{
		style = new GUIStyle(GUI.skin.label);
		style.normal.textColor = Color.red;

		GUI.Label(new Rect(10, 10, 500, 20), ErrorMessage, style);
		GUI.Label(new Rect(10, 30, 500, 20), "H: " + Health + "/" + HealthMaximum + " (" + HealthRegenOn + "), E: " + Energy + "/" + EnergyMaximum + "(" + EnergyRegenOn + ")", style);
		GUI.Label(new Rect(10, 50, 500, 20), "J/D/T: " + Jump + "/" + DoubleJump + "/" + TripleJump, style);
		GUI.Label(new Rect(10, 70, 500, 20), "BB/ML: " + BasicBlaster + "/" + MissileLauncher, style);
		GUI.Label(new Rect(10, 90, 500, 20), "Speed: " + MoveSpeed, style);
	}
}
