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

    public Text ScoreText;
    public Text bestText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;
    

    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("From Main " + MenuUIHandler.daName);
        ScoreText.text = MenuUIHandler.daName + " Score 0";
        bestText.text = "Best Score " + DataHandler.Instance.bestPlayer + " " + DataHandler.Instance.bestScore;
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = MenuUIHandler.daName + $" Score : {m_Points}";
    }

    public void GameOver()
    {
        
        m_GameOver = true;
        GameOverText.SetActive(true);
        if (m_Points > DataHandler.Instance.bestScore)
        {
            Debug.Log("Update Score");
            DataHandler.Instance.bestPlayer = MenuUIHandler.daName;
            DataHandler.Instance.bestScore = m_Points;
            DataHandler.Instance.SaveBestPlayer();
            bestText.text = "Best Score " + DataHandler.Instance.bestPlayer + " " + DataHandler.Instance.bestScore;
           Debug.Log("Gameover updated");
        }
    }
}
