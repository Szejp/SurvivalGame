using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effector : MonoBehaviour {

	[SerializeField]
	private ParticleSystem[] particles;

	private void Awake() {
		Projectile.OnEntityDestroyed += p => StartCoroutine(GenerateParticleEffect(particles[0], p.transform.position));
		MeleeWeapon.OnMeleeWeaponHit += position => StartCoroutine(GenerateParticleEffect(particles[1], position));
	}

	private IEnumerator GenerateParticleEffect(ParticleSystem effect, Vector3 position) {
		var result = GameOvermind.instance.spawner.Spawn(effect, position);
		yield return new WaitForSeconds(effect.main.duration);
		GameOvermind.instance.spawner.Collect(result);
	}
}
