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
		PlayerAbilities.Current.Jump = !PlayerAbilities.Current.Jump;
	}

	public void _DEBUG_TOGGLE_DOUBLE_JUMP()
	{
		PlayerAbilities.Current.DoubleJump = !PlayerAbilities.Current.DoubleJump;
	}

	public void _DEBUG_TOGGLE_TRIPLE_JUMP()
	{
		PlayerAbilities.Current.TripleJump = !PlayerAbilities.Current.TripleJump;
	}
	
	public void _DEBUG_POSSESSION_LEVEL_DECREASE()
	{
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			PlayerAbilities.Current.PossessionLevel = PlayerAbilities.Current.PossessionLevel - 10;
		}
		else
		{
			PlayerAbilities.Current.PossessionLevel--;
		}

		if (PlayerAbilities.Current.PossessionLevel < 0)
		{
			PlayerAbilities.Current.PossessionLevel = 0;
		}
	}
	
	public void _DEBUG_POSSESSION_LEVEL_INCREASE()
	{
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			PlayerAbilities.Current.PossessionLevel = PlayerAbilities.Current.PossessionLevel + 10;
		}
		else
		{
			PlayerAbilities.Current.PossessionLevel++;
		}

		if (PlayerAbilities.Current.PossessionLevel > 5)
		{
			PlayerAbilities.Current.PossessionLevel = 5;
		}
	}
	
	public void _DEBUG_MAX_POSSESSION_DISTANCE_DECREASE()
	{
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			PlayerAbilities.Current.PossessionMaximumDistance = PlayerAbilities.Current.PossessionMaximumDistance - 10;
		}
		else
		{
			PlayerAbilities.Current.PossessionMaximumDistance--;
		}

		if (PlayerAbilities.Current.PossessionMaximumDistance < 1)
		{
			PlayerAbilities.Current.PossessionMaximumDistance = 1;
		}
	}
	
	public void _DEBUG_MAX_POSSESSION_DISTANCE_INCREASE()
	{
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			PlayerAbilities.Current.PossessionMaximumDistance = PlayerAbilities.Current.PossessionMaximumDistance + 10;
		}
		else
		{
			PlayerAbilities.Current.PossessionMaximumDistance++;
		}

		if (PlayerAbilities.Current.PossessionMaximumDistance > 25)
		{
			PlayerAbilities.Current.PossessionMaximumDistance = 25;
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
		PlayerAbilities.Current.PossessSpeed--;
	
		if (PlayerAbilities.Current.PossessSpeed < 0)
		{
			PlayerAbilities.Current.PossessSpeed = 0;
		}
	}

	public void _DEBUG_POSSESSION_CAST_TIME_INCREASE()
	{
		PlayerAbilities.Current.PossessSpeed++;
		
		if (PlayerAbilities.Current.PossessSpeed > 10)
		{
			PlayerAbilities.Current.PossessSpeed = 10;
		}
	}
}
