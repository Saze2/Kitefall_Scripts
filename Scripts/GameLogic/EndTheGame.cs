using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndTheGame : MonoBehaviour
{
    public GameObject CompleteLevelUI;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Invoke("EndGame", 4);
            Destroy(other.gameObject, 2);
            CompleteLevelUI.SetActive(true);

        }
    }

    public void EndGame()
    {     
        SceneManager.LoadScene("MainMenu");
    }
}
