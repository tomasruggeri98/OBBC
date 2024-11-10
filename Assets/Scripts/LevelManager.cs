using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    

   public void Level(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
