﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maquina : MonoBehaviour {

    private IEnumerator coroutine;
    private SpriteRenderer tape_sr;

    // Use this for initialization
    void Start () {
        tape_sr = gameObject.GetComponent<SpriteRenderer>();

        coroutine = move_tape(.3f);
        StartCoroutine(coroutine);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator move_tape(float waitTime)
    {
        while(true)
        {
            switch (tape_sr.sprite.name)
            {
                case "maquina_cinta":
                    tape_sr.sprite = (Sprite)Resources.Load("Sprites/maquina_cinta_2", typeof(Sprite));
                    break;
                case "maquina_cinta_2":
                    tape_sr.sprite = (Sprite)Resources.Load("Sprites/maquina_cinta_3", typeof(Sprite));
                    break;
                case "maquina_cinta_3":
                    tape_sr.sprite = (Sprite)Resources.Load("Sprites/maquina_cinta", typeof(Sprite));
                    break;
            }

            yield return new WaitForSeconds(waitTime);
        }
    }
}
