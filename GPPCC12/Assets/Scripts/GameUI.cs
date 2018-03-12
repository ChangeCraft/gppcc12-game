using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {

    [SerializeField]
    TextMeshProUGUI score;
    [SerializeField]
    GameObject gameOverUI;

    private void Start()
    {
        if (score == null || gameOverUI == null)
        {
            Debug.LogError("Empty SerializeFilds: " + gameObject.name + "\n Therefor to prevent errors the script will be destroyed!");
            Destroy(this);
            return;
        }

        gameOverUI.SetActive(false);
        score.text = "0000";
    }

    public void AddToScore (int points)
    {
        int currentScore;
        currentScore = Convert.ToInt32(score.text);
        currentScore += points;
        score.text = currentScore.ToString();
    }

    public void ReloadScene ()
    {
        //SceneManager.UnloadScene();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToMenu ()
    {
        SceneManager.LoadScene(0);
    }

    public void EnableGameOverUI ()
    {
        gameOverUI.SetActive(true);
    }
}
