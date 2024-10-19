using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int nextScene;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
             SceneManager.LoadScene(nextScene);

        }
    }
}
