using UnityEngine;
using System.Collections;

public class ParticleSortLayerScript : MonoBehaviour
{
	
	void Start () {
		
		this.GetComponent<Renderer>().sortingLayerName = "Projectiles";
		this.GetComponent<Renderer>().sortingOrder = -1;
	}
}
