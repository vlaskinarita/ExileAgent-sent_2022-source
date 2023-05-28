using System;
using System.Collections.Generic;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PusherClient
{
	public class EventEmitter
	{
		private IDictionary<string, IEventBinder> Emitters { get; } = new SortedList<string, IEventBinder>
		{
			{
				EventEmitter.getString_0(107311727),
				new PusherEventEmitter()
			},
			{
				EventEmitter.getString_0(107311784),
				new TextEventEmitter()
			},
			{
				EventEmitter.getString_0(107311813),
				new DynamicEventEmitter()
			}
		};

		public void Bind(string eventName, Action<dynamic> listener)
		{
			((DynamicEventEmitter)this.Emitters[EventEmitter.getString_0(107311813)]).Bind(eventName, listener);
		}

		public void Bind(string eventName, Action<string> listener)
		{
			((TextEventEmitter)this.Emitters[EventEmitter.getString_0(107311784)]).Bind(eventName, listener);
		}

		public void Bind(string eventName, Action<PusherEvent> listener)
		{
			((PusherEventEmitter)this.Emitters[EventEmitter.getString_0(107311727)]).Bind(eventName, listener);
		}

		public void BindAll(Action<string, dynamic> listener)
		{
			((DynamicEventEmitter)this.Emitters[EventEmitter.getString_0(107311813)]).Bind(listener);
		}

		public void BindAll(Action<string, string> listener)
		{
			((TextEventEmitter)this.Emitters[EventEmitter.getString_0(107311784)]).Bind(listener);
		}

		public void BindAll(Action<string, PusherEvent> listener)
		{
			((PusherEventEmitter)this.Emitters[EventEmitter.getString_0(107311727)]).Bind(listener);
		}

		public void Unbind(string eventName)
		{
			foreach (IEventBinder eventBinder in this.Emitters.Values)
			{
				eventBinder.Unbind(eventName);
			}
		}

		public void Unbind(string eventName, Action<dynamic> listener)
		{
			((DynamicEventEmitter)this.Emitters[EventEmitter.getString_0(107311813)]).Unbind(eventName, listener);
		}

		public void Unbind(string eventName, Action<string> listener)
		{
			((TextEventEmitter)this.Emitters[EventEmitter.getString_0(107311784)]).Unbind(eventName, listener);
		}

		public void Unbind(string eventName, Action<PusherEvent> listener)
		{
			((PusherEventEmitter)this.Emitters[EventEmitter.getString_0(107311727)]).Unbind(eventName, listener);
		}

		public void Unbind(Action<string, dynamic> listener)
		{
			((DynamicEventEmitter)this.Emitters[EventEmitter.getString_0(107311813)]).Unbind(listener);
		}

		public void Unbind(Action<string, string> listener)
		{
			((TextEventEmitter)this.Emitters[EventEmitter.getString_0(107311784)]).Unbind(listener);
		}

		public void Unbind(Action<string, PusherEvent> listener)
		{
			((PusherEventEmitter)this.Emitters[EventEmitter.getString_0(107311727)]).Unbind(listener);
		}

		public void UnbindAll()
		{
			foreach (IEventBinder eventBinder in this.Emitters.Values)
			{
				eventBinder.UnbindAll();
			}
		}

		internal IEventBinder GetEventBinder(string eventBinderKey)
		{
			return this.Emitters[eventBinderKey];
		}

		protected void SetEventEmitterErrorHandler(Action<PusherException> errorHandler)
		{
			foreach (IEventBinder eventBinder in this.Emitters.Values)
			{
				eventBinder.ErrorHandler = errorHandler;
			}
		}

		// Note: this type is marked as 'beforefieldinit'.
		static EventEmitter()
		{
			Strings.CreateGetStringDelegate(typeof(EventEmitter));
			EventEmitter.EmitterKeys = new string[]
			{
				EventEmitter.getString_0(107311727),
				EventEmitter.getString_0(107311784),
				EventEmitter.getString_0(107311813)
			};
		}

		internal static readonly string[] EmitterKeys;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
