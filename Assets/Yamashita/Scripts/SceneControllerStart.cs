using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public void Scenemg()
    {
        SceneManager.LoadScene("InGame");
        Time.timeScale = 1.0f;
    }
}
