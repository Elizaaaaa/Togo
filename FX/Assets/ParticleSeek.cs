using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSeek : MonoBehaviour {

    [SerializeField] Transform target;
    [SerializeField] float force = 10.0f;

    ParticleSystem ps;

	// Use this for initialization
	void Start () {
        ps = GetComponent<ParticleSystem>();
	}

    private void LateUpdate()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.particleCount];
        ps.GetParticles(particles);

        Vector3 particleWorldPosition; 

        for (int i = 0; i < particles.Length; i++)
        {
            ParticleSystem.Particle p = particles[i];

            particleWorldPosition = new Vector3(0, 0, 0);
            if (ps.main.simulationSpace == ParticleSystemSimulationSpace.Local)
            {
                particleWorldPosition = this.transform.TransformPoint(p.position);
            }
            else if (ps.main.simulationSpace == ParticleSystemSimulationSpace.Custom)
            {
                particleWorldPosition = ps.main.customSimulationSpace.TransformPoint(p.position);
            }
            else
            {
                particleWorldPosition = p.position;
            }

            Vector3 dirctionToTarget = (target.position - particleWorldPosition).normalized;
            Vector3 speed = (dirctionToTarget * force) * Time.deltaTime;

            p.velocity += speed;

            particles[i] = p;
        }

        ps.SetParticles(particles, particles.Length);
    }
}
