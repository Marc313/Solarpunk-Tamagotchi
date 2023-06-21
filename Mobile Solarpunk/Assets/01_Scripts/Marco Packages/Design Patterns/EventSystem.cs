using System.Collections.Generic;

namespace MarcoHelpers
{
    public enum EventName
    {
        NEEDMANAGER_UPDATE = 0,
        SET_DECAY_ACTIVE = 1
    }

    public delegate void EventCallback(object _value);
    public delegate void NeedChange(Needs _need, float _newValue);

    public static class EventSystem
    {
        // Normal Eventsystem
        private static Dictionary<EventName, List<EventCallback>> eventRegister = new Dictionary<EventName, List<EventCallback>>();

        public static void Subscribe(EventName _evt, EventCallback _func)
        {
            if (!eventRegister.ContainsKey(_evt))
            {
                eventRegister[_evt] = new List<EventCallback>();
            }

            eventRegister[_evt].Add(_func);
        }

        public static void Unsubscribe(EventName _evt, EventCallback _func)
        {
            if (eventRegister.ContainsKey(_evt))
            {
                eventRegister[_evt].Remove(_func);
            }
        }

        public static void RaiseEvent(EventName _evt, object _value = null)
        {
            if (eventRegister.ContainsKey(_evt))
            {
                foreach (EventCallback e in eventRegister[_evt])
                {
                    e.Invoke(_value);
                }
            }
        }

        // Needs eventsystem
        private static Dictionary<EventName, List<NeedChange>> needsEventRegister = new Dictionary<EventName, List<NeedChange>>();

        public static void Subscribe(EventName _evt, NeedChange _func)
        {
            if (!needsEventRegister.ContainsKey(_evt))
            {
                needsEventRegister[_evt] = new List<NeedChange>();
            }

            needsEventRegister[_evt].Add(_func);
        }

        public static void Unsubscribe(EventName _evt, NeedChange _func)
        {
            if (needsEventRegister.ContainsKey(_evt))
            {
                needsEventRegister[_evt].Remove(_func);
            }
        }

        public static void RaiseEvent(EventName _evt, Needs _need, float _value)
        {
            if (needsEventRegister.ContainsKey(_evt))
            {
                foreach (NeedChange e in needsEventRegister[_evt])
                {
                    e.Invoke(_need, _value);
                }
            }
        }
    }

    //public delegate void EventCallback(object _value);

    /*    public static class EventSystem
        {
            private static Dictionary<EventName, List<Action>> eventRegister = new Dictionary<EventName, List<Action>>();

            public static void Subscribe(EventName _evt, Action _func)
            {
                if (!eventRegister.ContainsKey(_evt))
                {
                    eventRegister[_evt] = new List<Action>();
                }

                eventRegister[_evt].Add(_func);
            }

            public static void Unsubscribe(EventName _evt, Action _func)
            {
                if (eventRegister.ContainsKey(_evt))
                {
                    eventRegister[_evt].Remove(_func);
                }
            }

            public static void RaiseEvent(EventName _evt, object _value = null)
            {
                if (eventRegister.ContainsKey(_evt))
                {
                    foreach (Action e in eventRegister[_evt])
                    {
                        e.Invoke();
                    }
                }
            }
        }*/
} 

