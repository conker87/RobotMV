using UnityEngine;
using System.Collections;

public class ItemBombMega : Item {

	public override void Use () {

		if (!Player.Current.BOMBS_MEGA_INFINITE && Player.Current.Bombs_Mega_Current < 1) {

			return;

		}


		if (Time.time > nextShotTime) {

			int random = Random.Range (0, Projectiles.Length);
			CurrentDamage	= Mathf.RoundToInt (InitialDamage * Player.Current.Item_Bomb_Mega_DamageMod);
			CurrentCooldown = InitialCooldown * Player.Current.Item_Bomb_Mega_CooldownMod;
			CurrentAttackLength = InitialAttackLength * Player.Current.Item_Bomb_Mega_AttackLengthMod;

			BombProjectile projectile = Instantiate (Projectiles [random], Player.Current.transform.position, Quaternion.identity) as BombProjectile;

			projectile.SetSettings (Vector2.zero, 0f, false, ProjectileType.PLAYER, CurrentDamage, 4, true, false, true, CurrentAttackLength);

			nextShotTime = Time.time + CurrentCooldown;

			if (!Player.Current.BOMBS_MEGA_INFINITE) {
				Player.Current.Bombs_Mega_Current--;
			}

		}

	}

}