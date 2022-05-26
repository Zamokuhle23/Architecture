using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimator : MonoBehaviour
{

    public void LoadPanels(JSONNode jsonData)
    {
        number = jsonData.Count;

        for (int i = 0; i < jsonData.Count; i++)
        {
            panels[i].SetActive(true);


            shopPanels[i].title.text = jsonData[i].AsObject["name"];
            shopPanels[i].description.text = jsonData[i].AsObject["description"];
            shopPanels[i].cost.text = "$ " + jsonData[i].AsObject["cost"];

            bool have;

            if (jsonData[i].AsObject["player"].ToString().Trim('"') == DBManager.username)
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



            tempObject temp = new tempObject(jsonData[i].AsObject["name"], jsonData[i].AsObject["description"], jsonData[i].AsObject["cost"].ToString(), jsonData[i].AsObject["player"].ToString(), have);

            items[i] = temp;








        }
    }
