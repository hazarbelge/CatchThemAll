using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Button Multi1, Multi2, Multi3, Multi4;
    public static int levelpassed = 0;
    public static int money = 300;
    public static int diamond = 10;
    public static int xplevel = 1;
    public static int xp = 0;

    public GameObject panel1;
    public Text levelnow, levelnext, moneytext, diamondtext, xpleveltext;
    public Slider xpslider;

    void Start()
    {
        Multi1.interactable = false;
        Multi2.interactable = false;
        Multi3.interactable = false;
        Multi4.interactable = false;
        levelpassed = PlayerPrefs.GetInt("LevelPassed");
        if (levelpassed == 0)
        {
            PlayerPrefs.SetInt("Money", money);
            PlayerPrefs.SetInt("XP", xp);
            PlayerPrefs.SetInt("Diamond", diamond);
            PlayerPrefs.SetInt("XPLevel", xplevel);
            PlayerPrefs.SetInt("LevelPassed", levelpassed);
        }
        else
        {
            money = PlayerPrefs.GetInt("Money");
            xp = PlayerPrefs.GetInt("XP");
            diamond = PlayerPrefs.GetInt("Diamond");
            xplevel = PlayerPrefs.GetInt("XPLevel");
            panel1.gameObject.SetActive(false);
        }
        if (money >= 1000) Multi1.interactable = true;
        if (money >= 5000) Multi2.interactable = true;
        if (money >= 30000) Multi3.interactable = true;
        if (money >= 250000) Multi4.interactable = true;
        if (levelpassed == 0)
        {
            levelnow.text = " 1";
            levelnext.text = " 2";
        }
        if (levelpassed == 1)
        {
            levelnow.text = " 2";
            levelnext.text = " 3";
        }
        if (levelpassed == 2)
        {
            levelnow.text = " 3";
            levelnext.text = " 4";
        }
        if (levelpassed == 3)
        {
            levelnow.text = " 4";
            levelnext.text = " 5";
        }
        if (levelpassed == 4)
        {
            levelnow.text = " 5";
            levelnext.text = " 6";
        }
        if (levelpassed == 5)
        {
            levelnow.text = " 6";
            levelnext.text = " 7";
        }
        if (levelpassed == 6)
        {
            levelnow.text = " 7";
            levelnext.text = " 8";
        }
        if (levelpassed == 7)
        {
            levelnow.text = " 8";
            levelnext.text = " 9";
        }
        if (levelpassed == 8)
        {
            levelnow.text = " 9";
            levelnext.text = "10";
        }
        if (levelpassed == 9)
        {
            levelnow.text = "10";
            levelnext.text = "11";
        }
        if (levelpassed == 10)
        {
            levelnow.text = "11";
            levelnext.text = "12";
        }
        if (levelpassed == 11)
        {
            levelnow.text = "12";
            levelnext.text = "13";
        }
        if (levelpassed == 12)
        {
            levelnow.text = "13";
            levelnext.text = "14";
        }
        if (levelpassed == 13)
        {
            levelnow.text = "14";
            levelnext.text = "15";
        }
        if (levelpassed == 14)
        {
            levelnow.text = "15";
            levelnext.text = "∞";
        }
        if (levelpassed == 15)
        {
            levelnow.text = "∞";
            levelnext.text = "∞";
        }
    }

    void Update()
    {
        moneytext.text = "" + money;
        diamondtext.text = " " + diamond;
        xpleveltext.text = "" + xplevel;
        if (xp >= 360) xp = 360;
        xpslider.value = xp;
        if(xp == 360)
        {
            xplevel++;
            xp = 0;
        }
    }

    public static void reward()
    {
        money += 1000;
        PlayerPrefs.SetInt("Money", money);
    }

    public void multiplayer()
    {
        GameSetupController.multilevel = 1;
        money -= 1000;
        PlayerPrefs.SetInt("Money", money);
        SceneManager.LoadScene("MultiplayerMenu");
    }
    public void multiplayer2()
    {
        GameSetupController.multilevel = 2;
        money -= 5000;
        PlayerPrefs.SetInt("Money", money);
        SceneManager.LoadScene("MultiplayerMenu");
    }
    public void multiplayer3()
    {
        GameSetupController.multilevel = 3;
        money -= 30000;
        PlayerPrefs.SetInt("Money", money);
        SceneManager.LoadScene("MultiplayerMenu");
    }
    public void multiplayer4()
    {
        GameSetupController.multilevel = 4;
        money -= 250000;
        PlayerPrefs.SetInt("Money", money);
        SceneManager.LoadScene("MultiplayerMenu");
    }
    public void multiplayer5()
    {
        GameSetupController.multilevel = 5;
        SceneManager.LoadScene("MultiFriendsMenu");
    }

    public void singleplayer()
    {
        if (levelpassed == 0)
        {
            LevelI();
        }
        else if (levelpassed == 1)
        {
            LevelII();
        }
        else if (levelpassed == 2)
        {
            LevelIII();
        }
        else if (levelpassed == 3)
        {
            LevelIV();
        }
        else if (levelpassed == 4)
        {
            LevelV();
        }
        else if (levelpassed == 5)
        {
            LevelVI();
        }
        else if (levelpassed == 6)
        {
            LevelVII();
        }
        else if (levelpassed == 7)
        {
            LevelVIII();
        }
        else if (levelpassed == 8)
        {
            LevelIX();
        }
        else if (levelpassed == 9)
        {
            LevelX();
        }
        else if (levelpassed == 10)
        {
            LevelXI();
        }
        else if (levelpassed == 11)
        {
            LevelXII();
        }
        else if (levelpassed == 12)
        {
            LevelXIII();
        }
        else if (levelpassed == 13)
        {
            LevelXIV();
        }
        else if (levelpassed == 14)
        {
            LevelXV();
        }
        else LevelInfinity();
    }

    public void LevelI()
    {
        SceneManager.LoadScene("Space Catch");
        PlayerController.timeleft = 10.5f;
        PlayerController.runspeed = 4f;
        PlayerController.jumpForce = 4f;
    }
    public void LevelII()
    {
        SceneManager.LoadScene("Jump Catch");
        PlayerController.timeleft = 10.5f;
        PlayerController.timeleftcount = 0;
        PlayerController.runspeed = 4f;
        PlayerController.jumpForce = 6f;
    }
    public void LevelIII()
    {
        SceneManager.LoadScene("Morning Catch");
        PlayerController.timeleft = 12.5f;
        PlayerController.timeleftcount = 0;
        PlayerController.runspeed = 5f;
        PlayerController.jumpForce = 4f;
    }
    public void LevelIV()
    {
        SceneManager.LoadScene("Park Catch");
        PlayerController.timeleft = 15.5f;
        PlayerController.timeleftcount = 0;
        PlayerController.runspeed = 7f;
        PlayerController.jumpForce = 3f;
    }
    public void LevelV()
    {
        SceneManager.LoadScene("Bonus Level - 5");
        PlayerController.timeleft = 20.5f;
        PlayerController.timeleftcount = 0;
        PlayerController.runspeed = 10f;
        PlayerController.jumpForce = 3.5f;
    }
    public void LevelVI()
    {
        SceneManager.LoadScene("Rock Scene");
        PlayerController.chosenone = true;
        PlayerController.timeleft = 8f;
        PlayerController.timeleftcount = 0;
        PlayerController.runspeed = 6f;
        PlayerController.jumpForce = 4f;
    }
    public void LevelVII()
    {
        SceneManager.LoadScene("Maze Scene");
        PlayerController.timeleft = 12f;
        PlayerController.timeleftcount = 0;
        PlayerController.runspeed = 7f;
        PlayerController.jumpForce = 2.5f;
    }
    public void LevelVIII()
    {
        SceneManager.LoadScene("Ring Scene");
        PlayerController.timeleft = 15f;
        PlayerController.timeleftcount = 0;
        PlayerController.runspeed = 9f;
        PlayerController.jumpForce = 5f;
    }
    public void LevelIX()
    {
        SceneManager.LoadScene("Fall Scene");
        PlayerController.timeleft = 8f;
        PlayerController.timeleftcount = 0;
        PlayerController.runspeed = 5f;
        PlayerController.jumpForce = 4f;
    }
    public void LevelX()
    {
        SceneManager.LoadScene("Bonus Level - 10");
        PlayerController.timeleft = 20.5f;
        PlayerController.timeleftcount = 0;
        PlayerController.runspeed = 10f;
        PlayerController.jumpForce = 3.5f;
    }
    public void LevelXI()
    {
        SceneManager.LoadScene("Forest Scene");
        PlayerController.chosenone = true;
        PlayerController.timeleft = 7f;
        PlayerController.timeleftcount = 0;
        PlayerController.runspeed = 6.5f;
        PlayerController.jumpForce = 4f;
    }
    public void LevelXII()
    {
        SceneManager.LoadScene("Snow Scene");
        PlayerController.timeleft = 7f;
        PlayerController.timeleftcount = 0;
        PlayerController.runspeed = 7f;
        PlayerController.jumpForce = 4f;
    }
    public void LevelXIII()
    {
        SceneManager.LoadScene("Ramp Scene");
        PlayerController.timeleft = 7f;
        PlayerController.timeleftcount = 0;
        PlayerController.runspeed = 7f;
        PlayerController.jumpForce = 4f;
    }
    public void LevelXIV()
    {
        SceneManager.LoadScene("Last Scene");
        PlayerController.timeleft = 10.5f;
        PlayerController.timeleftcount = 0;
        PlayerController.runspeed = 6f;
        PlayerController.jumpForce = 4f;
    }
    public void LevelXV()
    {
        SceneManager.LoadScene("Bonus Level - 15");
        PlayerController.timeleft = 20.5f;
        PlayerController.timeleftcount = 0;
        PlayerController.runspeed = 10f;
        PlayerController.jumpForce = 3.5f;
    } 
    public void LevelInfinity()
    {
        SceneManager.LoadScene("Bonus Level - Infinity");
        PlayerController.timeleft = 10f;
        PlayerController.timeleftcount = 0;
        PlayerController.runspeed = 10f;
        PlayerController.jumpForce = 3.5f;
    }


}
