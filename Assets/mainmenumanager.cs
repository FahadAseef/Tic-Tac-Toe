using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenumanager : MonoBehaviour
{
    public static bool issingleplayer = false;

   public void multiplayer()
    {
        issingleplayer=false;
        SceneManager.LoadScene("gameplay");
    }

    public void singleplayer()
    {
        issingleplayer=true;
        SceneManager.LoadScene("gameplay");
    }


}
