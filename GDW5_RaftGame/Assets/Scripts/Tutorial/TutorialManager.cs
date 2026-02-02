using UnityEngine;
using System.Collections.Generic;
using TMPro;
namespace SDS_Tutorial
{
    public class TutorialManager : MonoBehaviour
    {

        [SerializeField] TutorialLines lines;
        [SerializeField] TextMeshProUGUI tutorialText;

        public void SetTutorialFlag(TutorialStep step)
        {
            stepFlags.TryGetValue(step, out bool curValue);
            if (!curValue) // only set to true and update the line if it's the first time triggering the tutorial step.
            {
                stepFlags[step] = true;
                string line = lines.GetLine((int)step);
                if (string.IsNullOrEmpty(line)) { // if empty line hit, end of tutorial has been reached, and we can close the text.
                    tutorialText.text = ""; 
                    tutorialText.GetComponentInParent<Canvas>().enabled = false; 
                }
                tutorialText.text = line;
            }
        }

        public enum TutorialStep
        {
            Moved,
            TurnedMast,
            Disembarked,
            OpenedJobMenu,
            SelectedJob,
            AcceptedJob,
            PlayerBackOnBoat,
            CargoOffDock,
            BothPlayersBackOnBoat,
            MapOpened,
            DockedAtEndGoal,
            CargoOffBoat,
            JobCompleted
        }


        Dictionary<TutorialStep, bool> stepFlags = new ()
        {
            { TutorialStep.Moved, false }, // has moved with wasd (and controller once implemented)
            { TutorialStep.TurnedMast, false }, // has grabbed mast
            { TutorialStep.Disembarked, false }, // has navigated to a dock and pressed F to depart from the boat.
            { TutorialStep.OpenedJobMenu, false }, // has Opened the job menu
            { TutorialStep.SelectedJob, false }, // has selected a job in the job menu
            { TutorialStep.AcceptedJob, false }, // has accepted the job.
            { TutorialStep.PlayerBackOnBoat, false }, // has 1 player got back on the boat
            { TutorialStep.CargoOffDock, false }, // has all the cargo been moved off the dock
            { TutorialStep.BothPlayersBackOnBoat, false }, // are both players back on ship
            { TutorialStep.MapOpened, false }, // has map been opened
            { TutorialStep.DockedAtEndGoal, false }, // has docked at destination
            { TutorialStep.CargoOffBoat, false }, // has cargo been moved off the boat
            { TutorialStep.JobCompleted, false }, // has job been completed

        };
    }

}
