using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private static float temptimer = 1f;

    public GameObject RewardPanel;

    void Start()
    {

    }

    void Update()
    {
        bonuslevels();
    }

    public void bonuslevels()
    {
        if (SceneManager.GetActiveScene().name == "Bonus Level - 5" ||
            SceneManager.GetActiveScene().name == "Bonus Level - 10" ||
            SceneManager.GetActiveScene().name == "Bonus Level - 15" ||
            SceneManager.GetActiveScene().name == "Bonus Level - Infinity")
        {
            RewardPanel.SetActive(false);
            if (PlayerController.timeleft < 0f)
            {
                PlayerController.yellowchosen = false;
                PlayerController.bluechosen = false;
                PlayerController.chosenone = false;
                temptimer -= Time.deltaTime;
                if (temptimer < 0f)
                {
                    RewardPanel.SetActive(true);
                }
            }
        }
    }

    public void gamemenu()
    {
        checklevel();
        SceneManager.LoadScene("GameMenu");
    }
    public void invokegamemenu() => Invoke("gamemenu", 2f);


    public static void checklevel()
    {
        if (SceneManager.GetActiveScene().name == "Space Catch")
        {
            MenuController.levelpassed = 1;
            MenuController.xp += 360;
            MenuController.money += 400;
            MenuController.diamond += 10;
        }
        else if (SceneManager.GetActiveScene().name == "Jump Catch")
        {
            MenuController.levelpassed = 2;
            MenuController.xp += 180;
            MenuController.money += 600;
        }
        else if(SceneManager.GetActiveScene().name == "Morning Catch")
        {
            MenuController.levelpassed = 3;
            MenuController.xp += 180;
            MenuController.money += 800;
        }
        else if (SceneManager.GetActiveScene().name == "Park Catch")
        {
            MenuController.levelpassed = 4;
            MenuController.xp += 120;
            MenuController.money += 1000;
        }
        else if (SceneManager.GetActiveScene().name == "Bonus Level - 5")
        {
            MenuController.levelpassed = 5;
            MenuController.xp += 120;
            MenuController.diamond += 30;
        }
        else if (SceneManager.GetActiveScene().name == "Rock Scene")
        {
            MenuController.levelpassed = 6;
            MenuController.xp += 120;
            MenuController.money += 1500;
        }
        else if (SceneManager.GetActiveScene().name == "Maze Scene")
        {
            MenuController.levelpassed = 7;
            MenuController.xp += 90;
            MenuController.money += 2000;
        }
        else if (SceneManager.GetActiveScene().name == "Ring Scene")
        {
            MenuController.levelpassed = 8;
            MenuController.xp += 90;
            MenuController.money += 2500;
        }
        else if (SceneManager.GetActiveScene().name == "Fall Scene")
        {
            MenuController.levelpassed = 9;
            MenuController.xp += 90;
            MenuController.money += 3000;
        }
        else if (SceneManager.GetActiveScene().name == "Bonus Level - 10")
        {
            MenuController.levelpassed = 10;
            MenuController.xp += 90;
            MenuController.diamond += 50;
        }
        else if (SceneManager.GetActiveScene().name == "Forest Scene")
        {
            MenuController.levelpassed = 11;
            MenuController.xp += 60;
            MenuController.money += 4000;
        }
        else if (SceneManager.GetActiveScene().name == "Snow Scene")
        {
            MenuController.levelpassed = 12;
            MenuController.xp += 60;
            MenuController.money += 5000;
        }
        else if (SceneManager.GetActiveScene().name == "Ramp Scene")
        {
            MenuController.levelpassed = 13;
            MenuController.xp += 60;
            MenuController.money += 6000;
        }
        else if (SceneManager.GetActiveScene().name == "Last Scene")
        {
            MenuController.levelpassed = 14;
            MenuController.xp += 60;
            MenuController.money += 8000;
        }
        else if (SceneManager.GetActiveScene().name == "Bonus Level - 15")
        {
            MenuController.levelpassed = 15;
            MenuController.xp += 120;
            MenuController.diamond += 100;
            MenuController.money += 10000;
        }
        else if (SceneManager.GetActiveScene().name == "Bonus Level - Infinity")
        {
            MenuController.levelpassed = 15;
            MenuController.xp += 20;
            MenuController.money += 500;
        }
        PlayerPrefs.SetInt("Money", MenuController.money);
        PlayerPrefs.SetInt("XP", MenuController.xp);
        PlayerPrefs.SetInt("Diamond", MenuController.diamond);
        PlayerPrefs.SetInt("XPLevel", MenuController.xplevel);
        PlayerPrefs.SetInt("LevelPassed", MenuController.levelpassed);
    }
}
