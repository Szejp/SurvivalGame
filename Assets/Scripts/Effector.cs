using System.Collections;
using QFramework.GameTools.Effects;
using QFramework.GameTools.Entities;
using UnityEngine;

public class Effector : MonoBehaviour
{
    [SerializeField] private Effect[] particles;

    private void Awake()
    {
        Projectile.OnDestroyed += OnEntityDestroyedHandler;
        MeleeWeapon.OnMeleeWeaponHit +=
            position => StartCoroutine(GenerateEffect(particles[1], position));
    }

    void OnDestroy()
    {
        Projectile.OnDestroyed -= OnEntityDestroyedHandler;
    }

    void OnEntityDestroyedHandler(Entity entity)
    {
        if (entity.DestroyEffect)
            StartCoroutine(GenerateEffect(entity.DestroyEffect, entity.transform.position));
    }

    private IEnumerator GenerateEffect(Effect effect, Vector3 position)
    {
        if (effect != null)
        {
            var result = GameOvermind.instance.spawner.Spawn(effect, position);
            yield return new WaitForSeconds(.9f * effect.Duration);
            GameOvermind.instance.spawner.Collect(result);
        }
    }
}