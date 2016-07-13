using UnityEngine;
using System.Collections;

public class Item_Jump_Double : MonoBehaviour
{
	public string ItemName = "";

	void GiveItem()
	{
		// You SHOULD check whether the player is allowed to have the ability, but fuck that, sequence breaking!
		PlayerAbilities.DoubleJump = true;
			
		Destroy(gameObject);
	}
}
