using UnityEngine;

public class CameraRotation : MonoBehaviour {

	[SerializeField]
	float speed;

	// Update is called once per frame
	void Update () {

		transform.Rotate(0, 0,Time.deltaTime * speed);	
	}
}
