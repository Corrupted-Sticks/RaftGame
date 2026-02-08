using UnityEngine;
using SDS_Locations;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine.UI;
using System;
using System.Runtime.CompilerServices;
using SDS_Weather;
using TMPro;

namespace SDS_Jobs
{
    public class JobManager : CargoObserver
    {
        public static JobManager instance;
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this.gameObject); // keeps the oldest jobmanager, as to not lose progress.
                return;
            }
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        [SerializeField] JobWaypoint _waypoint;

        [SerializeField] Button currentlySelectedButton;
        JobObject currentJob = null;

        JobWaypoint jwp;
        Stack<ICommand> _executedJobActions = new Stack<ICommand>();

        [SerializeField] TextMeshProUGUI _curMoneyText;

        [SerializeField] TextMeshProUGUI _jobDescription;

        [SerializeField] Button _TurnInButton;

        [SerializeField] Transform _JobSelectionUIContent;

        int _currentMoney;

        public int CurrentMoney
        {
            get => _currentMoney;
            set
            {
                _currentMoney = value;
                _curMoneyText.text = _currentMoney.ToString();
            }
        }


        private void Start() => jwp = FindFirstObjectByType<JobWaypoint>();
        private void FixedUpdate() => _waypoint.UpdateWaypoint();


        /// <summary>
        /// called to triggers a new job action.
        /// </summary>
        public void OnJobSelect(Button newButton, JobObject jObj)
        {
            if (currentlySelectedButton != null) currentlySelectedButton.interactable = true;
            currentlySelectedButton = newButton;
            _jobDescription.text = jObj.Description;
            currentJob = jObj;
        }


        public void AcceptJob()
        {
            CargoFactory.instance.SpawnCargo(currentJob.CargoTypes);
        }



        public void CheckJobComplete(Docks dockArrivedAt)
        {
            if (currentJob == null) return;
            if (dockArrivedAt == currentJob.EndDock) _TurnInButton.interactable = true;
        }


        public void CompleteJob()
        {
            CurrentMoney += currentJob.Reward;
            _TurnInButton.interactable = false;
            currentJob = null;
        }

        public override void Notify(CargoSubject subject)
        {

        }
    }
}

