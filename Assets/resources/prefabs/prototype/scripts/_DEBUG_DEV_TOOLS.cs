using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class _DEBUG_DEV_TOOLS : MonoBehaviour
{
	public GameObject GUIPanel, details;

	public void _DEBUG_TOGGLE_DEV_TOOLS()
	{
		if (GUIPanel.activeInHierarchy)
		{
			details.SetActive(false);
		}

		if (GUIPanel != null)
		{
			GUIPanel.SetActive(!GUIPanel.activeInHierarchy);
		}
	}

	public void _DEBUG_TOGGLE_JUMP()
	{
		PlayerAbilities.Jump = !PlayerAbilities.Jump;
	}

	public void _DEBUG_TOGGLE_DOUBLE_JUMP()
	{
		PlayerAbilities.DoubleJump = !PlayerAbilities.DoubleJump;
	}

	public void _DEBUG_TOGGLE_TRIPLE_JUMP()
	{
		PlayerAbilities.TripleJump = !PlayerAbilities.TripleJump;
	}
	
	public void _DEBUG_POSSESSION_LEVEL_DECREASE()
	{
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			PlayerAbilities.PossessionLevel = PlayerAbilities.PossessionLevel - 10;
		}
		else
		{
			PlayerAbilities.PossessionLevel--;
		}

		if (PlayerAbilities.PossessionLevel < 0)
		{
			PlayerAbilities.PossessionLevel = 0;
		}
	}
	
	public void _DEBUG_POSSESSION_LEVEL_INCREASE()
	{
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			PlayerAbilities.PossessionLevel = PlayerAbilities.PossessionLevel + 10;
		}
		else
		{
			PlayerAbilities.PossessionLevel++;
		}

		if (PlayerAbilities.PossessionLevel > 5)
		{
			PlayerAbilities.PossessionLevel = 5;
		}
	}
	
	public void _DEBUG_MAX_POSSESSION_DISTANCE_DECREASE()
	{
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			PlayerAbilities.PossessionMaximumDistance = PlayerAbilities.PossessionMaximumDistance - 10;
		}
		else
		{
			PlayerAbilities.PossessionMaximumDistance--;
		}

		if (PlayerAbilities.PossessionMaximumDistance < 1)
		{
			PlayerAbilities.PossessionMaximumDistance = 1;
		}
	}
	
	public void _DEBUG_MAX_POSSESSION_DISTANCE_INCREASE()
	{
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			PlayerAbilities.PossessionMaximumDistance = PlayerAbilities.PossessionMaximumDistance + 10;
		}
		else
		{
			PlayerAbilities.PossessionMaximumDistance++;
		}

		if (PlayerAbilities.PossessionMaximumDistance > 25)
		{
			PlayerAbilities.PossessionMaximumDistance = 25;
		}
	}

	public void _DEBUG_MOVEMENT_SPEED_DECREASE()
	{
		GameObject player;
		player = GameObject.FindGameObjectWithTag("Player");

		if (player != null)
		{
			ThePlayer thePlayer = player.GetComponent<ThePlayer>();

			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			{
				thePlayer.moveSpeed = thePlayer.moveSpeed - 10;
			}
			else
			{
				thePlayer.moveSpeed--;
			}

			if (thePlayer.moveSpeed < 0)
			{
				thePlayer.moveSpeed = 0;
			}
		}
		else
		{
			Debug.Log("Player is null");
		}
	}
	
	public void _DEBUG_MOVEMENT_SPEED_INCREASE()
	{
		GameObject player;
		player = GameObject.FindGameObjectWithTag("Player");

		if (player != null)
		{
			ThePlayer thePlayer = player.GetComponent<ThePlayer>();

			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			{
				thePlayer.moveSpeed = thePlayer.moveSpeed + 10;
			}
			else
			{
				thePlayer.moveSpeed++;
			}

			if (thePlayer.moveSpeed > 200)
			{
				thePlayer.moveSpeed = 200;
			}
		}
		else
		{
			Debug.Log("Player is null");
		}
	}

	public void _DEBUG_POSSESSION_CAST_TIME_DECREASE()
	{
		PlayerAbilities.PossessSpeed--;
	
		if (PlayerAbilities.PossessSpeed < 0)
		{
			PlayerAbilities.PossessSpeed = 0;
		}
	}

	public void _DEBUG_POSSESSION_CAST_TIME_INCREASE()
	{
		PlayerAbilities.PossessSpeed++;
		
		if (PlayerAbilities.PossessSpeed > 10)
		{
			PlayerAbilities.PossessSpeed = 10;
		}
	}
}
