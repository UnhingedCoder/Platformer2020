using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerViewController : MonoBehaviour
{
    [Range(0, 1)] public float fade;
    public Material playerMat;
    public GameObject burstFX;
    public VectorValue spawnPos;

    private PlayerController player;
    private Animator anim;

    public UnityEvent e_OnDeath;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        anim = GetComponent<Animator>();    
    }

    private void OnEnable()
    {
        this.transform.position = spawnPos.runtimeValue;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim.SetTrigger("Spawn");
    }

    // Update is called once per frame
    void Update()
    {
        playerMat.SetFloat("_Fade", fade);
        anim.SetBool("Invulnerable", player.playerMovement.controller.Invulnerable);
        anim.SetBool("Alive", player.unit.IsAlive());
    }

    public void OnSpawn()
    {
        player.playerMovement.controller.CanMove = true;
    }

    public void OnDeath()
    {
        e_OnDeath.Invoke();
        player.playerMovement.controller.CanMove = false;
    }

    public void SpawnDeathFX()
    {
        GameObject fx = Instantiate(burstFX, new Vector3(this.transform.position.x, this.transform.position.y + 0.25f, 0), burstFX.transform.rotation);
        fx.transform.localScale = new Vector3(1, 1, 1);
    }

    public void OnDeathComplete()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
