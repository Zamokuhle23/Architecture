using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update

    IEnumerator SaveData()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", DBManager.username);
        form.AddField("sum", DBManager.sum);
        //form.AddField("update", items);

        WWW www = new WWW("http://localhost/sqlconnect/savedata.php", form);
        yield return www;

        if (www.text == "0")
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
