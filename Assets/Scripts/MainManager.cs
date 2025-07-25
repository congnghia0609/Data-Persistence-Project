using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text BestScoreText;
    public Text ScoreText;
    public GameObject GameOverText;
    public GameObject GuideText;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        UpdateBestScoreText();
        GenBrickPrefab();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                GuideText.SetActive(false);
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public void GenBrickPrefab()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score: {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        GuideText.SetActive(true);
        // Update player score.
        DataManager.Instance.playerRecord.BestScore = m_Points;
        // Save new Record.
        if (DataManager.Instance.playerRecord.BestScore > DataManager.Instance.recordData.BestScore)
        {
            DataManager.Instance.recordData.BestName = DataManager.Instance.playerRecord.BestName;
            DataManager.Instance.recordData.BestScore = DataManager.Instance.playerRecord.BestScore;
            DataManager.Instance.SaveRecord();
            UpdateBestScoreText();
        }
    }

    public void UpdateBestScoreText()
    {
        if (DataManager.Instance.recordData.BestName.Length > 0 && DataManager.Instance.recordData.BestScore > 0)
        {
            BestScoreText.text = "Best Score: " + DataManager.Instance.recordData.BestName + ": " + DataManager.Instance.recordData.BestScore;
        }
        else
        {
            BestScoreText.text = "Best Score";
        }
    }
}
