using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ExpTrigger : MonoBehaviour
{
    ParticleSystem ParticleSystem;
    int exp = 0;
   List<ParticleSystem.Particle> ParticleEnter = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> ParticleOutSide = new List<ParticleSystem.Particle>();
    const float EXP_SPEED = 5.0f;

    void OnEnable()
    {
        ParticleSystem = GetComponent<ParticleSystem>();
        ParticleSystem.trigger.AddCollider(GameObject.FindWithTag("Player").GetComponent<BoxCollider2D>());

    }

    private void Update()
    {

    }

    void OnParticleTrigger()
    {
        var main = ParticleSystem.main;
        if (main.maxParticles <= 0)
            Destroy(gameObject);

        ParticleSystem.TriggerModule trigger = ParticleSystem.trigger;
        int numEnter = ParticleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, ParticleEnter);
        int numOutSide = ParticleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Outside, ParticleOutSide);


        for (int i = 0; i < numEnter; i++)
        {
            Debug.Log($"Exp: {++exp}");
            --main.maxParticles;

        }
        var playerPosition = ParticleSystem.trigger.GetCollider(0).gameObject.GetComponent<Rigidbody2D>().position;
        for (int i = 0; i < numOutSide; ++i)
        {
            var particle = ParticleOutSide[i];
            var velocity = ParticleSystem.transform.TransformPoint(particle.velocity);
            var position = ParticleSystem.transform.TransformPoint(particle.position);
            if (particle.startLifetime - particle.remainingLifetime <= 99.0f)
                position = Vector3.MoveTowards(position, new Vector3(playerPosition.x, playerPosition.y, 0.0f), EXP_SPEED * Time.deltaTime);
            particle.position = ParticleSystem.transform.InverseTransformPoint(position);
            ParticleOutSide[i] = particle;
        }

        // 変更したパーティクルをパーティクルシステムに再割り当てします
        ParticleSystem.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, ParticleEnter);
        ParticleSystem.SetTriggerParticles(ParticleSystemTriggerEventType.Outside, ParticleOutSide);

        if (numEnter > 0)
            trigger.enter = ParticleSystemOverlapAction.Kill;
        else
            trigger.enter = ParticleSystemOverlapAction.Callback;

        trigger.outside = ParticleSystemOverlapAction.Callback;

    }

    private IEnumerator Moving(Particle p)
    {
        yield return null;
    }
}
