﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class ThePlayer : MonoBehaviour
{
	const string PREFAB_LOC = "prefabs/prototype/prefabs/";
	public bool isCurrentlyPossessing = false;
	
	GUIStyle style;

	LineRenderer lr;

	//Text GUIError;

	Controller2D controller;

	public float moveSpeed = 6;

	public float jumpHeight = 3.5f, timeToJumpApex = .4f;
	float gravity, jumpVelocity;
	public float accelerationTimeAirbourne = .2f, accelerationTimeGrounded = .1f;

	public int currentPossessionLevel = 0;

	// Triple jump REQUIRES you to be able to double jump, obviously.
	public bool canJump = true, canDoubleJump = true, canTripleJump = true;
	[SerializeField]
	bool hasJumped = false, hasDoubleJumped = false, hasTripleJumped = false;
	
	Vector3 velocity;
	float velocityXSmoothing;

	public LayerMask raycastLayerMask;

	public static string ErrorMessage = "";

	void Awake()
	{

	}

	void Start ()
	{
		controller = GetComponent<Controller2D>();

		gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

		lr = GameObject.Find ("$GameMaster").GetComponent<LineRenderer>();
		lr.enabled = false;

		Debug.Log ("gravity: " + gravity + ", jumpVelocity: " + jumpVelocity);
	}

	void Update ()
	{
		if (Input.GetKeyUp(KeyCode.P))
		{
			Application.LoadLevel(Application.loadedLevel);
		}

		if (controller.collisions.above || controller.collisions.below) 
		{
			velocity.y = 0;
		}

		Vector2 input = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

		if (controller.collisions.below)
		{
			hasJumped = false;
			hasDoubleJumped = false;
			hasTripleJumped = false;
		}

		if (Input.GetMouseButton(0))
		{
			lr.enabled = true;
			Vector2 mousePositionToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 direction = mousePositionToWorld - (Vector2) gameObject.transform.position;

			Vector2 newPosition = (Vector2) gameObject.transform.position + Vector2.ClampMagnitude(direction, PlayerAbilities.PossessionMaximumDistance);

			GameObject _HITPOINT = Instantiate(Resources.Load(PREFAB_LOC + "_HITPOINT", typeof(GameObject)), newPosition, Quaternion.identity) as GameObject;

			lr.useWorldSpace = true;
			lr.SetWidth(0.2f, 0.2f);
			lr.SetPosition(0, transform.position);
			lr.SetPosition(1, _HITPOINT.transform.position);
		}
		else
		{
			lr.enabled = false;
		}

		if (Input.GetButtonDown("Kill Possession") && isCurrentlyPossessing)
		{
			Debug.Log("GetButtonDown(\"Kill Possession\")");

			GameObject playerReplace = Instantiate(Resources.Load(PREFAB_LOC + "PlayerPossession", typeof(GameObject)),
			                                       gameObject.transform.position, Quaternion.identity) as GameObject;
			
			ThePlayer thePlayer = playerReplace.GetComponent<ThePlayer>();
			thePlayer.isCurrentlyPossessing = false;
			
			Destroy(gameObject);
		}

		// Jumping
		if (Input.GetButtonDown("Jump") && canTripleJump && (PlayerAbilities.TripleJump && !hasTripleJumped) &&
		    								(PlayerAbilities.DoubleJump && !hasDoubleJumped) &&
		    									hasJumped )
		{
			velocity.y = jumpVelocity;

			hasTripleJumped = true;
		}
		else if (Input.GetButtonDown("Jump") && canDoubleJump && (PlayerAbilities.DoubleJump && !hasDoubleJumped) &&
		           									hasJumped )
		{
			velocity.y = jumpVelocity;

			hasDoubleJumped = true;
		}
		else if (Input.GetButtonDown("Jump") && PlayerAbilities.Jump && controller.collisions.below)
		{
			velocity.y = jumpVelocity;

			hasJumped = true;
		}

		float targetVelocityX = input.x * moveSpeed;

		velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing,
		                              (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirbourne);
		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime);
	}

	void OnGUI()
	{
		style = new GUIStyle(GUI.skin.label);
		style.normal.textColor = Color.red;

		GUI.Label(new Rect(10, 10, 500, 20), ErrorMessage, style);
		GUI.Label(new Rect(10, 30, 500, 20), "Jump: " + PlayerAbilities.Jump, style);
		GUI.Label(new Rect(10, 50, 500, 20), "DoubleJump: " + PlayerAbilities.DoubleJump, style);
		GUI.Label(new Rect(10, 70, 500, 20), "TripleJump: " + PlayerAbilities.TripleJump, style);
		GUI.Label(new Rect(10, 90, 500, 20), "moveSpeed: " + moveSpeed, style);
		GUI.Label(new Rect(10, 110, 500, 20), "PossessionLevel: " + PlayerAbilities.PossessionLevel, style);
		GUI.Label(new Rect(10, 130, 500, 20), "MaxPossessionDistance: " + PlayerAbilities.PossessionMaximumDistance, style);
		GUI.Label(new Rect(10, 150, 500, 20), "PossessionCastingSpeed (sec): " + PlayerAbilities.PossessSpeed, style);
		GUI.Label(new Rect(10, 170, 500, 20), "PossessionTimer: " + PlayerAbilities.timer.ToString("0.0#"), style);
		if (PlayerAbilities.previousCircleCollider != null)
		{
			GUI.Label(new Rect(10, 970, 500, 20), "PreviousCircleCollider: " + PlayerAbilities.previousCircleCollider.name, style);
		}
	}
}