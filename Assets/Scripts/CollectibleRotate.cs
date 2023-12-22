using UnityEngine;

public class SimpleCollectibleScript : MonoBehaviour 
{
	[SerializeField] float RotationSpeed;

	// Update is called once per frame
	void Update () 
	{
		transform.Rotate (Vector3.up * RotationSpeed * Time.deltaTime, Space.World);
	}
}
