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

	[Header("Jumping")]
	public bool Jump = false;
	public bool DoubleJump = false, TripleJump = false;

	[Header("Items")]
	[Header("Weapons")]
	public bool BasicBlaster = false;
	public bool BasicBlasterChargeShot = false;
	public bool MissileLauncher = false, Grenade = false;
	public bool Laser = false;
	[Header("Bombs")]
	public bool Bomb = false;
	public bool MegaBomb = false;
	[Header("Bombs Count")]
	public int	Bombs = 0;
	public int MegaBombs = 0,		BombsMaximum = 0,		MegaBombsMaximum = 0;

	[Header("Currently Equipped Items")]
	public Item CurrentItem = null;
	public Weapon CurrentWeapon = null;

	public bool CanChangeWeapon = true;

	void Start() {

		Current = this;

	}

	public override void DamageHealth(float damage) {

		if (hasInvincibilityFrames && !isCurrentlyInInvulnerabilityFrames) {

			base.DamageHealth (damage);
			isCurrentlyInInvulnerabilityFrames = true;

			iFramesRemoveTime = Time.time + invincibilityFramesLength;
		}

	}

	//void OnCollisionEnter(Collision2D col) {
	void OnTriggerStay2D(Collider2D col) {

		Enemy e;

		if ((e = col.gameObject.GetComponentInParent<Enemy> ()) != null) {
			
			DamageHealth (e.DamageOnTouch);

		}

	}

	void OnGUI()
	{
		style = new GUIStyle(GUI.skin.label);
		style.normal.textColor = Color.magenta;

		GUI.Label(new Rect(10, 90, 500, 20), ErrorMessage, style);
		GUI.Label(new Rect(10, 110, 500, 20), "H: " + Health + "/" + HealthMaximum + " (" + HealthRegenOn + "), E: " + Energy + "/" + EnergyMaximum + "(" + EnergyRegenOn + ")", style);
		GUI.Label(new Rect(10, 130, 500, 20), "Jumps: " + Jump + "/" + DoubleJump + "/" + TripleJump, style);
		GUI.Label(new Rect(10, 150, 500, 20), "Weaps: " + BasicBlaster + "/" + MissileLauncher + "/" + Laser, style);
		GUI.Label(new Rect(10, 170, 500, 20), "ATM: " + (CurrentWeapon == null ? "None" : CurrentWeapon.WeaponName), style);
		GUI.Label(new Rect(10, 190, 500, 20), "Speed: " + MoveSpeed, style);
	}
}
