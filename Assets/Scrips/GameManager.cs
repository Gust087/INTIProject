using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject box;
    public GameObject jam;
    public GameObject attention;

    public static GameManager instance;

    public static List<EmptyBoxScript> empty_box = new List<EmptyBoxScript>();
    public static List<JamBoxScript> jam_box = new List<JamBoxScript>();
    public static List<JarScript> jar_list = new List<JarScript>();

    //ButtonScript btn;

    public static float[] stockX1 = new float[] { -1.6f, -0.8f, 1.4f, 2.2f };
    public static float[] stockX2 = new float[] { 4.4f, 5.2f, 7.4f, 8.2f };
    public static float[] stockY = new float[] { 4.2f, 3.3f, 2.5f };

    public static int time_count = 0;
    public static int time_count_jar = 0;
    public static int box_count = 0;
    public static bool active = false;
    

    // Use this for initialization
    void Start ()
    {
        instance = this;

        instance.Instantiate_box();
        //btn = GetComponent<ButtonScript>();
    }
	
	// Update is called once per frame
	void Update () {
        instance.Animation_box(active);
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    if (ButtonScript.count_pc % 2 != 0 || ButtonScript.count_options % 2 != 0)
        //    {
        //        btn.ExitOfScreenGame();
        //    }
        //}
    }

    public static IEnumerator move_jar(float waitTime, bool jam, int cant_jam)
    {
        
        while (time_count_jar % 6700 == 0)
        {
            if(jam)
            {
                for (int a = 0; a < cant_jam; a++)
                {
                    GameObject auxgo = Instantiate((GameObject)Resources.Load("Prefabs/jam", typeof(GameObject)));
                    jar_list.Add(auxgo.GetComponent<JarScript>());
                    yield return new WaitForSeconds(waitTime);
                }
                jam = false;
            }
            time_count_jar++;
        }
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
        }else if(time_count % 6700 == 0)
        {
            instance.attention.gameObject.SetActive(true);
        }
        time_count++;
    }

    void Instantiate_box() {
        for (int i = 0; i < stockY.Length; i++)
        {
            for (int a = 0; a < stockX1.Length; a++)
            {
                GameObject auxgo = Instantiate(box, new Vector2(stockX1[a], stockY[i]), new Quaternion());
                empty_box.Add(auxgo.GetComponent<EmptyBoxScript>());
            }
            for (int e = 0; e < stockX2.Length; e++)
            {
                GameObject auxgo2 = Instantiate(jam, new Vector2(stockX2[e], stockY[i]), new Quaternion());
                jam_box.Add(auxgo2.GetComponent<JamBoxScript>());
            }
        }
    }

}
