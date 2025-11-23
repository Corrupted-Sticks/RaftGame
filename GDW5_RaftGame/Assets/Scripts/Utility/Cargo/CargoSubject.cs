using System.Collections;
using UnityEngine;

public abstract class CargoSubject : MonoBehaviour
{
    private readonly ArrayList observers = new ArrayList();

    protected void Attach(CargoObserver observer)
    {
        observers.Add(observer);
    }

    protected void Detach(CargoObserver observer)
    {
        observers.Remove(observer);
    }

    protected void NotifyObservers()
    {
        foreach (CargoObserver observer in observers)
        {
            observer.Notify(this);
        }
    }
}
