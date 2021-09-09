using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    public string[] scenes;
    protected override void OnCollide(Collider2D coll)
    {
        if(coll.tag == "Player")
        {
            if (scenes.Length > 0)
            {
                GameManager.instance.SaveState();
                string sceneName = scenes[0];
                SceneManager.LoadScene(sceneName);
            }

        }
    }
}
