using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAreaSpikeController : MonoBehaviour
{
    public List<RetractableSpikeVariantController> retractableSpikeList = new List<RetractableSpikeVariantController>();
    public List<BossAreaFallSpikeController> fallSpikeList = new List<BossAreaFallSpikeController>();
    public int epicenterSpikeIndex = 0;

    public List<int> spikeLength = new List<int>();
    public bool ShouldReleaseSpikeWave;
    public float retractDelay;
    public float fallDelay;

    public float PowerupDelay;
    float startTime = 0f;

    public ParticleSystem ps_poweringUp;
    public ParticleSystem ps_Release;
    [SerializeField] Vector2 fallSpikeRange;
    bool waveDirRight;
    int index;

    int maxRetactableSpikes;
    int maxFallSpikes;

    public bool poweringUp = false;

    bool canTriggerSpikeWave = true;
    [SerializeField]bool isSpikesDropping = false;

    public EnemyBossController boss;
    private void OnValidate()
    {
        //retractableSpikeList.Clear();
        //int i = 0;
        //foreach (Transform child in this.transform)
        //{
        //    RetractableSpikeVariantController _spike = child.GetComponent<RetractableSpikeVariantController>();
        //    _spike.id = i;
        //    retractableSpikeList.Add(_spike);
        //    i++;
        //}

        //fallSpikeList.Clear();
        //foreach (Transform child in this.transform)
        //{
        //    if(child.GetComponent<BossAreaFallSpikeController>() != null)
        //        fallSpikeList.Add(child.GetComponent<BossAreaFallSpikeController>());
        //}
    }

    private void Update()
    {
        if (ShouldReleaseSpikeWave)
        {
            StartCoroutine(ReleaseRetractableSpikeWave());
            if (boss.stage > BossStage.Lvl2 && !isSpikesDropping)
                StartCoroutine(ReleaseFallSpikeWave());
        }
    }

    IEnumerator ReleaseRetractableSpikeWave()
    {
        canTriggerSpikeWave = false;
        int spikeLength = retractableSpikeList.Count;
        if (waveDirRight)
        {
            spikeLength = epicenterSpikeIndex + maxRetactableSpikes;
            if (spikeLength > retractableSpikeList.Count)
                spikeLength = retractableSpikeList.Count;

            for (index = epicenterSpikeIndex; index < spikeLength; index++)
            {
                retractableSpikeList[index].ShouldGrow = true;
                yield return new WaitForSeconds(retractDelay);
            }
        }
        else
        {
            spikeLength = epicenterSpikeIndex - maxRetactableSpikes;
            if (spikeLength < 0)
                spikeLength = 0;

            for (index = epicenterSpikeIndex; index >= spikeLength; index--)
            {
                retractableSpikeList[index].ShouldGrow = true;
                yield return new WaitForSeconds(retractDelay);
            }
        }

        ResetRetractableSpikes();
    }

    IEnumerator ReleaseFallSpikeWave()
    {
        isSpikesDropping = true;
        
        for (int i = (int)fallSpikeRange.x; i < fallSpikeRange.y; i++)
        {
            fallSpikeList[i].DropTheSpike();
            yield return new WaitForSeconds(fallDelay);
        }

        isSpikesDropping = false;
    }

    public void ResetRetractableSpikes()
    {
        startTime = 0;
        ps_poweringUp.gameObject.SetActive(false);
        ps_Release.gameObject.SetActive(false);
        poweringUp = false;
        canTriggerSpikeWave = true;
        ShouldReleaseSpikeWave = false;
    }

    void ResetAllFallSpikes()
    {
        for (int i = 0; i < fallSpikeList.Count; i++)
        {
            fallSpikeList[i].Reset();
        }
    }

    public void InitiateSpikeWave(int facingDir)
    {
        if (startTime <= PowerupDelay)
        {
            poweringUp = true;
            ps_poweringUp.gameObject.SetActive(true);
            startTime += Time.deltaTime;
        }
        else
        {
            ps_poweringUp.gameObject.SetActive(false);
            if (canTriggerSpikeWave)
            {
                SetMaxRetractableSpikeLength();
                for (int i = 0; i < retractableSpikeList.Count; i++)
                {
                    retractableSpikeList[i].upTime = 0;
                    retractableSpikeList[i].retractingTime = 0;
                }

                if (facingDir > 0)
                    waveDirRight = true;
                else
                    waveDirRight = false;

                ps_Release.gameObject.SetActive(true);
                ps_Release.Stop();
                ps_Release.Play();
                
                SetMaxFallSpikeLength();
                GetFallSpikeCluster();

                canTriggerSpikeWave = false;
                ShouldReleaseSpikeWave = true;
            }
        }
    }

    void SetMaxRetractableSpikeLength()
    {
        switch (boss.stage)
        {
            case BossStage.Lvl1:
                {
                    maxRetactableSpikes = spikeLength[0];
                }
                break;
            case BossStage.Lvl2:
                {
                    maxRetactableSpikes = spikeLength[1];
                }
                break;
            case BossStage.Lvl3:
                {
                    maxRetactableSpikes = spikeLength[2];
                }
                break;
            case BossStage.Lvl4:
                {
                    maxRetactableSpikes = spikeLength[3];
                }
                break;
            case BossStage.Lvl5:
                {
                    maxRetactableSpikes = retractableSpikeList.Count;
                }
                break;
        }
    }

    void SetMaxFallSpikeLength()
    {
        switch (boss.stage)
        {
            case BossStage.Lvl1:
            case BossStage.Lvl2:
            case BossStage.Lvl3:
                {
                    maxFallSpikes = 1;
                }
                break;
            case BossStage.Lvl4:
                {
                    maxFallSpikes = 2;
                }
                break;
            case BossStage.Lvl5:
                {
                    maxFallSpikes = 3;
                }
                break;
        }
    }


    int GetFallSpikeEpicenter()
    {
        int index = -1;

        for (int i = 0; i < fallSpikeList.Count; i++)
        {
            if (fallSpikeList[i].isPlayerUnder)
                index = i;

        }

        return index;
    }

    void GetFallSpikeCluster()
    {
        fallSpikeRange = Vector2.zero;

        int fallSpikeEpicenter = GetFallSpikeEpicenter();
        Debug.Log("Center: " + fallSpikeEpicenter);

        int startIndex = fallSpikeEpicenter - maxFallSpikes;
        if (startIndex < 0)
            startIndex = 0;

        int endIndex = fallSpikeEpicenter + maxFallSpikes;
        if (endIndex > fallSpikeList.Count)
            endIndex = fallSpikeList.Count;

        fallSpikeRange = new Vector2(startIndex, endIndex);
    }
}
