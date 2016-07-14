using UnityEngine;
using System.Collections;

public class HitPointCollisionDetection : MonoBehaviour
{
	const string PREFAB_LOC = "prefabs/prototype/prefabs/";
	public LayerMask collisionMask;
	string ErrorMessage;

	Collider2D circle;

	GameObject player;
	
	GUIStyle style;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		
		GameObject[] obj = GameObject.FindGameObjectsWithTag("_HITPOINT");
		
		if (obj.Length > 1)
		{
			Destroy(obj[0]);
		}
	}

	void Start()
	{
		circle = Physics2D.OverlapCircle(gameObject.transform.position, .1f, collisionMask);
		RaycastHit2D hit;

		if (circle != PlayerAbilities.Current.previousCircleCollider)
		{
			PlayerAbilities.Current.previousCircleCollider = circle;
			PlayerAbilities.Current.timer = PlayerAbilities.Current.PossessSpeed;
		}

		if (circle != null)
		{
			if (RaycastFromGameObject.RaycastToGameObject(player, gameObject, collisionMask, out hit, PlayerAbilities.Current.PossessionMaximumDistance, true))
			{
				GameObject hitCollider = hit.collider.gameObject;

				if (hitCollider.tag == "Item")
				{
					Item colliderItem = hitCollider.GetComponent<Item> ();

					colliderItem.GiveItem ();
					ThePlayer.ErrorMessage = "You have collected: " + colliderItem.ItemName;
					return;

				}
				else if (hit.collider.gameObject.tag == "Actor")
				{
					PlayerAbilitiesFromActors possessionAbilities = hitCollider.GetComponent<PlayerAbilitiesFromActors>();
					
					if (PlayerAbilities.Current.PossessionLevel >= possessionAbilities.possessionLevel)
					{
						if (circle == PlayerAbilities.Current.previousCircleCollider && PlayerAbilities.Current.timer >= 0)
						{
							PlayerAbilities.Current.timer -= Time.deltaTime;
						}
					
						if (PlayerAbilities.Current.timer < 0)
						{
							GameObject playerReplace = Instantiate(Resources.Load(PREFAB_LOC + possessionAbilities.prefabName + "Possession", typeof(GameObject)),
							                                       hit.collider.transform.position, Quaternion.identity) as GameObject;
							
							Vector3 previousScale = hit.collider.transform.localScale;

							ThePlayer.ErrorMessage = "You have possessed " + hit.collider.gameObject;

							playerReplace.transform.localScale = previousScale;
							
							ThePlayer thePlayer = playerReplace.GetComponent<ThePlayer>();
							
							thePlayer.moveSpeed = possessionAbilities.moveSpeed;
							thePlayer.canDoubleJump = possessionAbilities.canDoubleJump;
							thePlayer.canTripleJump = possessionAbilities.canTripleJump;
							thePlayer.jumpHeight = possessionAbilities.jumpHeight;
							thePlayer.timeToJumpApex = possessionAbilities.timeToJumpApex;
							thePlayer.isCurrentlyPossessing = true;

							Destroy(hit.collider.gameObject);
							Destroy(player);
							Destroy(gameObject);

							PlayerAbilities.Current.timer = 0;
							PlayerAbilities.Current.previousCircleCollider = null;
						}
					}
					else
					{
						ThePlayer.ErrorMessage = "Your possession level is too low.";

						Debug.Log ("Possession level too low (" + PlayerAbilities.Current.PossessionLevel + ", " + possessionAbilities.possessionLevel + ").");
					}
				}
			}
			else
			{
				ThePlayer.ErrorMessage = "Object unable to be possessed.";
			}
		}
	}
}