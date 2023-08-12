using Microsoft.AspNetCore.SignalR;
using System.Security.Cryptography.X509Certificates;
using GameCorpLib;
using System.Runtime.CompilerServices;

namespace MinnigGameBlazorServer.Services
{
	public class EventAgregator<T>
	{
		/// <summary>
		/// There is currently no way how to unsubscribe, so class should be used only in services thought to live for whole application
		/// </summary>

		IList<Action<T>> sucribers = new List<Action<T>>();
		public void Subscribe(Action<T> pusher)
		{
			sucribers.Add(pusher);
		}
		public void Publish(T value)
		{
			foreach (var subscriber in sucribers)
			{
				subscriber.Invoke(value);
			}
		}
	}
}
