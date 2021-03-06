using UnityEngine;
using System.Collections;

public class ItemBombMega : Item {

	public override void Use () {

		if (!Player.Current.CHEAT_BOMBS_MEGA_INFINITE && Player.Current.BombsMegaCurrent < 1) {

			return;

		}


		if (Time.time > nextShotTime) {

			int random = Random.Range (0, Projectiles.Length);
			CurrentDamage	= Mathf.RoundToInt (InitialDamage * Player.Current.ItemBombMegaDamageMod);
			CurrentCooldown = InitialCooldown * Player.Current.ItemBombMegaCooldownMod;
			CurrentAttackLength = InitialAttackLength * Player.Current.ItemBombMegaAttackLengthMod;

			BombProjectile projectile = Instantiate (Projectiles [random], Player.Current.transform.position, Quaternion.identity) as BombProjectile;

			projectile.SetSettings (Vector2.zero, 0f, false, ProjectileType.PLAYER, CurrentDamage, 4, true, false, true, CurrentAttackLength);

			nextShotTime = Time.time + CurrentCooldown;

			if (!Player.Current.CHEAT_BOMBS_MEGA_INFINITE) {
				Player.Current.BombsMegaCurrent--;
			}

		}

	}

}