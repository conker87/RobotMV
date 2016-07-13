using UnityEngine;
using System.Collections;

public class _DEBUG_SHOW_PLAYER_ABILITIES : MonoBehaviour
{
	public static void DoGUI()
	{
		GUIStyle style;
		
		style = new GUIStyle(GUI.skin.label);
		style.normal.textColor = Color.red;

		GUI.Label(new Rect(10, 30, 200, 20), "Jump: " + PlayerAbilities.Jump.ToString(), style);
		GUI.Label(new Rect(10, 50, 200, 20), "DoubleJump: " + PlayerAbilities.DoubleJump.ToString(), style);
		GUI.Label(new Rect(10, 70, 200, 20), "TripleJump: " + PlayerAbilities.TripleJump.ToString(), style);
		GUI.Label(new Rect(10, 90, 200, 20), "PossessionLevel: " + PlayerAbilities.PossessionLevel.ToString(), style);
		GUI.Label(new Rect(10, 110, 200, 20), "MaxPossessionDistance: " + PlayerAbilities.PossessionMaximumDistance.ToString(), style);
	}
}
