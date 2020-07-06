using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("FireProjectiles", 0.0f, 1.0f / fireRate);
    }

    // Update is called once per frame
    void Update()
    {
    }



    //private bool IsFacingTarget()
    //{
    //    Vector2 dir = (target.transform.position - transform.position).normalized;
    //    Vector2 faceDir = transform.right;
    //    if (transform.localScale.x < 0)
    //        faceDir = -faceDir;
    //    float dot = Vector2.Dot(dir, faceDir);

    //    if (dot > 0)
    //        return true;
    //    else
    //        return false;
    //}

    //private void CreateProjectiles()
    //{
    //    ProjectileController projectile = _objectPooler.GetPooledObject(fireSpawnPoint).GetComponent<ProjectileController>();
    //    projectile.gameObject.SetActive(true);

    //    targetDirection = (target.transform.position - fireSpawnPoint.position).normalized;

    //    projectile.SetupProjectile(targetDirection);
    //}

    //private void FireProjectiles()
    //{
    //    if (target == null)
    //        return;
    //    if (_characterController2D.Grounded && _characterController2D.IsStationary() && IsFacingTarget())
    //    {
    //        CreateProjectiles();
    //    }
    //}

}
