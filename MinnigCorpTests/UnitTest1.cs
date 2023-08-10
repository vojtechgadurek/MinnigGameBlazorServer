using System.ComponentModel;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using GameCorpLib;
using GameCorpLib.State;
using GameCorpLib.Tradables;
namespace MinnigCorpTests
{
	static public class Utils
	{
		public class BasicGame
		{
			public GameControler gameControler;
			public Player? rich;
			public Player? poor;
			public BasicGame()
			{
				gameControler = new GameControler();
				rich = gameControler.TryRegisterNewPlayer("rich", "");
				rich.Money = double.MaxValue;
				poor = gameControler.TryRegisterNewPlayer("poor", "");
				poor.Money = 0;
			}
		}
	}
	public class TestUserManagment
	{
		[Fact]
		public void TestCreatePlayer()
		{
			GameControler gameControler = new GameControler();
			Player? player = gameControler.TryRegisterNewPlayer("test", "test");
			Player? loggedPlayer = gameControler.TryLoginPlayer("test", "tets");
			Assert.Equal(null, loggedPlayer);
			loggedPlayer = gameControler.TryLoginPlayer("test", "test");
			Assert.Equal(player, loggedPlayer);
		}
	}
	public class TestPropertyManagment
	{
		[Fact]
		public void TestProspectNewOilField()
		{
			//Rich property buy should be succesful
			//Poor property buy should be unsuccesful
			Utils.BasicGame basicGame = new Utils.BasicGame();
			GameControler gameControler = basicGame.gameControler;
			Assert.Equal(true, gameControler.TryProspectNewOilField(basicGame.rich));
			Assert.Equal(false, gameControler.TryProspectNewOilField(basicGame.poor));
			Assert.Equal(1, basicGame.rich.Properties.Count);
			Assert.Equal(0, basicGame.poor.Properties.Count);
			Assert.Equal(true, gameControler.TryProspectNewOilField(basicGame.rich));
			Assert.Equal(2, basicGame.rich.Properties.Count);

		}
	}

	public class TestRegisters
	{
		[Fact]
		public void TestIssueId()
		{
			Register<int> register = new Register<int>();
			int first;
			register.RegisterItem(1, out first);
			int second;
			register.RegisterItem(2, out second);
			Assert.Equal(0, first);
			Assert.Equal(1, second);
		}
	}
}