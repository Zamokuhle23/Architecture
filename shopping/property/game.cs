using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using SimpleJSON;
using System;
using UnityEngine.SceneManagement;

public class game : MonoBehaviour
{
    //public TMP_Text playerDisplay;
    public TMP_Text sumDisplay;
    public int balance;
    public string title;
    public string description;
    public shop_item[] shopItemSO;
    public shop_template[] shopPanels;
    public GameObject[] panels;
    public Button[] purchaseButton;
    public Button[] sellButton;
    public int number;
    tempObject[] items = new tempObject[5];




    private void Awake()
    {
 

        StartCoroutine(FetchTable());

/*        for (int i = 0; i < number; i++)
        {
            panels[i].SetActive(true);
        }*/

        //LoadPanels();
        purchasable();  
        if (DBManager.username == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        //playerDisplay.text = "Player: " + DBManager.username;
        sumDisplay.text = "Balance: " + DBManager.sum;

        
    }

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


    IEnumerator FetchTable()
    {
        Debug.Log("star ");
        //using (UnityWebRequest www = UnityWebRequest.Get("http://127.0.0.1/sqlconnect/fetchData.php"))
        //{
        WWW www = new WWW("http://127.0.0.1/sqlconnect/fetchData.php");
        yield return www;
            
            //Debug.Log("just " + json);
            if (string.IsNullOrEmpty(www.error))
            {
            //Debug.Log("error " + www.error);
            string json = www.text;
            Debug.Log("just " + json);
        }
            //else
            //{
                //if (www.isDone)
                //{
                    Debug.Log("Not Error");
                    //if(www.isDone)
                    //{
                    //JSONNode jsonData = JSON.Parse(System.Text.Encoding.UTF8.GetString(www.downloadHandler.data));
                    //string js = www.downloadHandler.text;
                    Debug.Log("okkk " + www.text);
                    JSONNode jsonData = SimpleJSON.JSON.Parse(www.text);
                    //JSONArray jsonData = SimpleJSON.JSON.Parse(www.downloadHandler.text) as JSONArray;
                    number = jsonData.Count;
                    Debug.Log("ahaaa ");

                    if (jsonData == null)
                    {

                        Debug.Log("........No Data.......");

                    }
                    else
                    {
                        Debug.Log("........Json Data.......");
                        //print(jsonData.Count);
                        //Debug.Log(jsonData[0].AsObject["name"]);
                        LoadPanels(jsonData);
                        //Debug.Log("tested " + jsonData[0]);


                    }

    }


    public void CallSaveData()
    {
        StartCoroutine(SaveData());
    }

    public void purchase(int btnNo)
    {

        
        string intValue = items[btnNo].cost;

        Debug.Log(intValue);
        DBManager.sum = DBManager.sum - int.Parse(intValue.Trim('"'));
        items[btnNo].own = true;
     
        sumDisplay.text = "Balance: " + DBManager.sum;
        purchasable();
        //Debug.Log(convertToInt(shopItemSO[btnNo].cost).GetType());
        //Debug.Log(shopItemSO[btnNo].cost.GetType());
        Debug.Log(DBManager.sum);
        Debug.Log(sumDisplay.text);
    }

    public void sell(int btnNo)
    {
        //Debug.Log("Test Sondos " + shopItemSO[btnNo].cost);
        //Debug.Log("Test Sondos " + items[btnNo].cost);

        System.Random rd = new System.Random();
        int rand_num = rd.Next(-100, 200);

        DBManager.sum = DBManager.sum + rand_num + int.Parse(items[btnNo].cost.Trim('"'));
        items[btnNo].own = false;
       
        sumDisplay.text = "Balance: " + DBManager.sum;
        purchasable();
        //Debug.Log(convertToInt(items[btnNo].cost) + " Cost of Item");
        for (int i = 0; i < 5; i++)
        {
            Debug.Log(items[i].cost + " Cost of Item");
        }
    }


    public void LoadPanels(JSONNode jsonData)
    {
        number = jsonData.Count;

        for (int i = 0;i < jsonData.Count;i++)
        {
            panels[i].SetActive(true);


            shopPanels[i].title.text = jsonData[i].AsObject["name"];
            shopPanels[i].description.text = jsonData[i].AsObject["description"];
            shopPanels[i].cost.text = "$ " + jsonData[i].AsObject["cost"];

            bool have;

            if(jsonData[i].AsObject["player"].ToString().Trim('"') == DBManager.username)
            {
                shopItemSO[i].own = true;
                have = true;
            }
            else
            {
                shopItemSO[i].own = false;
                have = false;
            }
            shopItemSO[i].player_id = jsonData[i].AsObject["player"].ToString();
            shopItemSO[i].cost = jsonData[i].AsObject["cost"].ToString();
            shopItemSO[i].description = jsonData[i].AsObject["description"];
            shopItemSO[i].title = jsonData[i].AsObject["name"];

            

            tempObject temp = new tempObject(jsonData[i].AsObject["name"], jsonData[i].AsObject["description"], jsonData[i].AsObject["cost"].ToString(), jsonData[i].AsObject["player"].ToString(),have);

            items[i] = temp;








        }
        purchasable();

        
    }
    

    public void purchasable()
    {

        for (int i = 0; i < number; i++)
        {

          if(items[i].own)
            {
                Debug.Log(items[i].own);
                sellButton[i].gameObject.SetActive(true);
                purchaseButton[i].gameObject.SetActive(false);

            }
          else
            {
                Debug.Log(items[i].own);
                sellButton[i].gameObject.SetActive(false);
                purchaseButton[i].gameObject.SetActive(true);
                if (DBManager.sum >= int.Parse(items[i].cost.Trim('"')))
                    purchaseButton[i].interactable = true;
                else
                    purchaseButton[i].interactable = false;
            }

        }
    }
    

    IEnumerator SaveData()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", DBManager.username);
        form.AddField("sum", DBManager.sum);
        //form.AddField("update", items);

        WWW www = new WWW("http://localhost/sqlconnect/savedata.php", form);
        yield return www;

        if(www.text == "0")
        {
            Debug.Log("Saved ");
        }
        else
        {
            Debug.Log("Saved Failed " + www.text);
        }

        //DBManager.LogOut();
        UnityEngine.SceneManagement.SceneManager.LoadScene(6);
    }

}
