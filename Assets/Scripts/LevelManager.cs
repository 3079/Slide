using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private string currentScene;
    public AudioClip reload;
    public AudioClip insert;
    private AudioSource source;
    private void Awake() 
    {
        DontDestroyOnLoad(this.gameObject);
        currentScene = "Menu";
        source = GetComponent<AudioSource>();
    }
    public void Level1()
    {
        source.PlayOneShot(insert);
        SceneManager.LoadScene("level 1");
        currentScene = "level 1";
    }
    public void Level2()
    {
        source.PlayOneShot(insert);
        SceneManager.LoadScene("level 2");
        currentScene = "level 2";
    }
    public void Level3()
    {
        source.PlayOneShot(insert);
        SceneManager.LoadScene("level 3");
        currentScene = "level 3";
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(!currentScene.Equals("Menu"))
            {
                source.PlayOneShot(reload);
                SceneManager.LoadScene(currentScene);
            }
        }
    }
}
