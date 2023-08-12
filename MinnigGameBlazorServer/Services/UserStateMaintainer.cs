using GameCorpLib;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.SignalR;
using System.Data;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using MinnigGameBlazorServer.Data;
using GameCorpLib.State;

namespace MinnigGameBlazorServer.Services
{
	public class UserStateMaintainer
	{
		private Player? _player = null;
		public Player? Player { get { return _player; } }
		private IUserMaintainer _userMaintainer;
		bool _isInitialized = false;
		private readonly ProtectedLocalStorage _sessionStore;
		ILogger<UserStateMaintainer> _logger;
		EventAgregator<Player?> _eventAgregator;

		public UserStateMaintainer(ProtectedLocalStorage sessionStore, IUserMaintainer userMaintainer, ILogger<UserStateMaintainer> logger, EventAgregator<Player?> eventAgregator)
		{
			_sessionStore = sessionStore;
			_userMaintainer = userMaintainer;
			_logger = logger;
			_eventAgregator = eventAgregator;

			_logger.Log(LogLevel.Information, "User state maintainer created {code}", this.GetHashCode());
		}

		public async Task<Player?> Initialize()
		{
			Monitor.Enter(this);
			if (!_isInitialized)
			{
				_isInitialized = true;
				Player? MaybeStoredPlayer = await GetStoredPlayer();
				if (MaybeStoredPlayer != null)
				{
					UpdatePlayer(MaybeStoredPlayer);
				}
				_logger.Log(LogLevel.Information, "User state maintainer is initilized");
			}
			Monitor.Exit(this);
			return _player;

		}
		private async Task<Player?> GetStoredPlayer()
		{

			ProtectedBrowserStorageResult<string>? usernameStored = null;
			try
			{
				usernameStored = await _sessionStore.GetAsync<string>("storedUsername");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}

			if (!usernameStored.Value.Success)
			{
				return null;
			}


			return _userMaintainer.TryGetPlayerByName(usernameStored.Value.Value);
		}

		private void UpdatePlayer(Player? player)
		{
			//Has to be called from component otherwise js interop will not work


			//Lock is there to prevent different states in AuthStateProvider and UserMaintainer, if _player is locked for locked for longtime
			//it does not matter, because it is could not be used anyway    


			if (player == null)
			{
				_sessionStore.DeleteAsync("storedUsername");
				if (_player != null)
					_logger.Log(LogLevel.Information, "user {user} logged out", _player.Name);
				else
					_logger.Log(LogLevel.Information, "not logged user logged out");
			}
			else
			{
				_sessionStore.SetAsync("storedUsername", player.Name);
				_logger.Log(LogLevel.Information, "user {user} logged in", player.Name);
			}
			_player = player;
			_eventAgregator.Publish(player);
		}
		/// <summary>
		/// Tries to login player, if successfull, updates stateProvider and returns true, otherwise returns false
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <param name="stateProvider"></param>
		/// <param name="userMaintainer"></param>
		/// <returns></returns>
		public bool TryLoginPlayer(string username, string password)
		{
			Player? player = _userMaintainer.TryLoginPlayer(username, password);
			UpdatePlayer(player);
			return player != null;
		}
		/// <summary>
		/// Tries to register new player, if successfull, updates stateProvider and returns true, otherwise returns false
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <param name="stateProvider"></param>
		/// <param name="userMaintainer"></param>
		/// <returns></returns>
		public bool TryRegisterNewPlayer(string username, string password)
		{
			Player? player = _userMaintainer.TryRegisterNewPlayer(username, password);
			UpdatePlayer(player);
			return player != null;
		}
		public bool TryLogoutPlayer()
		{
			UpdatePlayer(null);
			return true;
		}

	}
}
