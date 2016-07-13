using UnityEngine;
using System.Collections;

public class PlayerAbilitiesFromActors : MonoBehaviour
{
	public string prefabName ="CHANGEME";
	
	public int possessionLevel = 1;

	public float moveSpeed = 5f;
	public float jumpHeight = 3f, timeToJumpApex = 0.5f;

	public bool canJump = false, canDoubleJump = false, canTripleJump = false;
}
