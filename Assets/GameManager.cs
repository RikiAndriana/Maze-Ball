using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text winText;
    [SerializeField] TMP_Text coinText;
    [SerializeField] TMP_Text pointText;
    [SerializeField] GameObject holeTrigger;
    [SerializeField] GameObject freezeObject;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject winPanel;
    [SerializeField] Inventory player;
    HoleTrigger trigger;
    FreezeItem freezeItem;
    float timer = 60;
    bool isOver = false;

    private void Start()
    {
        freezeItem = freezeObject.GetComponent<FreezeItem>();
        trigger = holeTrigger.GetComponent<HoleTrigger>();
    }

    void Update()
    {
        coinText.text = "Coin : " + player.coin + " / 3";
        if (trigger.IsGoal == false && isOver == false)
        {
            if (freezeItem.IsFreeze)
            {
                timerText.text = "~Freeze~ " + (int)timer;
                Invoke("Unfreeze", 4f);
                return;
            }
            timer -= Time.deltaTime;
            timerText.text = "Timer: " + (int)timer;

            if (timer <= 0)
            {
                gameOverPanel.SetActive(true);
                // isOver = true;
            }

            return;
        }

        winPanel.SetActive(true);
        if (isOver == false)
        {
            if (timer > 20 && player.coin == 3)
                pointText.text = "3";
            else if (timer > 10 && player.coin >= 2)
                pointText.text = "2";
            else if (timer > 0 || player.coin >= 2)
                pointText.text = "2";
            else
                pointText.text = "1";
            isOver = true;
        }
    }

    private void Unfreeze()
    {
        freezeItem.IsFreeze = false;
    }
}
