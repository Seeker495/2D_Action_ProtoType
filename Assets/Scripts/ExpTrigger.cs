using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ExpTrigger : MonoBehaviour
{
    ParticleSystem m_particleSystem;
    List<ParticleSystem.Particle> m_particleEnter = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> m_particleOutSide = new List<ParticleSystem.Particle>();
    const float EXP_SPEED = 5.0f;

    void OnEnable()
    {
        m_particleSystem = GetComponent<ParticleSystem>();
        m_particleSystem.trigger.AddCollider(GameObject.FindWithTag("Player").GetComponent<BoxCollider2D>());

    }

    private void Update()
    {

    }

    void OnParticleTrigger()
    {
        var main = m_particleSystem.main;
        if (main.maxParticles <= 0)
            Destroy(gameObject);

        ParticleSystem.TriggerModule trigger = m_particleSystem.trigger;
        int numEnter = m_particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, m_particleEnter);
        int numOutSide = m_particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Outside, m_particleOutSide);


        for (int i = 0; i < numEnter; i++)
        {
            --main.maxParticles;
        }
        var playerPosition = m_particleSystem.trigger.GetCollider(0).gameObject.GetComponent<Rigidbody2D>().position;
        for (int i = 0; i < numOutSide; ++i)
        {
            var particle = m_particleOutSide[i];
            var position = m_particleSystem.transform.TransformPoint(particle.position);
            if (particle.startLifetime - particle.remainingLifetime <= 99.0f)
                position = Vector3.MoveTowards(position, new Vector3(playerPosition.x, playerPosition.y, 0.0f), EXP_SPEED * Time.deltaTime);
            particle.position = m_particleSystem.transform.InverseTransformPoint(position);
            m_particleOutSide[i] = particle;
        }

        // 変更したパーティクルをパーティクルシステムに再割り当てします
        m_particleSystem.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, m_particleEnter);
        m_particleSystem.SetTriggerParticles(ParticleSystemTriggerEventType.Outside, m_particleOutSide);

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
