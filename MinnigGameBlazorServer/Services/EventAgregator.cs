using Microsoft.AspNetCore.SignalR;
using System.Security.Cryptography.X509Certificates;
using GameCorpLib;
namespace MinnigGameBlazorServer.Services
{
	public class EventAgregator<T>
	{
		/// <summary>
		/// There is currently no way how to unsubscribe, so class should be used only in services thought to live for whole application
		/// </summary>
		IList<Func<T, T>> sucribers = new List<Func<T, T>>();
		public void Subscribe(Func<T, T> pusher)
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
