using System;
using UnityEngine.Events;

public class Event
{
    private UnityEvent unityEvent = new UnityEvent();

    public void Publish()
    {
        unityEvent.Invoke();
    }

    public void Subscribe(UnityAction call)
    {
        unityEvent.AddListener(call);
    }

    public void Unsubscribe(UnityAction call)
    {
        unityEvent.RemoveListener(call);
    }
}

public class Event<T>
{
    private UnityEvent<T> unityEvent = new UnityEvent<T>();

    public void Publish(T t)
    {
        unityEvent.Invoke(t);
    }

    public void Subscribe(UnityAction<T> call)
    {
        unityEvent.AddListener(call);
    }

    public void Unsubscribe(UnityAction<T> call)
    {
        unityEvent.RemoveListener(call);
    }
}

public class Event<T, U>
{
    private UnityEvent<T, U> unityEvent = new UnityEvent<T, U>();

    public void Publish(T t, U u)
    {
        unityEvent.Invoke(t, u);
    }

    public void Subscribe(UnityAction<T, U> call)
    {
        unityEvent.AddListener(call);
    }

    public void Unsubscribe(UnityAction<T, U> call)
    {
        unityEvent.RemoveListener(call);
    }
}

public class Event<T, U, V>
{
    private UnityEvent<T, U, V> unityEvent = new UnityEvent<T, U, V>();

    public void Publish(T t, U u, V v)
    {
        unityEvent.Invoke(t, u, v);
    }

    public void Subscribe(UnityAction<T, U, V> call)
    {
        unityEvent.AddListener(call);
    }

    public void Unsubscribe(UnityAction<T, U, V> call)
    {
        unityEvent.RemoveListener(call);
    }
}

public class Event<T, U, V, W>
{
    private UnityEvent<T, U, V, W> unityEvent = new UnityEvent<T, U, V, W>();

    public void Publish(T t, U u, V v, W w)
    {
        unityEvent.Invoke(t, u, v, w);
    }

    public void Subscribe(UnityAction<T, U, V, W> call)
    {
        unityEvent.AddListener(call);
    }

    public void Unsubscribe(UnityAction<T, U, V, W> call)
    {
        unityEvent.RemoveListener(call);
    }
}