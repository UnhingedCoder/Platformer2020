using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAreaSpikeController : MonoBehaviour
{
    public List<RetractableSpikeVariantController> spikeList = new List<RetractableSpikeVariantController>();
    public int epicenterSpikeIndex = 0;

    public List<int> spikeLength = new List<int>();
    public int maxSpikes;
    public bool ShouldReleaseSpikeWave;
    public float delay;

    public float startDelay;
    public float startTime = 0f;
    public float waitDelay;

    public ParticleSystem ps_poweringUp;
    public ParticleSystem ps_Release;
    [SerializeField] private bool waveDirRight;
    [SerializeField]private int index;

    public bool poweringUp = false;

    bool canTriggerSpikeWave = true;

    public EnemyBossController boss;
    private void OnValidate()
    {
        //spikeList.Clear();
        //int i = 0;
        //foreach (Transform child in this.transform)
        //{
        //    RetractableSpikeVariantController _spike = child.GetComponent<RetractableSpikeVariantController>();
        //    _spike.id = i;
        //    spikeList.Add(_spike);
        //    i++;
        //}
    }

    private void Update()
    {
        if (ShouldReleaseSpikeWave)
        {
            StartCoroutine(ReleaseSpikeWave());
        }
    }

    IEnumerator ReleaseSpikeWave()
    {
        canTriggerSpikeWave = false;
        int spikeLength = spikeList.Count;
        if (waveDirRight)
        {
            spikeLength = epicenterSpikeIndex + maxSpikes;
            if (spikeLength > spikeList.Count)
                spikeLength = spikeList.Count;

            for (index = epicenterSpikeIndex; index < spikeLength; index++)
            {
                Debug.Log("Going through " + index + " Spike");
                spikeList[index].ShouldGrow = true;
                yield return new WaitForSeconds(delay);
            }
        }
        else
        {
            spikeLength = epicenterSpikeIndex - maxSpikes;
            if (spikeLength < 0)
                spikeLength = 0;

            for (index = epicenterSpikeIndex; index >= spikeLength; index--)
            {
                Debug.Log("Going through " + index + " Spike");
                spikeList[index].ShouldGrow = true;
                yield return new WaitForSeconds(delay);
            }
        }

        ResetSpikes();
    }

    public void ResetSpikes()
    {
        startTime = 0;
        ps_poweringUp.gameObject.SetActive(false);
        ps_Release.gameObject.SetActive(false);
        poweringUp = false;
        canTriggerSpikeWave = true;
        ShouldReleaseSpikeWave = false;
    }

    public void InitiateSpikeWave(int facingDir)
    {
        if (startTime <= startDelay)
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
                SetMaxSpikeLength();
                for (int i = 0; i < spikeList.Count; i++)
                {
                    spikeList[i].upTime = 0;
                    spikeList[i].retractingTime = 0;
                }

                if (facingDir > 0)
                    waveDirRight = true;
                else
                    waveDirRight = false;

                ps_Release.gameObject.SetActive(true);
                ps_Release.Stop();
                ps_Release.Play();

                ShouldReleaseSpikeWave = true;
                canTriggerSpikeWave = false;
            }
        }
    }

    void SetMaxSpikeLength()
    {
        switch (boss.stage)
        {
            case BossStage.Lvl1:
                {
                    maxSpikes = spikeLength[0];
                }
                break;
            case BossStage.Lvl2:
                {
                    maxSpikes = spikeLength[1];
                }
                break;
            case BossStage.Lvl3:
                {
                    maxSpikes = spikeLength[2];
                }
                break;
            case BossStage.Lvl4:
                {
                    maxSpikes = spikeLength[3];
                }
                break;
            case BossStage.Lvl5:
                {
                    maxSpikes = spikeList.Count;
                }
                break;
        }
    }

}
