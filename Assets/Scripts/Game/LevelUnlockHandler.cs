using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlockHandler : MonoBehaviour
{
    [SerializeField] Level[] levels; // An array of all the level objects
    [SerializeField] int maxStars = 3; // The maximum number of stars for each level
    List<int> starClears = new List<int>(); // A list of star clears for each level
    int unlockedLevelsNumber; // The number of levels that are unlocked
    int currentLevel; // The current level that the player is on
    public Sprite yellowStar; // The sprite for a yellow star

    void Start()
    {
        // Initialize the star clears list
        for (int i = 0; i < levels.Length; i++)
        {
            starClears.Add(0);
        }

        // Load the unlocked levels number and the star clears list from PlayerPrefs
        unlockedLevelsNumber = PlayerPrefs.GetInt("levelsUnlocked", 1);
        for (int i = 0; i < levels.Length; i++)
        {
            starClears[i] = PlayerPrefs.GetInt("level" + (i + 1) + "Stars", 0);
        }

        // Load the current level from PlayerPrefs
        currentLevel = PlayerPrefs.GetInt("currentLevel", 0);

        // Update the star clears list for the current level if necessary
        int index = currentLevel == 0 ? 0 : currentLevel - 1;
        int starClear = PlayerPrefs.GetInt("starClear", 0);
        if (starClears[index] < starClear)
        {
            starClears[index] = starClear;
            SaveStarClears();
        }

        // Check if the current level is the latest unlocked level
        if (currentLevel == unlockedLevelsNumber)
        {
            // Unlock the next level
            unlockedLevelsNumber++;

            // Add a new entry to the star clears list for the next level
            starClears.Add(0);

            // Save the changes to PlayerPrefs
            PlayerPrefs.SetInt("levelsUnlocked", unlockedLevelsNumber);
            SaveStarClears();
        }

        // Show the star ratings for all unlocked levels
        ShowStars();
    }

    void SaveStarClears()
    {
        // Save the star clears list to PlayerPrefs
        for (int i = 0; i < levels.Length; i++)
        {
            PlayerPrefs.SetInt("level" + (i + 1) + "Stars", starClears[i]);
        }
        PlayerPrefs.Save();
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
}
