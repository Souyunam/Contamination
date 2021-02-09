using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Contamination
{
    public class Panda : MonoBehaviour
    {
        [SerializeField] private Color colorNormal;
        [SerializeField] private Color colorContaminated;

        private bool isContaminated; /* true : have been contaminated */
        private bool isContagious; /*this should could help to set an infection rate*/
        [SerializeField] private List<Panda> neighboors; /*this will contains all the Neighboors that should be contaminated*/

        private void Start()
        {
            isContagious = false;
            isContaminated = false;
            neighboors = new List<Panda>();
        }



        public void FindNeighboors(List<Panda> forest)
        /// this will find all the neighboors of of current panda amoung a list in parameter calls forest ////
        {
            
            List<Vector3> listPositionPossible = new List<Vector3>();

            for (int i = 0; i < 3; i++)
            {
                Vector3 temp = Vector3.zero;
                Vector3 temp2 = Vector3.zero;

                temp[i] = 1;
                temp2[i] = -1;

                temp += this.transform.position;
                temp2 += this.transform.position;

                if (temp[i] <= 2)
                {
                    listPositionPossible.Add(temp);
                }

                if (temp2[i] >= 0)
                {
                    listPositionPossible.Add(temp2);
                }

            }

            foreach (Panda otherPanda in forest)
            {
                if(otherPanda != this)
                {
                    foreach (Vector3 positionPossible in listPositionPossible)
                    {
                        if (otherPanda.transform.position == positionPossible)
                        {
                            this.neighboors.Add(otherPanda);
                        }
                    }
                }
            }

            return;
        }
    }
}