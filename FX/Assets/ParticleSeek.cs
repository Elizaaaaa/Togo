using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ParticleSystem))]

public class ParticleSeek : MonoBehaviour {

    [SerializeField] Transform target;
    [SerializeField] float force = 10.0f;

    ParticleSystem ps;
    ParticleSystem.Particle[] particles;
    ParticleSystem.MainModule particleMain;

	// Use this for initialization
	void Start () {
        ps = GetComponent<ParticleSystem>();
        particleMain = ps.main;
	}

    private void LateUpdate()
    {
        int particleMax = particleMain.maxParticles;
        if (particles == null || particles.Length < particleMax)
        {
            particles = new ParticleSystem.Particle[particleMax];
        }

        ps.GetParticles(particles);

        float forceDelta = force * Time.deltaTime;
        Vector3 targetTransformPosition = new Vector3(0,0,0);

        switch (particleMain.simulationSpace)
        {
            case ParticleSystemSimulationSpace.Local:
                {
                    targetTransformPosition = transform.InverseTransformPoint(target.position);
                    break;
                }
            case ParticleSystemSimulationSpace.Custom:
                {
                    targetTransformPosition = particleMain.customSimulationSpace.TransformPoint(target.position);
                    break;
                }
            case ParticleSystemSimulationSpace.World:
                {
                    targetTransformPosition = target.position;
                    break;
                }
        }

        for (int i = 0; i < particles.Length; i++)
        {
            Vector3 dirctionToTarget = Vector3.Normalize(targetTransformPosition - particles[i].position);
            Vector3 speed = (dirctionToTarget * forceDelta);

            particles[i].velocity += speed;

        }

        ps.SetParticles(particles, particles.Length);
    }
}
