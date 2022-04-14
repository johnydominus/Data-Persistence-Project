using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;
    public float speed = 2.0f;
    public int bricks = 0;
    public string name;
    public int score;

    public Text ScoreText;
    public Text BestScoreText;
    public Tuple<string, int> bestScore;
    public GameObject StartGameMessage;
    public GameObject GameOverMessage;
    public GameObject WinMessage;
    
    private bool m_Started = false;
    private bool m_GameOver = false;
    private bool m_Win = false;
    private bool scoreAdded = false;

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        speed *= DataHolder.dataHolder.difficulty;
        
        DataHolder.dataHolder.SetColor();
        bestScore = GetBestScore();
        BestScoreText.text = "Best Score: " + bestScore.Item1 + " " + bestScore.Item2;
        name = DataHolder.dataHolder.playersNameAndScore.Item1;
        score = DataHolder.dataHolder.playersNameAndScore.Item2;
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
                bricks++;
            }
        }
    }

    private void Update()
    {
        CheckBestScore();
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartGameMessage.SetActive(false);
                m_Started = true;
                float randomDirection = UnityEngine.Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * speed, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            UpdateBestScore();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
        }
        else if (m_Win)
        {
            UpdateBestScore();
            Ball.detectCollisions = false;
            WinMessage.SetActive(true);
            DataHolder.dataHolder.SortScores();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }            
        }
    }

    void AddPoint(int point)
    {
        score += point * DataHolder.dataHolder.difficulty;
        ScoreText.text = $"Score : {score}";
        if(--bricks == 0)
        {
            m_Win = true;
        }
    }

    public void GameOver()
    {
        m_GameOver = true;
        DataHolder.dataHolder.SortScores();
        GameOverMessage.SetActive(true);
    }

    void CheckBestScore()
    {
        if(score > bestScore.Item2)
        {
            BestScoreText.text = "Best Score: " + name + " " + score;
        }
    }

    public void UpdateBestScore()
    {
        if(PassToScores())
        {
            DataHolder.dataHolder.scores.Add(new Tuple<string, int>(name, score));
            DataHolder.dataHolder.SortScores();
            if(DataHolder.dataHolder.scores.Count > 10)
            {
                DataHolder.dataHolder.scores.RemoveAt(DataHolder.dataHolder.scores.Count - 1);
            }
            scoreAdded = true;
        }
    }

    Tuple<string, int> GetBestScore()
    {
        if(DataHolder.dataHolder.scores.Count > 0)
        {
            DataHolder.dataHolder.SortScores();
            return DataHolder.dataHolder.scores[0];
        }
        return new Tuple<string, int>("---", 0);
    }

    bool PassToScores()
    {
        if(!scoreAdded){
            if(DataHolder.dataHolder.scores.Count < 10)
            {
                return true;
            }
            if(DataHolder.dataHolder.scores[DataHolder.dataHolder.scores.Count - 1].Item2 < score)
            {
                return true;
            }
        }
        return false;
    }
}
