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

	public Vector2 position;

	[Header("Jumping")]
	public bool Jump = false;
	public bool DoubleJump = false, TripleJump = false;

	[Header("Items")]
	[Header("Weapons")]
	public bool BasicBlaster = false;
	public bool BasicBlasterChargeShot = false;
	public bool MissileLauncher = false;
	public bool Laser = false;
	public bool DNU_Grenade = false;

	[Header("Bombs")]
	public int	Bombs = 0;
	public int 	MegaBombs = 0,		BombsMaximum = 0,		MegaBombsMaximum = 0;
	public float BombsRegen = 3f,	BombsMegaRegen = 60f;

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

		position = transform.position;

	}
		
	void DoBombsClamp() {
		
		Bombs = Mathf.Clamp (Bombs, 0, BombsMaximum);

	}

	void DoBombsRegen() {

		if (Bombs >= BombsMaximum) {

			Bombs = BombsMaximum;
			timeToNextBomb = Time.time + 3f;

			return;

		}

		if (Time.time > timeToNextBomb) {

			Bombs++;

			timeToNextBomb = Time.time + BombsRegen;

		}

	}
		
	void DoMegaBombsClamp() {
		
		MegaBombs = Mathf.Clamp (MegaBombs, 0, MegaBombsMaximum);

	}

	void DoMegaBombsRegen() {

		if (MegaBombs >= MegaBombsMaximum) {

			MegaBombs = MegaBombsMaximum;
			return;

		}

		if (Time.time > timeToNextMegaBomb) {

			MegaBombs++;

			timeToNextMegaBomb = Time.time + BombsMegaRegen;

		}

	}

	public override void DamageHealth(float damage) {

		base.DamageHealth (damage);

	}
		
	void OnTriggerStay2D(Collider2D col) {

		Debug.Log (col.gameObject.tag);

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
		GUI.Label(new Rect(10, 110, 500, 20), "H/E: " + Health + "/" + HealthMaximum + " (" + HealthRegenOn + ")|" + Energy + "/" + EnergyMaximum + "(" + EnergyRegenOn + ")", style);
		GUI.Label(new Rect(10, 130, 500, 20), "Jumps: " + Jump + "|" + DoubleJump + "|" + TripleJump, style);
		GUI.Label(new Rect(10, 150, 500, 20), "Weaps: " + BasicBlaster + "|" + MissileLauncher + "|" + Laser, style);
		GUI.Label(new Rect(10, 170, 500, 20), "CW/I: " + (CurrentWeapon == null ? "None" : CurrentWeapon.UsableName) + "|" + (CurrentItem == null ? "None" : CurrentItem.UsableName), style);
		GUI.Label(new Rect(10, 190, 500, 20), "Speed: " + MoveSpeed, style);
		GUI.Label(new Rect(10, 210, 500, 20), "Bombs/Max: " + Bombs + "/" + BombsMaximum + "|" + MegaBombs + "/" + MegaBombsMaximum, style);
	}
}
