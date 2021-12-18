using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    [SerializeField] private SceneLoader loader;
    [SerializeField] private bool restart = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (restart)
            {
                loader.LoadStartScreen();
            }
            else
            {
                loader.LoadNextScene();
            }
        }
    }
}
