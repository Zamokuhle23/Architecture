using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonModel : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene(3);
    }
    public void ExitGame()
    {
        DBManager.LogOut();
        SceneManager.LoadScene(0);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(4);
    }
    public void GoToShop()
    {
        SceneManager.LoadScene(5);
    }
}
