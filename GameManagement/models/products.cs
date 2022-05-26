using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class products : MonoBehaviour
{

    public void purchasable()
    {

        for (int i = 0; i < number; i++)
        {

            if (items[i].own)
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
        purchasable();
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
}
