using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text bestScoreText;
    [SerializeField] private TMP_InputField usernameInput;

    // Start is called before the first frame update
    void Start()
    {
        UpdateBestScoreText();
        usernameInput.text = "";
    }

    public void UpdateBestScoreText()
    {
        if (DataManager.Instance.recordData.BestName.Length > 0 && DataManager.Instance.recordData.BestScore > 0)
        {
            bestScoreText.text = "Best Score: " + DataManager.Instance.recordData.BestName + ": " + DataManager.Instance.recordData.BestScore;
        }
        else
        {
            bestScoreText.text = "Best Score";
        }
    }

    public void StartGame()
    {
        string playerName = usernameInput.text.Trim();
        if (playerName.Length > 0)
        {
            DataManager.Instance.playerRecord.BestName = playerName;
            SceneManager.LoadScene(1);
        }
    }

    public void OnChangeUsername(string newName)
    {
        usernameInput.text = newName.TrimStart();
    }
}
