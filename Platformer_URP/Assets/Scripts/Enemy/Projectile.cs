using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public string objectPoolerName;
    public string particleObjectPoolerName;

    private Rigidbody2D _rigidBody;
    private ObjectPooler _objectPooler;
    private ObjectPooler _particleObjectPooler;

    private void Awake()
    {
        _rigidBody = this.gameObject.GetComponent<Rigidbody2D>();
        _objectPooler = GameObject.Find("ObjectPoolers/" + objectPoolerName).GetComponent<ObjectPooler>();
        _particleObjectPooler = GameObject.Find("ObjectPoolers/" + particleObjectPoolerName).GetComponent<ObjectPooler>();
    }

    public void SetupProjectile(Vector3 targetDir)
    {
        this.transform.rotation = Quaternion.Euler(DetectProjectileFacingDirection(targetDir));
        _rigidBody.velocity = new Vector3(targetDir.x * speed, targetDir.y * speed, 0);
    }
    Vector3 DetectProjectileFacingDirection(Vector3 direction)
    {
        float temp = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit " + collision.gameObject.name);
        _objectPooler.DestroyPooledObject(this.gameObject);

        Vector3 offsetPos = new Vector3(this.transform.position.x - 0.5f, this.transform.position.y, 0);

        ParticleSystem ps_burstFx = _particleObjectPooler.GetPooledObject(offsetPos).GetComponent<ParticleSystem>();

        if (ps_burstFx == null)
            return;

        ps_burstFx.gameObject.SetActive(true);
        ps_burstFx.Stop();
        ps_burstFx.Play();
    }
}
