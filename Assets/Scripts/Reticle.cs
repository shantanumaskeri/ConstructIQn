using UnityEngine;

public class Reticle : MonoBehaviour 
{

	public Camera cameraFacing;

	private void Update() 
	{
		transform.position = cameraFacing.transform.position + cameraFacing.transform.rotation * Vector3.forward;
		transform.LookAt (cameraFacing.transform.position);
	}

}
