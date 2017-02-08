using UnityEngine;
using System.Collections;

public class ItemBomb : Item {

	public override void Use () {

		if (!Player.Current.BOMBS_INFINITE && Player.Current.Bombs_Current < 1) {

			return;

		}

		if (Time.time > nextShotTime) {

			int random = Random.Range (0, Projectiles.Length);
			CurrentDamage	= Mathf.RoundToInt (InitialDamage * Player.Current.Item_Bomb_DamageMod);
			CurrentCooldown = InitialCooldown * Player.Current.Item_Bomb_CooldownMod;
			CurrentAttackLength = InitialAttackLength * Player.Current.Item_Bomb_AttackLengthMod;

			BombProjectile projectile = Instantiate (Projectiles [random], Player.Current.transform.position, Quaternion.identity) as BombProjectile;

			projectile.SetSettings (Vector2.zero, 0f, false, ProjectileType.PLAYER, CurrentDamage, 1, true, false, true, CurrentAttackLength);

			nextShotTime = Time.time + CurrentCooldown;

			if (!Player.Current.BOMBS_INFINITE) {
				Player.Current.Bombs_Current--;
			}

		}

	}

}