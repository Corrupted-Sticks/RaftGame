using UnityEngine;
using SDS_Locations;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine.UI;
using System;
using System.Runtime.CompilerServices;

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

        /*

        example use of a factory. couldn't find a great place to use elsewhere in our GDW game, so here is a kinda unecissary forced one which i have commented out as i don't think
        keeping and using this really benifits the game.

        this example demonstrates how you could keep a dictionary to map some identifier (in my case, a difficulty enum) to various constructors, allowing simple runtime 
        constructor calls.

        you could also do this with just an int index for things like random items, or enemies, but our game doesn't have those.

        */
        public enum JobDifficulty{ // create enum for the different difficulties.
            easy,
            normal,
            hard,
            extreme,
            COUNT   // added COUNT to the enum so that if you wanted to randomize the difficulty, you could do something like:
                    // Random.range(0, JobDificulty.COUNT -1); 
                    // instead of having to manually keep track of how many difficulties there are.
        }



        // this dict maps job difficulties to a lambda which constructs jobs with specific params.
        Dictionary<JobDifficulty, Func<Job>> jobMap = new Dictionary<JobDifficulty, Func<Job>>() 
        {
            // in this specific case, they are all the same "Job" struct, but with different final paramaters which would represent the time limit.
            // in other games, however these could be various class/structs, with various differing params, it just didn't suit our game.
            { JobDifficulty.easy,    ()=> new Job( UnityEngine.Random.Range(0,(int)Islands.COUNT-1), UnityEngine.Random.Range(0,(int)Islands.COUNT-1), 10)},
            { JobDifficulty.normal,  ()=> new Job( UnityEngine.Random.Range(0,(int)Islands.COUNT-1), UnityEngine.Random.Range(0,(int)Islands.COUNT-1), 8)},
            { JobDifficulty.hard,    ()=> new Job( UnityEngine.Random.Range(0,(int)Islands.COUNT-1), UnityEngine.Random.Range(0,(int)Islands.COUNT-1), 6)},
            { JobDifficulty.extreme, ()=> new Job( UnityEngine.Random.Range(0,(int)Islands.COUNT-1), UnityEngine.Random.Range(0,(int)Islands.COUNT-1), 4)}
        };

        /// <summary>
        /// Create a job with the given difficulty.
        /// </summary>
        /// <param name="jobDifficulty">The desired difficulty of the job, controlling the Time Limit.</param>
        /// <param name="job"> Reference to the job (nullable) </param>
        /// <returns>If job was successfully created</returns>
        public bool TryCreateJob(JobDifficulty jobDifficulty, out Job? job) // job? allows me to nullify the job.
        {
            if (jobMap.TryGetValue(jobDifficulty, out Func<Job> jobConstructor)) // i use trygetvalue in case you attempt to use a jobDifficulty not found in the map ( for example, the COUNT option). 
            {                                                         // if you index with [...] and miss, it actually creates an element in the dict which is undesired
                job = jobConstructor();  // jobDifficulty was found in map. set to value, and return true to signal success.
                return true;
            }
            else
            {
                job = null; // jobDifficulty was not found in map. set to null, and return false to signal failure.
                return false;
            }
        }


        // but this is way simpler then all this.

        public Job CreateJob(JobDifficulty difficulty){

         int timer = 10 - (2* (int)difficulty); // starts at 10 and decreases by 2 per difficulty.
         return new Job(UnityEngine.Random.Range(0,(int)Islands.COUNT-1), UnityEngine.Random.Range(0,(int)Islands.COUNT-1), timer);


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
                _jwp.transform.position = newPos;
                _jwp.gameObject.SetActive(true);


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

            if (_currentJobs.Count <= 0)
                _jwp.gameObject.SetActive(false);

            ICommand lastAction = _executedJobs.Pop();
            _currentJobs.Pop();



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
        [SerializeField] Islands _startLocation;
        public Islands StartLocation { get { return _startLocation; } }

        [SerializeField] Islands _endLocation;
        public Islands EndLocation { get { return _endLocation; } }

        // not sure if we want.
        [SerializeField] float TimeLimit;

        public Job(int startIsland, int endIsland, int timeLimit = -1)
        {
            _startLocation = (Islands)startIsland;
            _endLocation = (Islands)endIsland;
            TimeLimit = timeLimit;
        }

        public Job(Islands startIsland, Islands endIsland, int timeLimit = -1)
        {
            _startLocation = startIsland;
            _endLocation = endIsland;
            TimeLimit = timeLimit;
        }


    }
}
