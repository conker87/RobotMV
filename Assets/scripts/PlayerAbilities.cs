using UnityEngine;
using System.Collections;

public class PlayerAbilities : MonoBehaviour
{
	// TODO: This class is the area where the abilities for the Player is stored. It is then saved to the save file via IO.
		// Get/Set or direct changing? Meh.
	// THIS MUST BE LOADED FROM THE SAVE FILE AT SaveFileLoad!!!!!!!

	public static PlayerAbilities Current { get; protected set; }

	// Usable abilities
		// Jumping
		public bool Jump = false, DoubleJump = false, TripleJump = false;
		// Hookshot
		public bool EctoplasmPull = false;

	// Possession
	public float PossessSpeed = 5f;
	public int PossessionLevel = 0, PossessionMaximumDistance = 1;

	// Spells (items)
	public Item currentItem = null;
	
	// -- //
	public Collider2D previousCircleCollider;
	public float timer = 0.0f;

	public static void LoadFromFile()
	{
		// TODO: this.script
	}

	void Start() {

		Current = this;

	}
}
