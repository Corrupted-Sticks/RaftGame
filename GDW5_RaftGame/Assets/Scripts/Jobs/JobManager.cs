using UnityEngine;
using SDS_Locations;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine.UI;

public class JobManager : MonoBehaviour
{
    public static JobManager instance;

    [SerializeField] JobWaypoint _waypoint;
    Stack<Job> _currentJobs = new Stack<Job>();
    JobWaypoint jwp;
    Stack<ICommand> _executedJobActions = new Stack<ICommand>();


    [SerializeField] Transform _JobSelectionUIContent;



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
    private void Start() => jwp = FindFirstObjectByType<JobWaypoint>();
    private void FixedUpdate() => _waypoint.UpdateWaypoint();


    /// <summary>
    /// called to triggers a new job action.
    /// </summary>
    public void ExecuteJobAction(Transform targetObject)
    {
        ICommand jobAction = new JobAction(targetObject, _JobSelectionUIContent,jwp, _currentJobs, _executedJobActions);

        jobAction.Execute();
    }


    /// <summary>
    /// called to undo job action.
    /// </summary>
    public void UndoLastAction()
    {
        if (_executedJobActions.Count == 0)
        {
            Debug.Log("No job actions to undo.");
            return;
        }

        ICommand lastAction = _executedJobActions.Pop();
        _currentJobs.Pop();
        lastAction.Undo();
    }




}


public class JobAction: ICommand
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
        lastAction.Undo();



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
