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
    public Transform directionIndicator;
    public ParticleSystem ps_OrbPickup;

    public float m_Angle;
    public bool flipRot = true;
    private PlayerController player;
    private Animator anim;

    public UnityEvent e_OnDeath;

    private LevelManager _lvlManger;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        anim = GetComponent<Animator>();
        _lvlManger = FindObjectOfType<LevelManager>();
    }

    private void OnEnable()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = spawnPos.runtimeValue;
        anim.SetTrigger("Spawn");
    }

    // Update is called once per frame
    void Update()
    {
        ShowJumpIndicator();

        playerMat.SetFloat("_Fade", fade);
        anim.SetBool("Invulnerable", player.playerMovement.controller.Invulnerable);
        anim.SetBool("Alive", player.unit.IsAlive());
    }

    void ShowJumpIndicator()
    {
        if (Time.timeScale < 1)
        {
            if (player.playerMovement.joystickDirection != Vector2.zero)
                directionIndicator.gameObject.SetActive(true);
            else
                directionIndicator.gameObject.SetActive(false);
        }
        else
        {
            directionIndicator.gameObject.SetActive(false);
        }
        m_Angle = (float)(Mathf.Atan2(player.playerMovement.joystickDirection.x, player.playerMovement.joystickDirection.y) / Mathf.PI) * 180f;
        if (m_Angle < 0) m_Angle += 360f;
        m_Angle = flipRot ? -m_Angle : m_Angle;
        directionIndicator.rotation = Quaternion.Euler(new Vector3(0, 0, m_Angle));
    }

    public void OnSpawn()
    {
        player.playerMovement.controller.CanMove = true;
    }

    public void OnOrbCollected()
    {
        ps_OrbPickup.gameObject.SetActive(true);
        ps_OrbPickup.Stop();
        ps_OrbPickup.Play();
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
        _lvlManger.ReloadCurrentScene();
    }
}
