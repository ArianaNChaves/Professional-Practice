using System;
using System.Collections.Generic;
using UnityEngine;

public static class MessageSystem
{
    private static readonly Dictionary<Type, Delegate> Handlers = new Dictionary<Type, Delegate>();

    public static void Subscribe<TEvent>(Action<TEvent> handler)
    {
        if (handler == null)
        {
            throw new ArgumentNullException(nameof(handler));
        }

        Type eventType = typeof(TEvent);
        if (Handlers.TryGetValue(eventType, out Delegate existingHandlers))
        {
            Handlers[eventType] = Delegate.Combine(existingHandlers, handler);
            return;
        }

        Handlers[eventType] = handler;
    }

    public static void Unsubscribe<TEvent>(Action<TEvent> handler)
    {
        if (handler == null)
        {
            return;
        }

        Type eventType = typeof(TEvent);
        if (!Handlers.TryGetValue(eventType, out Delegate existingHandlers))
        {
            return;
        }

        Delegate updatedHandlers = Delegate.Remove(existingHandlers, handler);
        if (updatedHandlers == null)
        {
            Handlers.Remove(eventType);
            return;
        }

        Handlers[eventType] = updatedHandlers;
    }

    public static void Publish<TEvent>(TEvent gameEvent)
    {
        Type eventType = typeof(TEvent);
        if (!Handlers.TryGetValue(eventType, out Delegate handlers))
        {
            return;
        }

        foreach (Delegate handler in handlers.GetInvocationList())
        {
            ((Action<TEvent>)handler).Invoke(gameEvent);
        }
    }

    public static void Clear()
    {
        Handlers.Clear();
    }
}
