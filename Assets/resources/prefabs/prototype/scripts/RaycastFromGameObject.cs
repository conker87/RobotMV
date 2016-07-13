using UnityEngine;
using System.Collections;

public class RaycastFromGameObject : MonoBehaviour
{
	public static bool RaycastToMouse(GameObject raycastFrom, LayerMask layerMask, out RaycastHit2D hit, int maxDistance)
	{
		Vector2 cameraPositionInWorldSpace = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 raycastFromPosition = raycastFrom.transform.position;
		float distance = Vector2.Distance(raycastFromPosition, cameraPositionInWorldSpace);
		float clampedMagnitude = Mathf.Clamp(distance, 0, maxDistance);

		hit = Physics2D.Raycast(raycastFromPosition, (cameraPositionInWorldSpace - raycastFromPosition), clampedMagnitude, layerMask);

		if (hit)
		{
			return true;
		}

		return false;
	}

	public static bool RaycastToMouse(GameObject raycastFrom, LayerMask layerMask, out RaycastHit2D hit, int maxDistance, bool debug)
	{
		Vector2 cameraPositionInWorldSpace = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 raycastFromPosition = raycastFrom.transform.position;
		float distance = Vector2.Distance(raycastFromPosition, cameraPositionInWorldSpace);

		if (debug)
		{
			Debug.DrawRay(raycastFromPosition, Vector2.ClampMagnitude(cameraPositionInWorldSpace - raycastFromPosition, maxDistance), Color.magenta);
		}

		float clampedMagnitude = Mathf.Clamp(distance, 0, maxDistance);

		hit = Physics2D.Raycast(raycastFromPosition, (cameraPositionInWorldSpace - raycastFromPosition), clampedMagnitude, layerMask);
		
		if (hit)
		{
			return true;
		}
		
		return false;
	}

	public static bool RaycastToGameObject(GameObject raycastFrom, GameObject raycastTo, LayerMask layerMask, out RaycastHit2D hit, int maxDistance, bool debug)
	{
		Vector2 raycastToPosition = raycastTo.transform.position;
		Vector2 raycastFromPosition = raycastFrom.transform.position;
		float distance = Vector2.Distance(raycastFromPosition, raycastToPosition);
		float clampedMagnitude = Mathf.Clamp(distance, 0, maxDistance);

		if (debug)
		{
			Debug.DrawRay(raycastFromPosition, Vector2.ClampMagnitude(raycastToPosition - raycastFromPosition, maxDistance), Color.magenta);
		}
		
		hit = Physics2D.Raycast(raycastFromPosition, (raycastToPosition - raycastFromPosition), clampedMagnitude, layerMask);
		
		if (hit)
		{
			return true;
		}
		
		return false;
	}
}