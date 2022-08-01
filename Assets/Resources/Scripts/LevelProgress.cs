using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgress : MonoBehaviour
{
    public static LevelProgress singleton;

    public int level { get; private set; }

    private void Awake()
    {
        if (singleton == null) singleton = this;
        else Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

        level = 1;
        if (PlayerPrefs.HasKey("level")) level = PlayerPrefs.GetInt("level");

        if (SceneManager.GetActiveScene().buildIndex == 0) SceneManager.LoadScene(level);
    }
    public void GoToNextLevel()
    {
        level++;
        PlayerPrefs.SetInt("level", level);
        SceneManager.LoadScene(level);
    }
}
