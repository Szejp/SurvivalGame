using UnityEngine;

public class SimplePlane3D : MonoBehaviour {

	[SerializeField]
	private float thurst = 10;
	[SerializeField]
	private float torque = 10;
	[SerializeField]
	private float topSpeed = 200;
	[SerializeField]
	private Rigidbody rbody;
	[SerializeField]
	private float turnFactor = 1;
	private Vector2 turnVector;

	public void Turn(Vector2 turnVector) {
		this.turnVector = turnVector * turnFactor;
		Debug.Log("[SimplePlane3D] turn vector: " + this.turnVector);
	}

	private void Awake() {
		rbody = GetComponent<Rigidbody>();
		TouchInput.OnDragEvent += Turn;
	}

	private void FixedUpdate() {
		if (rbody != null) {
			rbody.AddForce(rbody.transform.forward * thurst);
			rbody.AddRelativeTorque(Vector3.up * torque * turnVector.x);
			rbody.AddRelativeTorque(Vector3.right * torque * turnVector.y);
		}

		if (rbody.velocity.magnitude > topSpeed)
			rbody.velocity = rbody.velocity.normalized * topSpeed;

		turnVector = Vector2.zero;
	}

	private void Update() {
#if UNITY_EDITOR
		float turnHorizontal = Input.GetAxis("Horizontal");
		float turnVertical = Input.GetAxis("Vertical");
	//	turnVector = new Vector2(turnHorizontal, turnVertical);
#endif
	}
}
