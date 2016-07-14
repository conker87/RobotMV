using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FindNearestGameObject : MonoBehaviour
{
	public string searchTag = "Respawn";
	public float scanFrequency = 1.0f;
	public Text nearestGUI;

	Transform target;
	
	void Start()
	{
		Invoke("ScanForTarget", scanFrequency);
	}
		
	void Update()
	{ // we rotate to look at the target every frame (if there is one)
		if (target != null)
		{
			transform.LookAt(target);
		}
	}

	void OnGUI()
	{
		if (nearestGUI != null && target != null)
		{
			nearestGUI.text = target.ToString();
		}
	}
			
	void ScanForTarget() 
	{
		target = GetNearestTaggedObject();
		
		Invoke("ScanForTarget", scanFrequency);
	}
		
	Transform GetNearestTaggedObject()
	{
		float nearestDistanceSqr = Mathf.Infinity;
		GameObject[] taggedGameObjects = GameObject.FindGameObjectsWithTag(searchTag); 
		Transform nearestObj = null;
		
		// loop through each tagged object, remembering nearest one found
		foreach (GameObject obj in taggedGameObjects)
		{
			Vector3 objectPos = obj.transform.position;
			float distanceSqr = (objectPos - transform.position).sqrMagnitude;
			
			if (distanceSqr > nearestDistanceSqr)
			{
				nearestObj = obj.transform;
				nearestDistanceSqr = distanceSqr;
			}

			Debug.Log (obj.ToString());
		}
		
		return nearestObj;
	}
}