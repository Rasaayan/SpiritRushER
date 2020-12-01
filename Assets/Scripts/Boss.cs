using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public GameManager gameManObj;

    public BossSpawning bossSpawningObj;    

    const string NIGHT_TERROR = "Night Terror HP : ";
    const string CHRONO_WITCH = "Chrono Witch HP : ";
    const string ECTOPLASMIC_EMBALMER = "Ectoplasmic HP : ";

    public void Update()
    {
        int bossID = gameManObj.currentGameLevel - 1;

        if (bossID == 0)
        {
            bossSpawningObj.bossHealthText.text = NIGHT_TERROR + bossSpawningObj.bossHp;
        }
        else if (bossID == 1)
        {
            bossSpawningObj.bossHealthText.text = CHRONO_WITCH + bossSpawningObj.bossHp;
        }

        else if (bossID == 2)
        {
            bossSpawningObj.bossHealthText.text = ECTOPLASMIC_EMBALMER + bossSpawningObj.bossHp;
        }
    }

    public void TakeDamage(float damage)
    {
        bossSpawningObj.bossHp -= damage;

        if (gameManObj.currentGameLevel == 1 && bossSpawningObj.bossHp >= 0)
        {
            AudioManager.Instance.PlayClip("Embalmer_Hurt");
            AudioManager.Instance.PlayClip("Shoot");
        }
        else if (gameManObj.currentGameLevel == 2 && bossSpawningObj.bossHp >= 0)
        {
            AudioManager.Instance.PlayClip("ChronoWitch_Hurt");
        }
        else if (gameManObj.currentGameLevel == 3 && bossSpawningObj.bossHp >= 0)
        {
            AudioManager.Instance.PlayClip("Embalmer_Hurt");
        }

        if (bossSpawningObj.bossHp <= 0)
        {
            gameManObj.BossDefeated();
            bossSpawningObj.bossHealthText.text = "Boss Defeated";
        }
    }
}
