using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    public string[] scenes;
    public GameObject promt;
    private bool isActive = true;
    public bool tutorialCompleted = false;
    private void LateUpdate()
    {
        if(isActive == false && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine("Cooldown");
        }
    }
    protected override void OnCollide(Collider2D coll)
    {
        if(coll.tag == "Player")
        {
            if (scenes.Length > 0 && GameManager.instance.contractSigned && tutorialCompleted)
            {

                string sceneName = scenes[0];
                GameManager.instance.SavePlayer("1");
                SceneManager.LoadScene(sceneName);
                
            }
            else
            {
                if (isActive)
                {
                    promt.GetComponent<DialogueManager>().PortalPromt();
                    isActive = false;
                }
                

                
            }

        }

    }
    
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(2.0f);
        isActive = true;
    }

}
