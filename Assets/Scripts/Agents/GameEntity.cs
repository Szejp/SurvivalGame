using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEntity : MonoBehaviour, IDamagable {

	public static Action<GameEntity> OnEntityDestroyed;
	public float maxHealth = 10;
	public bool destructable = true;


	public float health {
		get {
			return _health;
		}

		set {
			_health = Mathf.Clamp(value, 0, maxHealth);
			if (_health <= 0 && destructable) {
				Destroy();
				OnEntityDestroyedHandler();
			}
		}
	}

	[SerializeField]
	private float _health;
	[SerializeField]
	private int _sideId;

	public void SetDamage(float damage) {
		health -= damage;
	}

	public int GetSideId() {
		return _sideId;
	}

	public void Destroy() {
		OnEntityDestroyed?.Invoke(this);
	}

	public void Reset() {
		health = maxHealth;
	}

	protected virtual void Awake() {
		Reset();
	}

	protected void OnDisable() {
		Reset();
	}

	protected virtual void OnEntityDestroyedHandler() { }
}
