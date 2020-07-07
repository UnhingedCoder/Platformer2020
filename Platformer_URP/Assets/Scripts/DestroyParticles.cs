using UnityEngine;
using System.Collections;

public class DestroyParticles : MonoBehaviour
{
    private void Start()
    {
        Destroy(this.gameObject, GetComponent<ParticleSystem>().main.duration);
    }

}