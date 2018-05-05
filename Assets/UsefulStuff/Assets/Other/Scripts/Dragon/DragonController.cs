using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DragonController : MonoBehaviour {

	public static Action<GameObject> OnDragonAttackedObject;
	public static Action OnDragonJobFinished;
	public GameObject fire;
	public List<GameObject> objectsToDestroy;

	[SerializeField]
	private DragonNavMeshMovement navMeshMovement;
	private AnimationController animController;
	private const float range = 4;
	private Transform objOfInterest;
	private Quaternion rotation;

	private const float rotationDistanceThreshold = 10;

	public void SetObjectsToDestroy(List<GameObject> objs) {
		objectsToDestroy = objs;
	}

	private void Start() {
		animController = GetComponent<AnimationController>();
	}

	private void OnEnable() {

		StartCoroutine(MoveDragon());
	}

	private void Update() {
		transform.position = navMeshMovement.agent.transform.position;

		if (Vector3.SqrMagnitude(transform.position - navMeshMovement.agent.destination) < rotationDistanceThreshold * rotationDistanceThreshold) {
			if (objOfInterest != null) rotation = Quaternion.LookRotation(objOfInterest.position - transform.position, Vector3.up);
		}
		else {
			rotation = navMeshMovement.transform.rotation;
		}

		transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 3);
	}

	private void MoveToClosestObject() {
		objectsToDestroy = objectsToDestroy.Where(q => q != null && q.activeInHierarchy == true).ToList();
		if (objectsToDestroy.Count < 1) {
			if (OnDragonJobFinished != null) OnDragonJobFinished();
			return;
		};
		objOfInterest = objectsToDestroy.OrderBy(p => Vector3.SqrMagnitude(transform.position - p.transform.position)).First().transform;
		navMeshMovement.MoveToClosestPosition(objOfInterest.position);
	}

	private IEnumerator Attack(GameObject obj) {
		yield return StartCoroutine(animController.PlayAttackAnimation());
		if (OnDragonAttackedObject != null) OnDragonAttackedObject(obj);
	}

	private IEnumerator MoveDragon() {
		while (gameObject.activeInHierarchy) {
			MoveToClosestObject();
			yield return new WaitForSeconds(1);
		}
	}
}
