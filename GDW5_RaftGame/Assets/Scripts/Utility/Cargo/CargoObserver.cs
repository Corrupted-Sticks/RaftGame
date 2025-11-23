using UnityEngine;

public abstract class CargoObserver : MonoBehaviour
{
    public abstract void Notify(CargoSubject subject);
}
