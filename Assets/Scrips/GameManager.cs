using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject box;
    public GameObject jam;

    List<empty_box_script> empty_box = new List<empty_box_script>();
    List<jam_box_script> jam_box = new List<jam_box_script>();

    float[] stockX1 = new float[] { -1.6f, -0.8f, 1.4f, 2.2f };
    float[] stockX2 = new float[] { 4.4f, 5.2f, 7.4f, 8.2f };
    float[] stockY = new float[] { 4.2f, 3.3f, 2.5f };

    int time_count = 0;
    int box_count = 0;
    bool active = false;

    // Use this for initialization
    void Start () {
        Instantiate_box();
    }
	
	// Update is called once per frame
	void Update () {
        Animation_box(active);
    }

    void Animation_box(bool a)
    {
        if (box_count == 0)
        {
            box_count = jam_box.Count - 1;
            active = !active;
        }
        else if (time_count % 1100 == 0)
        {
            empty_box[box_count].gameObject.SetActive(!a);
            jam_box[box_count].gameObject.SetActive(a);
            box_count--;
        }
        time_count++;
    }

    void Instantiate_box() {
        for (int i = 0; i < stockY.Length; i++)
        {
            for (int a = 0; a < stockX1.Length; a++)
            {
                GameObject auxgo = Instantiate(box, new Vector2(stockX1[a], stockY[i]), new Quaternion());
                empty_box.Add(auxgo.GetComponent<empty_box_script>());
            }
            for (int e = 0; e < stockX2.Length; e++)
            {
                GameObject auxgo2 = Instantiate(jam, new Vector2(stockX2[e], stockY[i]), new Quaternion());
                jam_box.Add(auxgo2.GetComponent<jam_box_script>());
            }
        }
    }

}
