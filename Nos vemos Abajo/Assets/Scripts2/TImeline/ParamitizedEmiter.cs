using UnityEngine.Timeline;

public class ParameterizedEmitter<T> : SignalEmitter
{
    public T parameter;
    public T secondParameter;
}