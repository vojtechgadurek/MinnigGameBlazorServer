using GameCorpLib;

namespace MinnigGameBlazorServer.Services
{
	public class UserState<T>
	{
		IDictionary<Player, EventAgregator<T>> dic = new Dictionary<Player, EventAgregator<T>>();

		public void Subscribe(Player player, object owner, Action<T> pusher)
		{
            if (player is null) return;
            lock (this)
			{
				if (!dic.ContainsKey(player))
				{
					dic.Add(player, new EventAgregator<T>());
				}
				dic[player].Subscribe(owner, pusher);
			}
		}
		public void Unsubscribe(Player? player, object owner)
		{
			if (player is null) return;
			lock (this)
			{
				if (dic.ContainsKey(player))
				{
					dic[player].Unsubscribe(owner);
				}
			}
		}
		public void Publish(Player? player, T value)
		{
            if (player is null) return;
            lock (this)
			{
				if (dic.ContainsKey(player))
				{
					dic[player].Publish(value);
				}
			}
		}
	}
}
