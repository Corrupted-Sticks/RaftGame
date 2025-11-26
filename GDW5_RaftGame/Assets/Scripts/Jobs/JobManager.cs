using UnityEngine;
using SDS_Locations;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine.UI;
using System;
using System.Runtime.CompilerServices;
using SDS_Weather;

namespace SDS_Jobs
{
    public class JobManager : MonoBehaviour
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
        Stack<Job> _currentJobs = new Stack<Job>();
        JobWaypoint jwp;
        Stack<ICommand> _executedJobActions = new Stack<ICommand>();


        [SerializeField] Transform _JobSelectionUIContent;




        private void Start() => jwp = FindFirstObjectByType<JobWaypoint>();
        private void FixedUpdate() => _waypoint.UpdateWaypoint();


        /// <summary>
        /// called to triggers a new job action.
        /// </summary>
        public void ExecuteJobAction(Transform targetObject)
        {
            ICommand jobAction = new JobAction(targetObject, _JobSelectionUIContent, jwp, _currentJobs, _executedJobActions);

            jobAction.Execute();
        }


        /// <summary>
        /// called to undo job action.
        /// </summary>
        public void UndoLastAction()
        {
            if (_executedJobActions.Count <= 0)
            {
                Debug.Log("No job actions to undo.");
                return;
            }

            ICommand lastAction = _executedJobActions.Peek();
            lastAction.Undo();
        }

       public void ClearUndoStack()
        {
            _executedJobActions.Clear();
        }

    }


    public class JobAction : ICommand
    {
        /// <summary>
        /// the actual object we are moving.
        /// </summary>
        Transform _targetObject;
        /// <summary>
        /// since the job action will handle accepting/unaccepting jobs, it will move the UI object from the list of jobs to YOUR list of jobs. this is the transform for each.
        /// </summary>
        Transform _targetParent;

        Transform _originalParent;

        Vector2 _originalPositionOffset;

        Stack<Job> _currentJobs;

        Stack<ICommand> _executedJobs;
        JobWaypoint _jwp;

        public JobAction(Transform targetObject, Transform targetParent, JobWaypoint jwp, Stack<Job> jobStack, Stack<ICommand> executedJobs)
        {
            _targetObject = targetObject;
            _targetParent = targetParent;
            _originalParent = targetObject.parent;
            _currentJobs = jobStack;
            _jwp = jwp;
            _executedJobs = executedJobs;
            _originalPositionOffset = targetObject.localPosition;
        }

        void ICommand.Execute()
        {
            if (_targetParent == null)
            {
                Debug.Log("targetParent null");
                return;
            }




            if (_targetObject.TryGetComponent(out JobSelection js))
            {
                _currentJobs.Push(js.Job);

                Vector3 newPos = Locations.IslandPositions[_currentJobs.Peek().EndLocation];
                Vector3 curPos = Locations.IslandPositions[_currentJobs.Peek().StartLocation];
                _jwp.transform.position = newPos;
                _jwp.gameObject.SetActive(true);
                _jwp.UpdateWaypoint();

                Vector3 dir = newPos - curPos;

                float windAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
                Debug.Log(windAngle);

                WeatherInfo newWeather = new WeatherInfo(WeatherTypes.none, 0, windAngle );
                WeatherManager.instance.CurrentWeather = newWeather;





            }
            else
            {
                Debug.Log("Error getting the JobSelection from target UI.");
                return;
            }

            if (_targetObject.TryGetComponent(out Button button))
            {
                button.enabled = false;
            }

            _targetObject.SetParent(_targetParent);
            _targetObject.localPosition = Vector2.zero;
            _executedJobs.Push(this);
        }

        void ICommand.Undo()
        {
            if (_originalParent == null)
            {
                Debug.Log("originalParent null");
                return;
            }

            _executedJobs.Pop();
            _currentJobs.Pop();


            if (_currentJobs.Count <= 0)
                _jwp.gameObject.SetActive(false);
            else
            {

                Vector3 newPos = Locations.IslandPositions[_currentJobs.Peek().EndLocation];
                _jwp.transform.position = newPos + Vector3.up * 5;
                _jwp.gameObject.SetActive(true);
                _jwp.UpdateWaypoint();
            }

           



            if (_targetObject.TryGetComponent(out Button button))
            {
                button.enabled = true;
            }

            _targetObject.SetParent(_originalParent);
            _targetObject.localPosition = _originalPositionOffset;
        }
    }



    public struct Job
    {
        [SerializeField] Docks _startLocation;
        public Docks StartLocation { get { return _startLocation; } }

        [SerializeField] Docks _endLocation;
        public Docks EndLocation { get { return _endLocation; } }

        // not sure if we want.
        [SerializeField] float TimeLimit;

        public Job(int startIsland, int endIsland, int timeLimit = -1)
        {
            _startLocation = (Docks)startIsland;
            _endLocation = (Docks)endIsland;
            TimeLimit = timeLimit;
        }

        public Job(Docks startIsland, Docks endIsland, int timeLimit = -1)
        {
            _startLocation = startIsland;
            _endLocation = endIsland;
            TimeLimit = timeLimit;
        }


    }
}
