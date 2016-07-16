using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class ThePlayer : MonoBehaviour
{
	const string PREFAB_LOC = "prefabs/prototype/prefabs/";
	public bool isCurrentlyPossessing = false;

	public Projectile projectile;
	public GameObject shootLocation;

	public GameObject CollectionRadius;
	
	GUIStyle style;

	LineRenderer lr;

	Controller2D controller;

	public float moveSpeed = 6, jumpHeight = 3.5f, timeToJumpApex = .4f, accelerationTimeAirbourne = .2f, accelerationTimeGrounded = .1f;
	float gravity, jumpVelocity;

	public int currentPossessionLevel = 0;

	public bool canJump = true, canDoubleJump = true, canTripleJump = true;
	bool hasJumped = false, hasDoubleJumped = false, hasTripleJumped = false;
	
	Vector3 velocity;
	float velocityXSmoothing, nextShotTime;

	public LayerMask raycastLayerMask;

	public static string ErrorMessage = "";

	public float attackSpeed = 1f;

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
		Vector2 input = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

		if (controller.collisions.above || controller.collisions.below) 
		{
			velocity.y = 0;
		}

		if (controller.collisions.below)
		{
			hasJumped = false;
			hasDoubleJumped = false;
			hasTripleJumped = false;
		}

		Jumping (input);

		Shoot ();

		PossessLine ();

		if (Input.GetButtonDown("Kill Possession") && isCurrentlyPossessing)
		{
			Debug.Log("GetButtonDown(\"Kill Possession\")");

			GameObject playerReplace = Instantiate(Resources.Load(PREFAB_LOC + "PlayerPossession", typeof(GameObject)),
			                                       gameObject.transform.position, Quaternion.identity) as GameObject;
			
			ThePlayer thePlayer = playerReplace.GetComponent<ThePlayer>();
			thePlayer.isCurrentlyPossessing = false;
			
			Destroy(gameObject);
		}

		ReloadLevelWithKey (KeyCode.P);

	}

	void OnGUI()
	{
		style = new GUIStyle(GUI.skin.label);
		style.normal.textColor = Color.red;

		GUI.Label(new Rect(10, 10, 500, 20), ErrorMessage, style);
		GUI.Label(new Rect(10, 30, 500, 20), "Jump: " + PlayerAbilities.Current.Jump, style);
		GUI.Label(new Rect(10, 50, 500, 20), "DoubleJump: " + PlayerAbilities.Current.DoubleJump, style);
		GUI.Label(new Rect(10, 70, 500, 20), "TripleJump: " + PlayerAbilities.Current.TripleJump, style);
		GUI.Label(new Rect(10, 90, 500, 20), "moveSpeed: " + moveSpeed, style);
		GUI.Label(new Rect(10, 110, 500, 20), "PossessionLevel: " + PlayerAbilities.Current.PossessionLevel, style);
		GUI.Label(new Rect(10, 130, 500, 20), "MaxPossessionDistance: " + PlayerAbilities.Current.PossessionMaximumDistance, style);
		GUI.Label(new Rect(10, 150, 500, 20), "PossessionCastingSpeed (sec): " + PlayerAbilities.Current.PossessSpeed, style);
		GUI.Label(new Rect(10, 170, 500, 20), "PossessionTimer: " + PlayerAbilities.Current.timer.ToString("0.0#"), style);
		if (PlayerAbilities.Current.previousCircleCollider != null)
		{
			GUI.Label(new Rect(10, 970, 500, 20), "PreviousCircleCollider: " + PlayerAbilities.Current.previousCircleCollider.name, style);
		}
	}

	void ReloadLevelWithKey(KeyCode key) {

		if (Input.GetKeyUp(KeyCode.P))
		{
			Application.LoadLevel(Application.loadedLevel);
		}

	}

	void PossessLine() {

		if (Input.GetMouseButton(1))
		{
			
			lr.enabled = true;
			Vector2 mousePositionToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 direction = mousePositionToWorld - (Vector2) gameObject.transform.position;

			Vector2 newPosition = (Vector2) gameObject.transform.position + Vector2.ClampMagnitude(direction, PlayerAbilities.Current.PossessionMaximumDistance);

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

	}

	void Shoot() {

		if (PlayerAbilities.Current.CurrentWeapon != null) {

			Vector2 mousePositionToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 direction = mousePositionToWorld - (Vector2) gameObject.transform.position;

			if (Input.GetMouseButton (0)) {

				Debug.Log ("CurrentWeapon != null");

				PlayerAbilities.Current.CurrentWeapon.Shoot (shootLocation, direction);

			}

		}

	}

	void Jumping(Vector2 input) {

		if (Input.GetButtonDown("Jump") &&
			(PlayerAbilities.Current.TripleJump&& !hasTripleJumped && hasDoubleJumped) &&
			(hasJumped || !controller.collisions.below) )
		{
			
			velocity.y = jumpVelocity;

			hasTripleJumped = true;

		}

		if (Input.GetButtonDown ("Jump") &&
			(PlayerAbilities.Current.DoubleJump && !hasDoubleJumped) &&
			(hasJumped || !controller.collisions.below)) {
			
			velocity.y = jumpVelocity;

			hasDoubleJumped = true;

		}

		if (Input.GetButton("Jump") &&
			PlayerAbilities.Current.Jump &&
			controller.collisions.below)
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
}
