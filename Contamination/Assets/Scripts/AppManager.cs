using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Contamination
{
    public class AppManager : MonoBehaviour
    {
        #region Variables
        public static AppManager instance;

        private GameObject currentActivatedUI;
        [SerializeField] private GameObject startUI;
        [SerializeField] private GameObject patientZeroUI;
        [SerializeField] private GameObject contaminationUI;
        [SerializeField] private GameObject errorMessageUI; //errorMessage will be disable directly with unity 
        [SerializeField] private GameObject menuUI;

        [SerializeField] private InputField inputFieldDimensionForest;
        [SerializeField] private Transform forestTransform;

        [SerializeField] private Button buttonSpreading;
        [SerializeField] private Text textDay;
        [SerializeField] private Text textContaminationRate;
        #endregion

        #region Instance
        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            currentActivatedUI = startUI;
            startUI.SetActive(true);
        }
        #endregion

        public void ConfirmDimension()
            ///This function is called when the user click on the confirm button about forest dimension
        {
            if(inputFieldDimensionForest != null)
            {
                int temp = int.Parse(inputFieldDimensionForest.text);
                if(temp > 0 && temp <= 100)
                {
                    ContaminationManager.instance.forestDimension = temp;
                    ContaminationManager.instance.CreateForest();

                    ActivateMenu(patientZeroUI);
                    MoveCamera.instance.SetCameraPosition(temp);
                    return;
                }
            }

            errorMessageUI.SetActive(true);
            return;
        }

        public void ConfirmPatientZero()
            ///This function is called when you click to confirm the patientZero
        {
            if(ContaminationManager.instance.ConfirmPatientZero()) // Check if patient zero is null or not
            {
                ActivateMenu(contaminationUI);
            }

            else
            {
                errorMessageUI.SetActive(true);
            }
        }


        public void UpdateTextContamination(float contaminationRate, int numberOfDay, bool everybodyContaminated)
        {
            ///<summary> Update the UI on contamination menu</summary>
            ///<param name="contaminationRate"> float the current contamination rate</param>
            ///<param name="numberOfDay"> int the current number of day until the beginning</param>
            ///<param name="everybodyContaminated"> bool this will set if you are allowed to click again to the spreading button (for exemple if the contaminationRate is over 100%) </param>
            textDay.text = "Day : " + numberOfDay;
            textContaminationRate.text = "Contamination rate : " + contaminationRate.ToString("#.0") + "%";

            if(everybodyContaminated)
            {
                buttonSpreading.interactable = false;
            }
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                ActivateMenu(menuUI);
            }
        }

        private void ActivateMenu(GameObject menuToActivate)
            ///Desactivate the current UI and activate the one set in parameter
        {
            if (menuToActivate != currentActivatedUI)
            {
                menuToActivate.SetActive(true);
                currentActivatedUI.SetActive(false);
                currentActivatedUI = menuToActivate;
            }
        }

        public void LeaveApp()
        {
            Application.Quit();
        }

        public void ResetToZero()
        {
            ///<summary> Reset to zero</summary>
            textDay.text = "Day : ";
            textContaminationRate.text = "Contamination rate : 0 %";
            buttonSpreading.interactable = true;
            ContaminationManager.instance.ResetToZero();
            ActivateMenu(startUI);
        }
    }
}
