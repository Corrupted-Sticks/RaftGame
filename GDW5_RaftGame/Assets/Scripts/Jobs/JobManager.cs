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
        public JobObject currentJob = null;

        [SerializeField] TextMeshProUGUI _curMoneyText;

        [SerializeField] TextMeshProUGUI _jobDescription;

        [SerializeField] Button _TurnInButton;

        [SerializeField] Transform _JobSelectionUIContent;

        [SerializeField] int _currentMoney;

        public int CurrentMoney
        {
            get => _currentMoney;
            set
            {
                _currentMoney = value;
                _curMoneyText.text = _currentMoney.ToString();
            }
        }

        private void Start() => _waypoint = FindFirstObjectByType<JobWaypoint>();
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
            HUDManager.instance.SendJobObj(currentJob);

            CargoFactory.instance.SpawnCargo(currentJob.CargoTypes);
            _waypoint.transform.position = Locations.IslandPositions[currentJob.EndDock];
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
            CubeCargo[] cube = FindObjectsByType<CubeCargo>(FindObjectsSortMode.None);
            foreach (var cargo in cube) Destroy(cargo.gameObject);
            BarrelCargo[] barrel = FindObjectsByType<BarrelCargo>(FindObjectsSortMode.None);
            foreach (var cargo in barrel) Destroy(cargo.gameObject);
            StretchCargo[] stretch = FindObjectsByType<StretchCargo>(FindObjectsSortMode.None);
            foreach (var cargo in stretch) Destroy(cargo.gameObject);
            currentJob = null;

            HUDManager.instance.ResetHUDJob();
            HUDManager.instance.SetMoney(CurrentMoney);
            AudioManager.instance.PlaySFX(0); //Success!
        }

        public override void Notify(CargoSubject subject)
        {
            HUDManager.instance.SetLostCargo(); //Ticks the number of cargo lost by 1 in the HUD
        }
    }
}

