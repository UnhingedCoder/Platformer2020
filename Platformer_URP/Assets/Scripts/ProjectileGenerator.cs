using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGenerator : MonoBehaviour
{
    public Vector3 directionToShoot;
    public float rateOfFire;
    public ObjectPooler objPooler;

    private void Awake()
    {
        objPooler = FindObjectOfType<ObjectPooler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("LaunchProjectile", 0f, rateOfFire);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LaunchProjectile()
    {
        ProjectileController projectile = objPooler.GetPooledObject(this.transform).GetComponent<ProjectileController>();
        projectile.SetupProjectile(directionToShoot);
        projectile.gameObject.SetActive(true);
        if(projectile != null)
            Debug.Log("Launching projectile");
    }
}
