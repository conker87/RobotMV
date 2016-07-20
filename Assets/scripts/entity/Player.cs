using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : Entity
{
	GUIStyle style;
	public static string ErrorMessage = "";

	// TODO: This class is the area where the abilities for the Player is stored. It is then saved to the save file via IO.
		// Get/Set or direct changing? Meh.
	// THIS MUST BE LOADED FROM THE SAVE FILE AT SaveFileLoad!!!!!!!

	public static Player Current { get; protected set; }

	// Jumping
	public bool Jump = false, DoubleJump = false, TripleJump = false;

	// Bombs
	public bool Bomb = false,	MegaBomb = false;
	public int	Bombs = 0,		MegaBombs = 0,		BombsMaximum = 0,		MegaBombsMaximum = 0;

	// Weapons
	public bool BasicBlaster = false, MissileLauncher = false, Laser = false;

	// Items
	public Item CurrentItem = null;

	// Weapon
	 public Weapon CurrentWeapon = null;

	void Start() {

		Current = this;

	}

	void OnGUI()
	{
		style = new GUIStyle(GUI.skin.label);
		style.normal.textColor = Color.red;

		GUI.Label(new Rect(10, 90, 500, 20), ErrorMessage, style);
		GUI.Label(new Rect(10, 110, 500, 20), "H: " + Health + "/" + HealthMaximum + " (" + HealthRegenOn + "), E: " + Energy + "/" + EnergyMaximum + "(" + EnergyRegenOn + ")", style);
		GUI.Label(new Rect(10, 130, 500, 20), "J/D/T: " + Jump + "/" + DoubleJump + "/" + TripleJump, style);
		GUI.Label(new Rect(10, 150, 500, 20), "BB/ML: " + BasicBlaster + "/" + MissileLauncher, style);
		GUI.Label(new Rect(10, 170, 500, 20), "Currently Equiped: " + (CurrentWeapon == null ? "None" : CurrentWeapon.WeaponName), style);
		GUI.Label(new Rect(10, 190, 500, 20), "Speed: " + MoveSpeed, style);
	}
}
