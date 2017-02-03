using UnityEngine;
using System.Collections;

public class PowerUpBombMegaOrb : PowerUp {

	public int bombs = 1;

	public override void Give() {

		Player.Current.Bombs_Mega_Current = (Player.Current.Bombs_Mega_Current == Player.Current.Bombs_Mega_Max) ? Player.Current.Bombs_Mega_Max : Player.Current.Bombs_Mega_Current + bombs;

		base.Give ();

	}

}
