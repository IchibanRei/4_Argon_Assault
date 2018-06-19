using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {
    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Start ()
    {
        Invoke("LoadFirstScene", 5f);
	}
	

    void LoadFirstScene()
    {
        SceneManager.LoadScene(1);
    }
}
