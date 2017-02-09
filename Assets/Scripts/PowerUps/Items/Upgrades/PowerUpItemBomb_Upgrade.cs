using UnityEngine;
using System.Collections;

public class PowerUpItemBomb_Upgrade : PowerUp {

	// These are MULTIPLICATED modifiers!
	public float AttackLengthMod = 2f, CooldownMod = 0.5f, DamageMod = 2f;

	public override void Give() {

		Player.Current.Item_Bomb_AttackLengthMod 		*= AttackLengthMod;
		Player.Current.Item_Bomb_AttackLengthMod 		*= CooldownMod;
		Player.Current.Item_Bomb_AttackLengthMod		*= DamageMod;

		base.Give ();

	}

}
