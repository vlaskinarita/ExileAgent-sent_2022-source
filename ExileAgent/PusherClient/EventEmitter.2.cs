using System;
using System.Collections.Generic;

namespace PusherClient
{
	public class EventEmitter<TData> : IEventEmitter<TData>, IEventBinder<TData>, IEventBinder
	{
		public Action<PusherException> ErrorHandler { get; set; }

		public bool HasListeners
		{
			get
			{
				return this._listeners.Count > 0 || this._generalListeners.Count > 0;
			}
		}

		public void Bind(string eventName, Action<TData> listener)
		{
			Guard.EventName(eventName);
			if (listener != null)
			{
				object sync = this._sync;
				lock (sync)
				{
					if (!this._listeners.ContainsKey(eventName))
					{
						this._listeners[eventName] = new List<Action<TData>>();
					}
					if (!this._listeners[eventName].Contains(listener))
					{
						this._listeners[eventName].Add(listener);
					}
				}
			}
		}

		public void Bind(Action<string, TData> listener)
		{
			if (listener != null)
			{
				object sync = this._sync;
				lock (sync)
				{
					if (!this._generalListeners.Contains(listener))
					{
						this._generalListeners.Add(listener);
					}
				}
			}
		}

		public void Unbind(string eventName, Action<TData> listener)
		{
			Guard.EventName(eventName);
			if (listener != null)
			{
				object sync = this._sync;
				lock (sync)
				{
					if (this._listeners.ContainsKey(eventName))
					{
						this._listeners[eventName].Remove(listener);
					}
				}
			}
		}

		public void Unbind(string eventName)
		{
			Guard.EventName(eventName);
			object sync = this._sync;
			lock (sync)
			{
				if (this._listeners.ContainsKey(eventName))
				{
					this._listeners[eventName].Clear();
					this._listeners.Remove(eventName);
				}
			}
		}

		public void Unbind(Action<string, TData> listener)
		{
			if (listener != null)
			{
				object sync = this._sync;
				lock (sync)
				{
					this._generalListeners.Remove(listener);
				}
			}
		}

		public void UnbindAll()
		{
			object sync = this._sync;
			lock (sync)
			{
				this._generalListeners.Clear();
				foreach (List<Action<TData>> list in this._listeners.Values)
				{
					list.Clear();
				}
				this._listeners.Clear();
			}
		}

		public void EmitEvent(string eventName, TData data)
		{
			if (this.HasListeners)
			{
				List<Action<string, TData>> list = new List<Action<string, TData>>();
				List<Action<TData>> list2 = new List<Action<TData>>();
				object sync = this._sync;
				lock (sync)
				{
					if (this._generalListeners.Count > 0)
					{
						foreach (Action<string, TData> item in this._generalListeners)
						{
							list.Add(item);
						}
					}
					if (this._listeners.Count > 0 && this._listeners.ContainsKey(eventName))
					{
						list2.AddRange(this._listeners[eventName]);
					}
				}
				foreach (Action<string, TData> action in list)
				{
					try
					{
						action(eventName, data);
					}
					catch (Exception e)
					{
						this.HandleException(e, eventName, data);
					}
				}
				foreach (Action<TData> action2 in list2)
				{
					try
					{
						action2(data);
					}
					catch (Exception e2)
					{
						this.HandleException(e2, eventName, data);
					}
				}
			}
		}

		private void HandleException(Exception e, string eventName, TData data)
		{
			if (this.ErrorHandler != null)
			{
				PusherException ex = e as PusherException;
				if (ex == null)
				{
					ex = new EventEmitterActionException<TData>(ErrorCodes.EventEmitterActionError, eventName, data, e);
				}
				this.ErrorHandler(ex);
			}
		}

		private readonly object _sync = new object();

		private readonly IDictionary<string, List<Action<TData>>> _listeners = new SortedList<string, List<Action<TData>>>();

		private readonly IList<Action<string, TData>> _generalListeners = new List<Action<string, TData>>();
	}
}
