using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panda
{
    [SerializeField] private Color colorNormal;
    [SerializeField] private Color colorContaminated;

    private bool IsContaminated { get; set; } /* true : have been contaminated */ 
    private bool IsContagious { get; set; } /*this should could help to set an infection rate*/
    private Vector3 Position { get; set; } /*contains the position into a cube */ 
    private List<Panda> Neighboors { get; set; } /*this will contains all the Neighboors that should be contaminated*/ 
    private GameObject GameObject { get; set; }

    public Panda(Vector3 position)
        ///this will set a new Panda with his/her position into the cube and if is contaminated and contagious/// 
    {
        this.Position = position;
        //this.transform.position = position;
        this.IsContaminated = false;
        this.IsContagious = false;
        this.GameObject = GameObject.Instantiate(Contamination_Manager.instance.gameObject, position, Quaternion.identity);
    }

    protected void Contaminate()
        /// this change some parameter when a neighboord contaminate this///  
    {
        this.IsContaminated = true;
        this.IsContagious = true;
    }


    protected void FindNeighboors(List<Panda> forest)
        ///  ////
    {
        List<Vector3> listPositionPossible = new List<Vector3>();

        for (int i = 0; i < 3; i++)
        {
            Vector3 temp = Vector3.zero;
            Vector3 temp2 = Vector3.zero;

            temp[i] = 1;
            temp2[i] = -1;

            temp += this.Position;
            temp2 += this.Position;

            if(temp[i] <= 2)
            {
                listPositionPossible.Add(temp);
            }

            if(temp2[i] >= 0)
            {
                listPositionPossible.Add(temp2);
            }

        }

        foreach (Panda otherPanda in forest)
        {
            foreach (Vector3 positionPossible in listPositionPossible)
            {
                if(otherPanda.Position == positionPossible)
                {
                    this.Neighboors.Add(otherPanda);
                }
            }
        }

        return;
    }
}
