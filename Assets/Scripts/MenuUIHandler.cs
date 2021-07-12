using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class MenuUIHandler : MonoBehaviour
{
    public InputField nameField;
    public static string daName;
    // Start is called before the first frame update
    void Start()
    {
        daName = "ZAZU";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartButtonPressed()
    {
        SceneManager.LoadScene(1);
    }

    public void NameEntered()
    {
      //  Debug.Log(DataHandler.Instance.bestPlayer);
      //  Debug.Log(DataHandler.Instance.bestScore);
      //  Debug.Log("Name Entered " + nameField.text);
       // Debug.Log("Best Player " + DataHandler.Instance.bestPlayer);
      //  Debug.Log("Best Score " + DataHandler.Instance.bestScore);
        daName = nameField.text.ToString();
      //   Debug.Log("daName " + daName);
        if (DataHandler.Instance.bestPlayer == "")
        {
            DataHandler.Instance.bestPlayer = daName;
            DataHandler.Instance.bestScore = 0;
            DataHandler.Instance.SaveBestPlayer();
            Debug.Log("IS NULL");
            Debug.Log("Best Player " + DataHandler.Instance.bestPlayer);
        }
    }

    public void Exit()
    {
         #if UNITY_EDITOR
           Debug.Log("Game Over");
           EditorApplication.ExitPlaymode();
         #else
            Application.Quit(); // original code to quit Unity player
         #endif
    }
}
