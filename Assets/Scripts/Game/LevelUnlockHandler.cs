using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUnlockHandler : MonoBehaviour
{
    static LevelUnlockHandler instance;
    private static object locker = new object();
    [SerializeField] Level[] levels; // An array of all the level objects
    [SerializeField] int maxStars = 3; // The maximum number of stars for each level
    public List<int> starClears = new List<int>(); // A list of star clears for each level
    public int unlockedLevelsNumber; // The number of levels that are unlocked
    public Sprite yellowStar; // The sprite for a yellow star
    const string GameProgressFile = "GameProgress.csv";

    protected LevelUnlockHandler()
    {
        
    }

    public static LevelUnlockHandler Instance
    {
        get
        {
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new LevelUnlockHandler();
                    }
                }
            }
            return instance;
        }
    }
    void Start()
    {
        GetGameProgress();
        ShowStars();
    }

    void ShowStars()
    {
        // Show the star ratings for all unlocked levels
        for (int levelCount = 0; levelCount < unlockedLevelsNumber; levelCount++)
        {
            levels[levelCount].levelButton.interactable = true;
            for (int starCount = 0; starCount < starClears[levelCount]; starCount++)
            {
                levels[levelCount].stars[starCount].sprite = yellowStar;
            }
        }
    }
    public void LevelCleared(int level, int starClear)
    {
        GetGameProgress();
        if (starClears[level - 1] < starClear) starClears[level-1] = starClear;
        if (level == unlockedLevelsNumber) unlockedLevelsNumber++;
        String filePath = Path.Combine(Application.streamingAssetsPath, GameProgressFile);
        SaveGameProgressFile(filePath);
    }

    public void GetGameProgress()
    {
        StreamReader input = null;
        String filePath = Path.Combine(Application.streamingAssetsPath, GameProgressFile);
        try
        {
            if (!File.Exists(filePath))
            {
                CreateGameProgressFile(filePath);
            }
            input = File.OpenText(filePath);
            string levels = input.ReadLine();
            string values = input.ReadLine();
            string levelsUnlocked = input.ReadLine();
            SetGameProgress(values, levelsUnlocked);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            if (input != null)
            {
                input.Close();
            }
        }
    }

    private void SetGameProgress(string csvValues, string csvLevelsUnlocked)
    {
        string[] values = csvValues.Split(',');
        foreach (string starClear in values)
        {
            starClears.Add(int.Parse(starClear));
        }
        char temp = csvLevelsUnlocked[0];
        unlockedLevelsNumber = int.Parse(temp.ToString());
    }

    private void CreateGameProgressFile(string filePath)
    {
        using (StreamWriter writer = File.CreateText(filePath))
        {
            writer.WriteLine("1,2,3,4");
            writer.WriteLine("0,0,0,0");
            writer.WriteLine("1");
        }
    }
    private void SaveGameProgressFile(string filePath)
    {
        string stars = string.Join(",", starClears);
        string csvContent = $"1,2,3,4\n{stars}\n{unlockedLevelsNumber}";
        File.WriteAllText(filePath, csvContent);
    }
}
