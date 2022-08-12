using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgress : MonoBehaviour
{
    public static LevelProgress singleton;

    int scenesCount { get { return 41; } } //min 10, max 41

    int[] levels;

    public static int currentLevel { get; private set; }

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();

        if (singleton == null) singleton = this;
        else Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

        currentLevel = 1;

        if (PlayerPrefs.HasKey("LevelsIsGenerated"))
        {
            if (PlayerPrefs.HasKey("currentLevel")) currentLevel = PlayerPrefs.GetInt("currentLevel");

            if(currentLevel < scenesCount)
            {
                int currentLevelIndex = PlayerPrefs.GetInt("GeneratedLevel " + currentLevel);
                SceneManager.LoadScene(currentLevelIndex);
            }
            else
            {
                ResetCurrentLevel();
                GenerateLevelsLine();
                SceneManager.LoadScene(currentLevel);
            }
        }
        else
        {
            GenerateLevelsLine();
            SceneManager.LoadScene(currentLevel);
        }
    }
    void GenerateLevelsLine()
    {
        levels = new int[scenesCount];

        for (int i = 1; i < 10; i++)
        {
            levels[i] = i;
        }

        for (int i = 1; i < (levels.Length / 10); i++)
        {
            int[] currentLevelChunk = new int[10];
            int[] mixLevels = new int[8];
            for (int j = 1; j < 9; j++)
            {
                currentLevelChunk[j] = (i * 10) + j;
                mixLevels[j - 1] = currentLevelChunk[j];
            }
            int[] alreadyMixedLevels = MixLevels(mixLevels);
            for (int j = 1; j < 9; j++)
            {
                currentLevelChunk[j] = alreadyMixedLevels[j - 1];
            }
            currentLevelChunk[0] = (i * 10);
            currentLevelChunk[9] = (i * 10) + 9;

            for (int j = 0; j < 10; j++)
            {
                levels[(i * 10) + j] = currentLevelChunk[j];
            }
        }

        levels[scenesCount - 1] = scenesCount - 1;

        for (int i = 0; i < levels.Length; i++)
        {
            PlayerPrefs.SetInt("GeneratedLevel " + i, levels[i]);
        }

        PlayerPrefs.SetInt("LevelsIsGenerated", 1);
    }
    int[] MixLevels(int[] levels)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            int currentLevel = levels[i];

            int randomIndex = Random.Range(i, levels.Length);

            levels[i] = levels[randomIndex];
            levels[randomIndex] = currentLevel;
        }
        return levels;
    }
    void ResetCurrentLevel()
    {
        currentLevel = 1;
        PlayerPrefs.SetInt("currentLevel", currentLevel);
    }
    public void GoToNextLevel()
    {
        currentLevel++;
        PlayerPrefs.SetInt("currentLevel", currentLevel);

        if (currentLevel < scenesCount)
        {
            int currentLevelIndex = PlayerPrefs.GetInt("GeneratedLevel " + currentLevel);
            SceneManager.LoadScene(currentLevelIndex);
        }
        else
        {
            ResetCurrentLevel();
            GenerateLevelsLine();
            SceneManager.LoadScene(currentLevel);
        }
    }
}
