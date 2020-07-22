using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGenerator : MonoBehaviour
{
    public Vector3 directionToShoot;
    public bool fireOnStart;
    public float delay;
    public float rateOfFire;
    public ParticleSystem ps_Flare;
    public string objectPoolerName;

    private ObjectPooler _objectPooler;

    private void Awake()
    {
        _objectPooler = GameObject.Find("ObjectPoolers/" + objectPoolerName).GetComponent<ObjectPooler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(fireOnStart)
            InvokeRepeating("LaunchProjectile", delay, rateOfFire);
    }

    public void LaunchProjectile()
    {
        if (_objectPooler == null)
            return;

        Projectile projectile = _objectPooler.GetPooledObject(this.transform.position).GetComponent<Projectile>();

        if (projectile == null)
            return;

        projectile.gameObject.SetActive(true);
        projectile.SetupProjectile(directionToShoot);
        ps_Flare.gameObject.SetActive(true);
        ps_Flare.Stop();
        ps_Flare.Play();
    }
}
