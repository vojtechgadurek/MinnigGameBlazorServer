using Microsoft.AspNetCore.SignalR;
using System.Security.Cryptography.X509Certificates;
using GameCorpLib;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace MinnigGameBlazorServer.Services
{
    public class EventAgregator<T>
    {
        /// <summary>
        /// There is currently no way how to unsubscribe, so class should be used only in services thought to live for whole application
        /// </summary>

        IDictionary<object, Action<T>> sucribers = new Dictionary<object, Action<T>>();
        public void Subscribe(object owner, Action<T> pusher)
        {
            lock (this)
            {
                sucribers.Add(owner, pusher);
            }
        }
        public void Unsubscribe(object owner)
        {
            lock (this)
            {
                sucribers.Remove(owner);
            }
        }
        public void Publish(T value)
        {
            lock (this)
            {
                foreach (var subscriber in sucribers)
                {
                    subscriber.Value.Invoke(value);
                }
            }
        }
    }
}
