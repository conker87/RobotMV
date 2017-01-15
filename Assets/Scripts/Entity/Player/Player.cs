using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : Entity
{
	GUIStyle style;
	public static string ErrorMessage = "";

	// TODO: This class is the area where the abilities for the Player is stored. It is then saved to the save file via IO.
		// Get/Set or direct changing? Meh.

	public static Player Current { get; protected set; }

	float timeToNextBomb, timeToNextMegaBomb;

	public InputManager inputManager;

	[Header("Jumping")]
	public bool 	Jump = false;
	public bool 	DoubleJump = false, TripleJump = false;

	[Header("Items")]
	[Header("Weapons")]
	public bool 	BasicBlaster = false;
	public bool 	BasicBlasterChargeShot = false;
	public bool 	Spinner = false;
	public bool 	ClusterSpreader = false;
	public bool 	MissileLauncher = false;
	public bool 	Laser = false;
	//	public bool DNU_Grenade = false;

	[Header("Shields")]
	public bool 	INFINITE_SHURIKENSHIELD = false;
	public bool 	ShurikenShield = false;
	public bool 	INFINITE_ENERGYSHIELD = false;
	public bool 	EnergyShield = false;

	[Header("Bombs")]
	public bool 	INFINITE_BOMBS = false;
	public int		Bombs = 0, BombsMaximum = 0;
	public float	BombsRegenCooldown = 1f;
	public bool		doBombsRegen = true;
	public bool 	INFINITE_MEGABOMBS = false;
	public int 		MegaBombs = 0, MegaBombsMaximum = 0;
	public float	BombsMegaRegenCooldown = 60f;
	public bool		doBombsMegaRegen = true;

	[Header("Tools")]
	public bool Magnet = false;

	[Header("Currently Equipped Items")]
	public Item CurrentItem = null;
	public Weapon CurrentWeapon = null;

	public bool CanChangeWeapon = true;

	void Start() {

		Current = this;
		inputManager = GameObject.FindObjectOfType<InputManager>();

	}

	public override void Update() {

		base.Update();

		if (BombsMaximum > 0) {
			DoBombsRegen ();
			DoBombsClamp ();
		}

		if (MegaBombsMaximum > 0) {
			DoMegaBombsRegen ();
			DoMegaBombsClamp ();
		}

	}
		
	void DoBombsClamp() {
		
		Bombs = Mathf.Clamp (Bombs, 0, BombsMaximum);

	}

	void DoBombsRegen() {

		if (!doBombsRegen || Bombs == BombsMaximum) {

			return;

		}

		if (doBombsRegen) {

			timeToNextBomb = Time.time + 1f;

		}

		if (Time.time > timeToNextBomb) {

			timeToNextBomb = Time.time + BombsRegenCooldown;
			Bombs++;

		}

	}
		
	void DoMegaBombsClamp() {
		
		MegaBombs = Mathf.Clamp (MegaBombs, 0, MegaBombsMaximum);

	}

	void DoMegaBombsRegen() {

		if (!doBombsMegaRegen || MegaBombs == MegaBombsMaximum) {

			return;

		}

		if (doBombsMegaRegen) {

			timeToNextMegaBomb = Time.time + BombsMegaRegenCooldown;

		}

		if (Time.time > timeToNextMegaBomb) {

			timeToNextMegaBomb = Time.time + BombsMegaRegenCooldown;
			MegaBombs++;

		}

	}

	public override void DamageHealth(int damage) {

		base.DamageHealth (damage);

	}
		
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

		GUI.Label(new Rect(10, 10, 500, 20), ErrorMessage, style);
		GUI.Label(new Rect(10, 30, 500, 20), "H: " + Health + "/" + HealthMaximum + "|" + HealthRegenOn + ")", style);
		GUI.Label(new Rect(10, 50, 500, 20), "Jumps: " + Jump + "|" + DoubleJump + "|" + TripleJump, style);
		GUI.Label(new Rect(10, 70, 500, 20), "Weaps: " + BasicBlaster + "|" + MissileLauncher + "|" + Laser, style);
		GUI.Label(new Rect(10, 90, 500, 20), "CW/I: " + (CurrentWeapon == null ? "None" : CurrentWeapon.UsableNameLocalisationID) + "|" + (CurrentItem == null ? "None" : CurrentItem.UsableNameLocalisationID), style);
		GUI.Label(new Rect(10, 110, 500, 20), "Speed: " + MoveSpeed, style);
		GUI.Label(new Rect(10, 130, 500, 20), "Bombs/Max: " + Bombs + "/" + BombsMaximum + "|" + MegaBombs + "/" + MegaBombsMaximum, style);
	}
}