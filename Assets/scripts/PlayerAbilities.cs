using UnityEngine;
using System.Collections;

public class PlayerAbilities : MonoBehaviour
{
	// TODO: This class is the area where the abilities for the Player is stored. It is then saved to the save file via IO.
		// Get/Set or direct changing? Meh.
	// THIS MUST BE LOADED FROM THE SAVE FILE AT SaveFileLoad!!!!!!!

	public static PlayerAbilities Current { get; protected set; }

	// Usable Abilities
	// Jumping
	public bool Jump = false, DoubleJump = false, TripleJump = false;

	// Bombs
	public bool Bomb = false, MegaBomb = false;
	public int CurrentBombs = 0, CurrentMegaBombs = 0, MaximumBombs = 0, MaximumMegaBombs = 0;

	// Weapons
	public bool BasicBlaster = false;

	// Items
	public Item CurrentItem = null;

	// Weapon
	 public Weapon CurrentWeapon = null;

	// Probably keep this in, but wont use it.
	// Possession
	public float PossessSpeed = 5f;
	public int PossessionLevel = 0, PossessionMaximumDistance = 1;
	
	// -- //
	public Collider2D previousCircleCollider;
	public float timer = 0.0f;

	void Start() {

		Current = this;

	}
}
