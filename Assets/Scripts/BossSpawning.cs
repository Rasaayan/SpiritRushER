using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSpawning : MonoBehaviour
{
    public Vector3 bossPosOffset;
    public GameObject[] bossTemplates;
    
    
    public bool bossActive = false;


    public const float BOSS_MAX_HP = 100f;
    public float bossHp;


    public Transform player;

    public Text bossHealthText;

    GameObject boss;

    public GameManager gameManObj;

    bool linkBossMovement = false;


    const string NIGHT_TERROR = "Night Terror HP : ";
    const string CHRONO_WITCH = "Chrono Witch HP : ";
    const string ECTOPLASMIC_EMBALMER = "Ectoplasmic HP : ";


    void Start()
    {
        bossPosOffset.x = 0;
        bossPosOffset.y = 5;
        bossPosOffset.z = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (linkBossMovement == true && boss != null) // need to set false somewhere
        {
            boss.transform.position = player.transform.position + bossPosOffset;
        }
        if (gameManObj.bossActive == false && boss != null)
        {
            Destroy(boss);
        }
    }

    public void SpawnBoss()
    {
        int bossID = gameManObj.currentGameLevel - 1; // -1 so that level level 1 is position 0 in array 

        boss = Instantiate(bossTemplates[bossID]);

        bossTemplates[bossID].transform.position = player.transform.position + bossPosOffset;

        boss.transform.position = bossTemplates[bossID].transform.position;
        boss.SetActive(true);

        bossHp = BOSS_MAX_HP;

        if (bossID == 0)
        {
            bossHealthText.text = NIGHT_TERROR + bossHp;
            AudioManager.Instance.PlayClip("NightTerror_Laugh");
        }
        else if (bossID == 1)
        {
            bossHealthText.text = CHRONO_WITCH + bossHp;
            AudioManager.Instance.PlayClip("ChronoWitch_Laugh");
        }

        else if (bossID == 2)
        {
            bossHealthText.text = ECTOPLASMIC_EMBALMER + bossHp;
            AudioManager.Instance.PlayClip("Embalmer_Laugh");
        }

        linkBossMovement = true;
    }



}
