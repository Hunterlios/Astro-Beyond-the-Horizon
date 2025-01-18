using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI highscore;

    void Start()
    {
        highscore.SetText(PlayerPrefs.GetInt("Highscore", 0).ToString());
        score.SetText(PlayerInventory.instance._resourcesCollected.ToString());

        if (PlayerInventory.instance._resourcesCollected > PlayerPrefs.GetInt("Highscore", 0))
        {
            PlayerPrefs.SetInt("Highscore", PlayerInventory.instance._resourcesCollected);
            highscore.SetText(PlayerInventory.instance._resourcesCollected.ToString());
        }
    }
}
