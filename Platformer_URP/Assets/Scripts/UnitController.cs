using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public Transform fireSpawnPoint;
    public float fireRate;
    public GameObject target;
    private Vector3 targetDirection;

    private CharacterController2D _characterController2D;
    private TargetDetector _targetDetector;
    public ObjectPooler _objectPooler;

    private void Awake()
    {
        _characterController2D = GetComponent<CharacterController2D>();
        _targetDetector = GetComponent<TargetDetector>();
        _objectPooler = FindObjectOfType<ObjectPooler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("FireProjectiles", 0.0f, 1.0f / fireRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        target = _targetDetector.DetectTarget();
    }

    private bool IsFacingTarget()
    {
        Vector2 dir = (target.transform.position - transform.position).normalized;
        Vector2 faceDir = transform.right;
        if (transform.localScale.x < 0)
            faceDir = -faceDir;
        float dot = Vector2.Dot(dir, faceDir);

        if (dot > 0)
            return true;
        else
            return false;
    }

    private void CreateProjectiles()
    {
        ProjectileController projectile = _objectPooler.GetPooledObject(fireSpawnPoint).GetComponent<ProjectileController>();
        projectile.gameObject.SetActive(true);

        targetDirection = (target.transform.position - fireSpawnPoint.position).normalized;

        projectile.SetupProjectile(targetDirection);
    }

    private void FireProjectiles()
    {
        if (target == null)
            return;

        if (_characterController2D.Grounded && IsFacingTarget())
            CreateProjectiles();
    }

}
