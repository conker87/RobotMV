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
		PlayerAbilities.Current.Jump = !PlayerAbilities.Current.Jump;
	}

	public void _DEBUG_TOGGLE_DOUBLE_JUMP() {
		PlayerAbilities.Current.DoubleJump = !PlayerAbilities.Current.DoubleJump;
	}

	public void _DEBUG_TOGGLE_TRIPLE_JUMP() {
		PlayerAbilities.Current.TripleJump = !PlayerAbilities.Current.TripleJump;
	}

	public void _DEBUG_TOGGLE_BASIC_BLASTER() {
		PlayerAbilities.Current.BasicBlaster = !PlayerAbilities.Current.BasicBlaster;
	}

	public void _DEBUG_TOGGLE_MISSILE_LAUNCHER() {
		PlayerAbilities.Current.MissileLauncher = !PlayerAbilities.Current.MissileLauncher;
	}

	public void _DEBUG_TOGGLE_HEALTH_REGEN() {

		PlayerAbilities.Current.HealthRegenOn = !PlayerAbilities.Current.HealthRegenOn;

	}

	public void _DEBUG_TOGGLE_ENERGY_REGEN() {

		PlayerAbilities.Current.EnergyRegenOn = !PlayerAbilities.Current.EnergyRegenOn;

	}

	public void _DEBUG_DAMAGE_PLAYER() {

		PlayerAbilities.Current.Health -= 10;

	}

	public void _DEBUG_MOVEMENT_SPEED_DECREASE() {
		
		GameObject player;
		player = GameObject.FindGameObjectWithTag("Player");

		if (player != null)
		{
			PlayerController thePlayer = player.GetComponent<PlayerController>();

			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			{
				PlayerAbilities.Current.MoveSpeed = PlayerAbilities.Current.MoveSpeed - 10;
			}
			else
			{
				PlayerAbilities.Current.MoveSpeed--;
			}

			if (PlayerAbilities.Current.MoveSpeed < 0)
			{
				PlayerAbilities.Current.MoveSpeed = 0;
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
			PlayerController thePlayer = player.GetComponent<PlayerController>();

			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			{
				PlayerAbilities.Current.MoveSpeed = PlayerAbilities.Current.MoveSpeed + 10;
			}
			else
			{
				PlayerAbilities.Current.MoveSpeed++;
			}

			if (PlayerAbilities.Current.MoveSpeed > 200)
			{
				PlayerAbilities.Current.MoveSpeed = 200;
			}
		}
		else
		{
			Debug.Log("Player is null");
		}

	}
		
}