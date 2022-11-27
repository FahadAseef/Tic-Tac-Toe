using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    int playcount=0;
    public GameObject[] cells;
    public Text notificationtext;
    bool isgameover;
    public Text turntext;
    string machinetag = "o";
    string playertag = "x";
    bool ismachinepressed=false;


    private void Start()
    {
        turntext.text = "player 1s turn(x)";    
    }


    public void cellclick(int cellno)
    {
        if (!isgameover)
        {

            playcount++;
            GameObject currentobject = cells[cellno];
            //currentobject.transform.GetChild(0).gameObject.SetActive(true);


            if (playcount % 2 == 0)
            {

                currentobject.transform.GetChild(0).GetComponent<Text>().text = "o";
                currentobject.tag = "o";
            }
            else
            {
                currentobject.transform.GetChild(0).GetComponent<Text>().text = "x";
                currentobject.tag = "x";
            }
            checkwinlostdraw(currentobject.tag);
            currentobject.GetComponent<Button>().interactable = false;

            if (mainmenumanager.issingleplayer && currentobject.tag!=machinetag && !isgameover)
            {
                ismachinepressed = false;
                machineplay();
            }
        }
    }

    void cellchecker(int firstobj, int secondobj, int thirdobj ,string reqtag)
    {
        if (!isgameover)
        {

            bool iswin = false;
            int cellno = 0;

            if (cells[firstobj].tag == reqtag && cells[secondobj].tag == reqtag && cells[thirdobj].tag == "Untagged")
            {
                iswin = true;
                cellno = thirdobj;
            }
            if (cells[firstobj].tag == reqtag && cells[secondobj].tag == "Untagged" && cells[thirdobj].tag == reqtag)
            {
                iswin = true;
                cellno = secondobj;
            }
            if (cells[firstobj].tag == "Untagged" && cells[secondobj].tag == reqtag && cells[thirdobj].tag == reqtag)
            {
                iswin = true;
                cellno = firstobj;
            }
            if (iswin)
            {
                if (!ismachinepressed)
                {
                    cellclick(cellno);
                    ismachinepressed = true;
                }
            }
        }
    }

    void machineplay()
    {
       ismachinepressed = false;

        for (int i = 0; i < 2; i++)
        {

            string currenttag = "";
            if (i == 0)
            {
                currenttag = machinetag;
            }
            else
            {
                currenttag=playertag;
            }
            //block for win contition;

            cellchecker(0, 1, 2, currenttag);
            cellchecker(3, 4, 5, currenttag);
            cellchecker(6, 7, 8, currenttag);

            cellchecker(0, 3, 6, currenttag);
            cellchecker(1, 4, 7, currenttag);
            cellchecker(2, 5, 8, currenttag);

            cellchecker(0, 4, 8, currenttag);
            cellchecker(2, 4, 6, currenttag);
        }

        //block for random;

        while (!ismachinepressed)
        {
            int randomcellno = Random.Range(0, 9);
            if (cells[randomcellno].tag == "Untagged")
            {
                cellclick(randomcellno);
                ismachinepressed=true;
                break;
            }
        }

        
    }

    void checkwinlostdraw(string tagstring)
    {

        bool iswin=false;

        //checking for horinzontal wins;  

        for (int i = 0; i <= 6; i += 3)
        {

            if(cells[i].tag == tagstring && cells[i+1].tag == tagstring && cells[i+2].tag == tagstring)
            {
                winninganimation(cells[i], cells[i + 1], cells[i + 2]);
                iswin =true;           
                break;
            }

        }

        //checking for vertical wins;

        for (int i = 0; i < 3  ; i++)
        {

            if (cells[i].tag == tagstring && cells[i + 3].tag == tagstring && cells[i + 6].tag == tagstring)
            {
                winninganimation(cells[i], cells[i + 3], cells[i + 6]);
                iswin = true;
                break;
            }

        }

        //checking for diagonel wins

        if  (cells[0].tag == tagstring && cells[4].tag == tagstring && cells[8].tag == tagstring)
        {
            winninganimation(cells[0], cells[4], cells[8]);
            iswin = true;
           
        }

        if (cells[2].tag == tagstring && cells[4].tag == tagstring && cells[6].tag == tagstring)
        {
            winninganimation(cells[2], cells[4], cells[6]);
            iswin = true; 
        }

            if (iswin)
        {
            turntext.text = null;
            notificationtext.text = tagstring + " wins";
            isgameover = true;
        }

        if (!iswin && playcount==9)
        {
            turntext.text=null;
            notificationtext.text = "draw";
            isgameover = true;
        }

        if(!iswin && playcount != 9)
        {
            if (tagstring == "o")
            {
                turntext.text = "player 1s turn(x)";
            }
            if (tagstring == "x")
            {
                turntext.text = "player 2s turn(o)";
            }
        }


    }

    private void winninganimation(GameObject gameObject1, GameObject gameObject2, GameObject gameObject3)
    {
        gameObject1.GetComponent<Animator>().enabled= true;
        gameObject2.GetComponent<Animator>().enabled = true;
        gameObject3.GetComponent<Animator>().enabled = true;
    }

    public void restart()
    {
        SceneManager.LoadScene("gameplay");
    }

    public void backbuttonpressed()
    {
        SceneManager.LoadScene("mainscene");
    }


}
