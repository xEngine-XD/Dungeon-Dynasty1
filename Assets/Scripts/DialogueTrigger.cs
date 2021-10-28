using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Collidable
{
    public bool isActive = false;
    public bool moreToSay = false;
    public DialogueTrigger nextTrigger;
    private bool activated = false;
    // Start is called before the first frame update


    protected override void OnCollide(Collider2D coll)
    {
        if(coll.name == "Player" && isActive)
        {
            if (!activated)
            {
                activated = true;
                GameManager.instance.dialogueManager.ShowDialogue();
            }
            if(activated && Input.GetKeyDown(KeyCode.E))
            {
                if (moreToSay)
                {
                    if (nextTrigger != null)
                    {
                        nextTrigger.isActive = true;
                        Destroy(this.gameObject);
                    }
                }
                else
                {
                    Destroy(this.gameObject);
                    Debug.Log("Thats all");
                }

            }


            
            
        }
    }
}
