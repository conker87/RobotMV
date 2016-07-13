using UnityEngine;
using System.Collections;

public class PlayerAbilities : MonoBehaviour
{
	// TODO: This class is the area where the abilities for the Player is stored. It is then saved to the save file via IO.
		// Get/Set or direct changing? Meh.
	// THIS MUST BE LOADED FROM THE SAVE FILE AT SaveFileLoad!!!!!!!

	// Usable abilities
		// Jumping
		public static bool Jump = false, DoubleJump = false, TripleJump = false;
		// Hookshot
		public static bool EctoplasmPull = false;

	// Possession
	public static float PossessSpeed = 5f;
	public static int PossessionLevel = 0, PossessionMaximumDistance = 1;

	// Spells (items)
	public static int currentUsingSpell = 0;
	public static string currentUsingSpellString = "nothing_null";



	
	// -- //
	public static Collider2D previousCircleCollider;
	public static float timer = 0.0f;

	public static void LoadFromFile()
	{
		// TODO: this.script
	}
}
