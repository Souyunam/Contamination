using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Contamination
{
    public class Panda : MonoBehaviour
    {
        #region Variables
        [SerializeField] private Color colorNormal;
        [SerializeField] private Color colorContaminated;
        [SerializeField] private Color colorSelected;

        [SerializeField] private bool isContaminated = false; /* true : have been contaminated */
        [SerializeField] private bool isContagious = false; /*this should could help to set an infection rate*/

        [SerializeField] private List<Panda> neighboors; /*this will contains all the Neighboors that should be contaminated*/

        private Material m_material;
        #endregion

        private void Start()
        {
            m_material = this.gameObject.GetComponent<Renderer>().material;
        }
        public void IsContaminated()
        {
            ///<summary> this will changes attributes when this panda is contaminated as color or if he is contagious</summary>
            isContagious = true;
            isContaminated = true;
            m_material.color = colorContaminated;
            ContaminationManager.instance.AddContagiousPanda(this);
        }


        public void cure()
        {
            ///<summary> this will change attributes when a panda is cure in this way he/she can't be contaminated again and will no longueur spread the virus</summary>
            ///This idea was not finished yet
            isContagious = false;
            ContaminationManager.instance.RemoveContagiousPanda(this);
        }
        public void Contaminate()
        {
            ///<summary> if the current panda is contagious this spread the disease to its neighboors</summary>
            if(isContagious)
            {
                foreach (Panda neighboor in this.neighboors)
                {
                    if(!neighboor.isContaminated)
                    {
                        neighboor.IsContaminated();
                    }

                }
                return;
            }

            else
            {
                return;
            }
        }


        public void FindNeighboors(List<Panda> forest)
            ///<summary>
            /// this will find all the neighboors of of current panda amoung a list in parameter calls forest ////
            ///</summary>
            ///<param name="forest"> list of pandas into the forest</param>
        {
            
            List<Vector3> listPositionPossible = new List<Vector3>();
            int forestDimension = ContaminationManager.instance.forestDimension;

            //We set a list of neighboor possible position 
            for (int i = 0; i < 3; i++)
            {
                Vector3 temp = Vector3.zero;
                Vector3 temp2 = Vector3.zero;

                temp[i] = 1;
                temp2[i] = -1;

                temp += this.transform.position;
                temp2 += this.transform.position;

                if (temp[i] < forestDimension)
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
                            neighboors.Add(otherPanda);
                        }
                    }
                }
            }

            return;
        }

        public void IsSelected()
        {
            ///<summary> This function is call when you select </summary>
            m_material.color = colorSelected;
        }

        public void Unselect()
            ///<summary> This function is call when you unselect a panda by clicking on another one</summary>
        {
            m_material.color = colorNormal;
        }

        public void KillIt()
        {
            ///<summary>This will destroy the object panda</summary>
            Destroy(this.gameObject);
        }
    }
}