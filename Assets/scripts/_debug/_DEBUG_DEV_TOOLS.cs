using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class _DEBUG_DEV_TOOLS : MonoBehaviour
{
	public GameObject GUIPanel;

	public void _DEBUG_TOGGLE_DEV_TOOLS() {
		
		if (GUIPanel != null)
		{
			GUIPanel.SetActive(!GUIPanel.activeInHierarchy);
		}

	}

	public void _DEBUG_TOGGLE_JUMP() {
		Player.Current.Jump = !Player.Current.Jump;
	}

	public void _DEBUG_TOGGLE_DOUBLE_JUMP() {
		Player.Current.DoubleJump = !Player.Current.DoubleJump;
	}

	public void _DEBUG_TOGGLE_TRIPLE_JUMP() {
		Player.Current.TripleJump = !Player.Current.TripleJump;
	}

	public void _DEBUG_TOGGLE_BASIC_BLASTER() {
		Player.Current.BasicBlaster = !Player.Current.BasicBlaster;
	}

	public void _DEBUG_TOGGLE_MISSILE_LAUNCHER() {
		Player.Current.MissileLauncher = !Player.Current.MissileLauncher;
	}

	public void _DEBUG_TOGGLE_LASER() {
		Player.Current.Laser = !Player.Current.Laser;
	}

	public void _DEBUG_TOGGLE_HEALTH_REGEN() {

		Player.Current.HealthRegenOn = !Player.Current.HealthRegenOn;

	}

	public void _DEBUG_TOGGLE_ENERGY_REGEN() {

		Player.Current.EnergyRegenOn = !Player.Current.EnergyRegenOn;

	}

	public void _DEBUG_TOGGLE_ENERGY_CONSUMPTION() {

		Player.Current._DEBUG_INFINITE_ENERGY = !Player.Current._DEBUG_INFINITE_ENERGY;

	}

	public void _DEBUG_DAMAGE_PLAYER() {

		Player.Current.DamageHealth (10);

	}

	public void _DEBUG_DAMAGE_PLAYER_IGNORE_iFRAMES() {

		Player.Current.Health -= 10;

	}

	public void _DEBUG_MOVEMENT_SPEED_DECREASE() {
		
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
	
	public void _DEBUG_MOVEMENT_SPEED_INCREASE() {
		
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

				_DEBUG_TOGGLE_JUMP ();

			} else if (!Player.Current.DoubleJump) {

				_DEBUG_TOGGLE_DOUBLE_JUMP ();

			} else if (!Player.Current.TripleJump) {

				_DEBUG_TOGGLE_TRIPLE_JUMP ();

			} else {

				_DEBUG_TOGGLE_JUMP ();
				_DEBUG_TOGGLE_DOUBLE_JUMP ();
				_DEBUG_TOGGLE_TRIPLE_JUMP ();

			}

		}

	}
		
}