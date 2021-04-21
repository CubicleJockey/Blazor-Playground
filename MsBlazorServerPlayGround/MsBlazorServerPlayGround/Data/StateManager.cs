using System;
using System.Collections.Generic;

namespace MsBlazorServerPlayGround.Data
{
    public class StateManager
    {
        private readonly IDictionary<string, object> states;

        public event Action OnChange;

        public StateManager()
        {
            states = new Dictionary<string, object>();
        }

        public void SetState(string key, object value)
        {
            if (states.ContainsKey(key))
            {
                states[key] = value;
                NotifyStateChanged();
            }
            else
            {
                states.Add(key, value);
                NotifyStateChanged();
            }
        }

        public T GetState<T>(string key)
        {
            if (!states.ContainsKey(key)) { return default; }
            return (T)states[key];
        }

        public (bool removed, T itemRemoved) DeleteState<T>(string key)
        {
            if (!states.ContainsKey(key))
            {
                return (false, default);
            }

            var removed = states.Remove(key, out var item);
            if(removed) { NotifyStateChanged(); }

            return (removed, (T)item ?? default);
        }

        #region Helper Methods

        private void NotifyStateChanged() => OnChange?.Invoke();

        #endregion Helper Methods
    }
}
