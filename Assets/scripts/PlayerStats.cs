using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
//	#region Singleton
//	private static PlayerStats _instance;
//	
//	public static PlayerStats instance
//	{
//		get
//		{
//			if(_instance == null)
//			{
//				_instance = GameObject.FindObjectOfType<PlayerStats>();
//				
//				//Tell unity not to destroy this object when loading a new scene!
//				DontDestroyOnLoad(_instance.gameObject);
//			}
//			
//			return _instance;
//		}
//	}
//	
//	void Awake() 
//	{
//		if(_instance == null)
//		{
//			//If I am the first instance, make me the Singleton
//			_instance = this;
//			DontDestroyOnLoad(this);
//		}
//		else
//		{
//			//If a Singleton already exists and you find
//			//another reference in scene, destroy it!
//			if(this != _instance)
//				Destroy(this.gameObject);
//		}
//	}
//	#endregion
//
//	// Components
//	public Text healthBarText;
//	public Text healthOrbsText;
//	
//	// Single Update() 
//	public static bool justLoaded = false;
//
//	// Health
//	private float 	amountHealthPerBar = 100;
//	public float 	defaultHealth = 100;			// The default health to start with at the start of the game.
//	public float 	currentHealth;					// The player's current health during the game.
//	public float 	totalHealth = 100;				// The total health the player has during the course of the game.
//	public float 	maximumHealth = 2000;			// The maximum health the player can acquire.
//
//	// Health Orbs
//		// Health orbs are now connected the amount of total health you have.
//
//	// Invincibility
//	[SerializeField] private bool 	isInvincible = false;
//	private bool 	invincibleTimeout = false;
//	public float	invinciblityFrames = 60;
//
//	// Prefabs
//	public Transform playerPrefab;
//	public Transform spawnPoint;
//
//	// Camera	
//	public float 	nextTimeToSearch = 0;
//	public float 	numberofTimesToSearchForPlayer = 2;
//
//	void Start()
//	{
//		justLoaded = true;
//
//		// Set to actual health based on save file.
//		instance.totalHealth = instance.amountHealthPerBar;
//	}
//
//	void Update()
//	{
//
//	}
//
//	void OnGUI()
//	{
//		int currentHealthOrbs = Mathf.CeilToInt(instance.currentHealth / instance.amountHealthPerBar) - 1,
//			maximumHealthOrbs = Mathf.CeilToInt(instance.totalHealth / instance.amountHealthPerBar) - 1;
//
//		if (currentHealthOrbs < 0)
//		{
//			currentHealthOrbs = 0;
//		}
//
//		if (healthBarText != null)
//		{
//			healthBarText.text = instance.currentHealth + "/" + instance.totalHealth;
//		}
//
//		if (healthOrbsText != null)
//		{
//			healthOrbsText.text = currentHealthOrbs + "/" +currentHealthOrbs;
//		}
//	}
//	
//	// Health of player
//	public void DEBUGHEAL()
//	{
//		instance.HealPlayer(25);
//	}
//
//	public bool HealPlayer(float amount)
//	{
//		float previousHealth = instance.currentHealth;
//		bool hasBeenHealed = false;
//
//		if (instance.currentHealth == amountHealthPerBar)
//		{
//			Debug.Log("Already at full health.");
//		}
//		else if ((instance.currentHealth + amount) > instance.totalHealth)
//		{
//			instance.currentHealth = instance.totalHealth;
//
//			hasBeenHealed = true;
//		}
//		else
//		{
//			instance.currentHealth += amount;
//			
//			hasBeenHealed = true;
//		}
//		
//		Debug.Log ("Health: " + currentHealth.ToString() + ", heal: " + amount.ToString() + ", previous health: " + previousHealth.ToString());
//
//		if (hasBeenHealed)
//		{
//			return true;
//		}
//
//		return false;
//	}
//
//	public bool HealToFull()
//	{
//		bool hasBeenHealed = false;
//		
//		if (instance.currentHealth == amountHealthPerBar)
//		{
//			Debug.Log("Already at full health.");
//		}
//		else
//		{
//			instance.currentHealth = instance.totalHealth;
//
//			hasBeenHealed = true;
//		}
//
//		if (hasBeenHealed)
//		{
//			return true;
//		}
//
//		return false;
//	}
//
//	public void givePlayerHealthOrb()
//	{
//		if (instance.totalHealth + instance.amountHealthPerBar > instance.maximumHealth)
//		{
//			instance.totalHealth = instance.maximumHealth;
//			instance.currentHealth = instance.totalHealth;
//		}
//		else
//		{
//			instance.totalHealth += instance.amountHealthPerBar;
//			instance.currentHealth = instance.totalHealth;
//		}
//	}
//
//	public void DamagePlayer(float amount)
//	{ 
//		// TODO: Add invulnerable frames.
//
//		float health = instance.currentHealth;
//		
//		instance.currentHealth -= amount;
//
//		if (instance.currentHealth <= 0f)
//		{
//			instance.currentHealth = 0f;
//
//			//_playerStats.KillPlayer(target.GetComponent<Player>());
//		}
//		else if (instance.currentHealth <= 0f)
//		{
//			instance.currentHealth = instance.currentHealth + amountHealthPerBar;
//			
//			//_playerStats.currentHealth = -1f;
//		}
//
//		Debug.Log ("Health: " + currentHealth.ToString() + ", damage: " + amount.ToString() + ", previous health: " + health.ToString());
//	}
//
//	public void DEBUGsetHealthToOne()
//	{
//		instance.currentHealth = 1;
//
//		Debug.Log ("Health set to 1 health, 0 orbs.");
//	}
//
//	// Killing of the play
//	public void KillPlayer(Player player)
//	{
//		if (player != null)
//		{
//			Destroy (player.gameObject);
//			Debug.Log ("Player has been killed.");
//			
//			instance.RespawnPlayer();
//		}
//		else
//		{
//			Debug.Log ("player is null");
//		}
//	}
//
//	public void RespawnPlayer()
//	{
//		Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
//	}
//
//	// etc
//	public bool hasPlayerEnteredDeathzone(Collider2D player, Collider2D killzone)
//	{
//		if (player.gameObject.tag == "Player" && killzone.gameObject.tag == "DeathTrigger")
//		{
//			Debug.Log ("Deathz");
//			
//			return true;
//		}
//		
//		return false;
//	}
}
