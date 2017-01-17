using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DEV_TOOLS : MonoBehaviour
{
	public GameObject GUIPanel;

	public void TOGGLE_DEV_TOOLS() {
		
		if (GUIPanel != null)
		{
			GUIPanel.SetActive(!GUIPanel.activeInHierarchy);
		}

	}

	public void TOGGLE_JUMP() {
		Player.Current.Jump = !Player.Current.Jump;
	}

	public void TOGGLE_DOUBLE_JUMP() {
		Player.Current.DoubleJump = !Player.Current.DoubleJump;
	}

	public void TOGGLE_TRIPLE_JUMP() {
		Player.Current.TripleJump = !Player.Current.TripleJump;
	}

	public void TOGGLE_BASIC_BLASTER() {
		Player.Current.BasicBlaster = !Player.Current.BasicBlaster;
	}

	public void TOGGLE_BASIC_BLASTER_CHARGED() {
		Player.Current.BasicBlasterChargeShot = !Player.Current.BasicBlasterChargeShot;
	}

	public void TOGGLE_MISSILE_LAUNCHER() {
		Player.Current.MissileLauncher = !Player.Current.MissileLauncher;
	}

	public void TOGGLE_LASER() {
		Player.Current.Laser = !Player.Current.Laser;
	}

	public void TOGGLE_HEALTH_REGEN() {

		Player.Current.HealthRegenOn = !Player.Current.HealthRegenOn;

	}
		
	public void DAMAGE_PLAYER() {

		Player.Current.DamageVital("HEALTH", 10);

	}

	public void DAMAGE_PLAYER_IGNORE_iFRAMES() {

		Player.Current.Health -= 10;

	}

	public void MOVEMENT_SPEED_DECREASE() {
		
		Player player = Player.Current;

		if (player != null)
		{

			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			{
				Player.Current.MoveSpeed = Player.Current.MoveSpeed - 10;
			}
			else
			{
				Player.Current.MoveSpeed--;
			}

			if (Player.Current.MoveSpeed < 0)
			{
				Player.Current.MoveSpeed = 0;
			}
		}
		else
		{
			Debug.Log("Player is null");
		}

	}
	
	public void MOVEMENT_SPEED_INCREASE() {
		
		GameObject player;
		player = GameObject.FindGameObjectWithTag("Player");

		if (player != null)
		{

			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			{
				Player.Current.MoveSpeed = Player.Current.MoveSpeed + 10;
			}
			else
			{
				Player.Current.MoveSpeed++;
			}

			if (Player.Current.MoveSpeed > 200)
			{
				Player.Current.MoveSpeed = 200;
			}

		}
		else
		{
			Debug.Log("Player is null");
		}

	}

	void Update() {

		if (Input.GetKeyUp (KeyCode.J)) {

			if (!Player.Current.Jump) {

				TOGGLE_JUMP ();

			} else if (!Player.Current.DoubleJump) {

				TOGGLE_DOUBLE_JUMP ();

			} else if (!Player.Current.TripleJump) {

				TOGGLE_TRIPLE_JUMP ();

			} else {

				TOGGLE_JUMP ();
				TOGGLE_DOUBLE_JUMP ();
				TOGGLE_TRIPLE_JUMP ();

			}

		}

	}
		
}