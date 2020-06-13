using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] float levelLoadDelay = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadNextScene", levelLoadDelay);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void LoadNextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        SceneManager.LoadScene(nextScene);
    }
}
