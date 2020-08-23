using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GotoMain : MonoBehaviour
{
    public void start()
    {
        SceneManager.LoadScene("Menu");
    }
}
