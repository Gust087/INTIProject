using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerScript : MonoBehaviour {

    public Animator animator;
    public int cantMovement;

    int count = 0;
    int numberDir = 0;
    int vel = 1;

    //int[] sales_stock_2 = new int[] {130, 300, 10}; // ida y vuelta es alrevés
    //int[] dir_sales_stock_2 = new int[] { 0, 2, 0, 1, 3, 1};
    //int[] material_stock_1 = new int[] { 130, 100, 10}; // ida y vuelta es alrevés
    //int[] dir_material_stock_1 = new int[] { 0, 3, 0, 1, 2, 1};
    //int[] maquina_materia_prima = new int[] { 80, 10, 10, 80}; // ida y vuelta es alrevés
    //int[] dir_maquina_materia_prima = new int[] { 3,1,0,2};
    //int[] maquina_mermeladas = new int[] { 50, 10, 10, 50}; // ida y vuelta es alrevés
    //int[] dir_maquina_mermeladas = new int[] { 2,1,0,3};

    // 0=arriba, 2=derecha, 3=izquierda, 1=abajo
    int[] cantMov = new int[] { 80,10,300,10,80,50,10,300,10,50,130,300,10,10,300,130,130,100,10,10,100,130};
    int[] recorrido = new int[] {3,1,99,0,2,2,1,99,0,3,0,2,0,1,3,1,0,3,0,1,2,1};

    const string
        wRight = "right",
        wLeft = "left",
        wUp = "up",
        wDown = "down"
        ;
    //   int[] directions = new int[] {'E','O','S','N' };

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        CheckAnims(Move(recorrido[numberDir], vel));

        if (count > cantMov[numberDir])
        {
            numberDir++;
            count = 0;
        }
        if (numberDir == recorrido.Length)
        {
            numberDir = 0;
        }
        transform.Translate(Move(recorrido[numberDir], vel));
        count++;
    }

    Vector2 Move(int a, int v)
    {
        switch (a)
        {
            case 0:
                return new Vector2(0f, 0.025f * v);
            case 1:
                return new Vector2(0f, -0.025f * v);
            case 2:
                return new Vector2(0.025f * v, 0f);
            case 3:
                return new Vector2(-0.025f * v, 0f);
            default:
                return new Vector2(0f,0f);
        }
    }

    void CheckAnims(Vector2 v)
    {
        if (animator == null)
        {
            return;
        }
        if (v[0] < 0)
        {
            animator.SetBool(wRight, false);
            animator.SetBool(wUp, false);
            animator.SetBool(wDown, false);
            animator.SetBool(wLeft, true);

        }
        else if(v[0] > 0)
        {
            animator.SetBool(wLeft, false);
            animator.SetBool(wUp, false);
            animator.SetBool(wDown, false);
            animator.SetBool(wRight, true);

        }
        else if (v[1] < 0)
        {
            animator.SetBool(wRight, false);
            animator.SetBool(wLeft, false);
            animator.SetBool(wUp, false);
            animator.SetBool(wDown, true);

        }
        else if (v[1] > 0)
        {
            animator.SetBool(wRight, false);
            animator.SetBool(wLeft, false);
            animator.SetBool(wDown, false);
            animator.SetBool(wUp, true);
        }
        else
        {
            animator.SetBool(wRight, false);
            animator.SetBool(wLeft, false);
            animator.SetBool(wUp, false);
            animator.SetBool(wDown, false);

        }

    }

}
