using System.Collections;
using UnityEngine;

public class ParticleEmitterOnEvent : MonoBehaviour
{
    [SerializeField] public ParticleSystem Particle;
    [SerializeField] public float Delay;


    public void EmitParticles(GameObject gameObject)
    {
        if (null != Particle)
        {
            if(Delay <= 0)
            {
                Instantiate(Particle, gameObject.transform.position, gameObject.transform.rotation);
            }
            else
            {
                StartCoroutine(EmitWithDelay());
            }
        }
    }

    private IEnumerator EmitWithDelay()
    {
        yield return new WaitForSeconds(Delay);

        Instantiate(Particle, gameObject.transform.position, gameObject.transform.rotation);
    }
}
