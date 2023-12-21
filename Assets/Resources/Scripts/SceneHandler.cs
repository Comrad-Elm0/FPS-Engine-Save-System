using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour, IDataPersistence
{
    [SerializeField] string currentScene;

    public void LoadData(GameData gameData)
    {
        if (SceneManager.GetActiveScene().name == currentScene)
        {

        }
        else
        {
            SceneManager.LoadSceneAsync(gameData.currentScene);
        }
    }

    public void SaveData(GameData gameData)
    {

    }
}
