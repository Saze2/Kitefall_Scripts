using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechInteract : Interactable
{
    public GameObject textBox;

    private void Start()
    {

    }

    public override void Interact()
    {
        base.Interact();
        textBox.SetActive(true);
    }

}
