using System.Collections.Generic;

public class Subject
{
    private List<IObserver> observers = new List<IObserver>();

    public void RegisterObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void UnregisterObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyWinObservers()
    {
        foreach (IObserver observer in observers)
        {
            observer.OnWin();
        }
    }
    public void NotifyLoseObservers()
    {
        foreach (IObserver observer in observers)
        {
            observer.OnLose();
        }
    }
}