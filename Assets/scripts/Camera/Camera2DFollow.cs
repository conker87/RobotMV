using UnityEngine;

namespace UnitySampleAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform target;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;

		public float nextTimeToSearch = 0;
		public float numberOfTimesToSearchForPlayer = 2;

        private float offsetZ;
        private Vector3 lastTargetPosition;
        private Vector3 currentVelocity;
        private Vector3 lookAheadPos;

		public Vector3 cameraClampMin, cameraClampMax;

        // Use this for initialization
        private void Start()
        {
			// ??
            lastTargetPosition = target.position;
            offsetZ = (transform.position - target.position).z;
            transform.parent = null;
        }

        private void Update()
        {
			// Find the player to follow.
			if (target == null || Input.GetKeyUp(KeyCode.R))
			{
				Debug.Log("Finding player to centre camera.");

				FindPlayer();
				return;
			}

            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (target.position - lastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                lookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
            }
            else
            {
                lookAheadPos = Vector3.MoveTowards(lookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = target.position + lookAheadPos + Vector3.forward*offsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocity, damping);

			newPos = new Vector3(Mathf.Clamp(newPos.x, cameraClampMin.x, cameraClampMax.x),
									Mathf.Clamp(newPos.y, cameraClampMin.y, cameraClampMax.y),
									Mathf.Clamp(newPos.z, cameraClampMin.z, cameraClampMax.z));

            transform.position = newPos;

            lastTargetPosition = target.position;
        }

		void FindPlayer()
		{
			
			if (nextTimeToSearch <= Time.time)
			{
				GameObject searchResult = GameObject.FindGameObjectWithTag("Player");

				target = searchResult.transform;

				nextTimeToSearch = Time.time + (1f / numberOfTimesToSearchForPlayer);
			}

		}

    }
}