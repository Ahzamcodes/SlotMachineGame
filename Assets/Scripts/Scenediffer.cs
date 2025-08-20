using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenediffer : MonoBehaviour
{
    public void change(int num)
    {
        SceneManager.LoadScene(num);
    }
}
