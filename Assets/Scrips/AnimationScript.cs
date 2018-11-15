using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour {


    public static AnimationScript instance;

    public GameObject box;
    public GameObject jam;

    private List<EmptyBoxScript> empty_box = new List<EmptyBoxScript>();
    private List<JamBoxScript> jam_box = new List<JamBoxScript>();
    private List<JarScript> jar_list = new List<JarScript>();

    private float[] stockX1 = new float[] { -1.6f, -0.8f, 1.4f, 2.2f };
    private float[] stockX2 = new float[] { 4.4f, 5.2f, 7.4f, 8.2f };
    private float[] stockY = new float[] { 4.2f, 3.3f, 2.5f };

    private int time_count = 0;
    private int box_count = 0;
    private bool active = false;

    private static bool is_holiday = false;

    public List<EmptyBoxScript> Empty_box
    {
        get
        {
            return empty_box;
        }

        set
        {
            empty_box = value;
        }
    }
    public List<JamBoxScript> Jam_box
    {
        get
        {
            return jam_box;
        }

        set
        {
            jam_box = value;
        }
    }
    public List<JarScript> Jar_list
    {
        get
        {
            return jar_list;
        }

        set
        {
            jar_list = value;
        }
    }

    public static bool Is_holiday
    {
        get
        {
            return is_holiday;
        }

        set
        {
            is_holiday = value;
        }
    }


    // Use this for initialization
    void Start()
    {
        instance = this;
        Instantiate_box();
    }
	
	// Update is called once per frame
	void Update () {
        if (!is_holiday)
            Animation_box(active, true);
        else
            Animation_box(active, false);
    }

    //Comienza animación producir
    public void StartAnimationJar(float wt, bool j, int c)
    {
        if (Jar_list.Capacity > 0)
        {
            ActiveJam();
        }
        else
        {
            IEnumerator coroutine = Move_jar(wt, j, c);
            StartCoroutine(coroutine);
        }
    }
    private IEnumerator Move_jar(float waitTime, bool jam, int cant_jam)
    {
        
            if(jam && !is_holiday)
            {
                for (int a = 0; a < cant_jam; a++)
                {
                    GameObject auxgo = Instantiate((GameObject)Resources.Load("Prefabs/jam", typeof(GameObject)));
                    Jar_list.Add(auxgo.GetComponent<JarScript>());
                    yield return new WaitForSeconds(waitTime);
                }
            }
    }

    void Animation_box(bool a, bool no_anim)
    {
        if (no_anim)
        {
            if (box_count == 0)
            {
                box_count = Jam_box.Count - 1;
                active = !active;
            }
            else if (time_count % 1100 == 0)
            {
                Empty_box = Empty_box ?? new List<EmptyBoxScript>();
                Jam_box = Jam_box ?? new List<JamBoxScript>();
                if (Jam_box.Count == 0) Instantiate_box();
                Empty_box[box_count].gameObject.SetActive(!a);
                Jam_box[box_count].gameObject.SetActive(a);
                box_count--;
            }
            time_count++;   
        }
    }

    void Instantiate_box() {
        for (int i = 0; i < stockY.Length; i++)
        {
            for (int a = 0; a < stockX1.Length; a++)
            {
                GameObject auxgo = Instantiate(box, new Vector2(stockX1[a], stockY[i]), new Quaternion());
                Empty_box.Add(auxgo.GetComponent<EmptyBoxScript>());
            }
            for (int e = 0; e < stockX2.Length; e++)
            {
                GameObject auxgo2 = Instantiate(jam, new Vector2(stockX2[e], stockY[i]), new Quaternion());
                Jam_box.Add(auxgo2.GetComponent<JamBoxScript>());
            }
        }
    }

    public void ClearJam()
    {
        foreach (JarScript jar in Jar_list)
        {
            jar.gameObject.SetActive(false);
        }
    }

    public void ActiveJam()
    {
        foreach (JarScript jar in Jar_list)
        {
            jar.gameObject.SetActive(true);
        }
    }

}
