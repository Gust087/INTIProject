using System.Collections;
using UnityEngine;

public class MachineScript : MonoBehaviour {

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
                case "machine_1":
                    tape_sr.sprite = (Sprite)Resources.Load("Sprites/machine_2", typeof(Sprite));
                    break;
                case "machine_2":
                    tape_sr.sprite = (Sprite)Resources.Load("Sprites/machine_3", typeof(Sprite));
                    break;
                case "machine_3":
                    tape_sr.sprite = (Sprite)Resources.Load("Sprites/machine_1", typeof(Sprite));
                    break;
            }

            yield return new WaitForSeconds(waitTime);
        }
    }
}
