using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class GameUI : MonoBehaviour {

    [SerializeField]
    TextMeshProUGUI score;

    private void Start()
    {
        if (score == null)
        {
            Debug.LogError("No Score Text assigned to: " + gameObject.name + "\n Therefor to prevent errors the script will be destroyed!");
            Destroy(this);
            return;
        }

        score.text = "0000";
    }

    public void AddToScore (int points)
    {
        int currentScore;
        currentScore = Convert.ToInt32(score.text);
        currentScore += points;
        score.text = currentScore.ToString();
    }

}
