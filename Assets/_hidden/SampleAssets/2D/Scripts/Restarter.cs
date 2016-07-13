using UnityEngine;

namespace UnitySampleAssets._2D
{
    public class Restarter : MonoBehaviour
    {
		public static PlayerStats _playerStats;
		Player _player;

		public Transform target;

		GameObject[] search;

		void Start()
		{
			if (_playerStats == null)
			{
				_playerStats 	= GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerStats>();
			}

			if (_player == null)
			{
				_player 	= GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
			}
		}

        private void OnTriggerEnter2D(Collider2D other)
        {
			Debug.Log("Entered kill.");

			FindPlayer();

            if (other.tag == "Player")
			{
			//	_playerStats.KillPlayer(other.gameObject.GetComponent<Player>());
			}
        }

		void FindPlayer()
		{
				GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
			
			target = searchResult.transform;
		}
    }
}