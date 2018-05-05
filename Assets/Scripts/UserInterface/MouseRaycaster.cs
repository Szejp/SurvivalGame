using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRaycaster : MonoBehaviour {

	public IMouseTargetable target { get; private set; }
	public GameObject targetGO;
	public Vector3 planeHit;

	[SerializeField]
	private GameObject _gamePlane;
	private Plane _plane;
	private RaycastHit _hit;
	private Collider _colliderCache;
	private IMouseTargetable _targetCache;
	private float _enterCache;

	private void Awake() {
		_plane = new Plane(_gamePlane.transform.up, _gamePlane.transform.position);
	}

	private void CastRay() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RefreshPointOnGamePlane(_plane, ray);
		RefreshTarget(ray);
	}

	private void RefreshPointOnGamePlane(Plane plane, Ray ray) {
		_plane.Raycast(ray, out _enterCache);
		planeHit = ray.GetPoint(_enterCache);
	}

	private void RefreshTarget(Ray ray) {
		if (Physics.Raycast(ray, out _hit)) {
			if (_hit.collider != _colliderCache) {
				_colliderCache = _hit.collider;
				_targetCache = _hit.collider.GetComponent<IMouseTargetable>();
				if (_targetCache != null) {
					target = _targetCache;
					targetGO = _colliderCache.gameObject;
				}
				else {
					target = null;
					_targetCache = null;
					_colliderCache = null;
					targetGO = null;
				}
			}
		}
		else
			target = null;
	}

	private void Update() {
		CastRay();
	}
}
