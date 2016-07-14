using UnityEngine;
using System.Collections;

public class ParticleSortLayerScript : MonoBehaviour
{
	void Start ()
	{
		//Change Foreground to the layer you want it to display on 
		//You could prob. make a public variable for this
		this.GetComponent<Renderer>().sortingLayerName = "Characters";
		this.GetComponent<Renderer>().sortingOrder = -2;
	}
}
