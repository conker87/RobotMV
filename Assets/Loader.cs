using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {
	
	public GameObject inputManager, pauseManager;


	void Awake ()
	{
		//Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
		if (InputManager.Current == null)

			//Instantiate gameManager prefab
			Instantiate(inputManager);

		//Check if a SoundManager has already been assigned to static variable GameManager.instance or if it's still null
		if (PauseManager.Current == null)

			//Instantiate SoundManager prefab
			Instantiate(pauseManager);
		
	}

}