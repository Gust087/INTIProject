using UnityEngine;

public class JarScript : MonoBehaviour {

    private Transform jar_pos;
    private float jar_pos_aux_y;
    private float jar_pos_aux_x;

    // Use this for initialization
    void Start () {
        jar_pos = gameObject.GetComponent<Transform>();
        jar_pos_aux_y = jar_pos.position.y;
        jar_pos_aux_x = jar_pos.position.x;
    }
        
	// Update is called once per frame
	void Update () {

        if (jar_pos.position.y > (jar_pos_aux_y - 0.8f) && jar_pos.position.x == jar_pos_aux_x)
        {
            jar_pos.position = new Vector2(jar_pos.position.x, jar_pos.position.y - 0.01f);
        }
        else if (jar_pos.position.y <= (jar_pos_aux_y - 0.8f) && jar_pos.position.x < (jar_pos_aux_x + 3f))
        {
            jar_pos.position = new Vector2(jar_pos.position.x + 0.01f, jar_pos.position.y);
        }
        else if (jar_pos.position.y < jar_pos_aux_y && jar_pos.position.x >= (jar_pos_aux_x + 3f))
        {
            jar_pos.position = new Vector2(jar_pos.position.x, jar_pos.position.y + 0.01f);
        }
        else if (jar_pos.position.y >= jar_pos_aux_y && jar_pos.position.x >= (jar_pos_aux_x + 0.10f))
        {
            jar_pos.position = new Vector2(jar_pos_aux_x, jar_pos_aux_y);
        }
    }
}
