using UnityEngine;
using System.Collections;

public class ItemBomb : Item {

	public override void Use () {

		if (!Player.Current.CHEAT_BOMBS_INFINITE && Player.Current.BombsCurrent < 1) {

			return;

		}

		if (Time.time > nextShotTime) {

			int random = Random.Range (0, Projectiles.Length);
			CurrentDamage	= Mathf.RoundToInt (InitialDamage * Player.Current.ItemBombDamageMod);
			CurrentCooldown = InitialCooldown * Player.Current.ItemBombCooldownMod;
			CurrentAttackLength = InitialAttackLength * Player.Current.ItemBombAttackLengthMod;

			BombProjectile projectile = Instantiate (Projectiles [random], Player.Current.transform.position, Quaternion.identity) as BombProjectile;

			projectile.SetSettings (Vector2.zero, 0f, false, ProjectileType.PLAYER, CurrentDamage, 1, true, false, true, CurrentAttackLength);

			nextShotTime = Time.time + CurrentCooldown;

			if (!Player.Current.CHEAT_BOMBS_INFINITE) {
				Player.Current.BombsCurrent--;
			}

		}

	}

}