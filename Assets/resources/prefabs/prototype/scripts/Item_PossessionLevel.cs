using UnityEngine;
using System.Collections;

public class Item_PossessionLevel : MonoBehaviour
{
	public string ItemName = "";
	public int level = 0;

	void GiveItem()
	{
		// You SHOULD check whether the player is allowed to have the ability, but fuck that, sequence breaking!

		if (level <= PlayerAbilities.PossessionLevel)
		{
			Debug.Log("Current Possession Level of '" + PlayerAbilities.PossessionLevel + "' is more than the item's level of '" + level.ToString() + "'!");
		}
		// TODO: Does this want to be here? We want this game to be sequence breaking friendly.
//		else if (level != PlayerAbilities.PossessionLevel + 1)
//		{
//			Debug.Log("This Possession Level is not the next upgrade!");
//		}
		else
		{
			PlayerAbilities.PossessionLevel = level;
			
			ThePlayer.ErrorMessage = "You acquired the PossessionLevel " + level.ToString() + "!";
			Debug.Log("You acquired the PossessionLevel " + level + "!");
			
			Destroy(gameObject);
		}
	}

	void _DEBUG_SET_POSSESSION_LEVEL_DIRECTLY(bool destroyGameObject)
	{
		PlayerAbilities.PossessionLevel = level;

		if (destroyGameObject)
		{
			Destroy(gameObject);
		}
	}
}
