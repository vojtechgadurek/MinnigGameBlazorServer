using System.ComponentModel;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using GameCorpLib;
using GameCorpLib.Persons;
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
				bool ok = true;
				rich = gameControler.TryRegisterNewPlayer("rich", "");
				ok &= rich.Stock.TrySetResourceCapacity(Resource.CreateMoney(double.PositiveInfinity));
				ok &= rich.Stock.TrySetResource(new Resource(ResourceType.Money, double.MaxValue));
				poor = gameControler.TryRegisterNewPlayer("poor", "");
				ok &= poor.Stock.TrySetResource(new Resource(ResourceType.Money, 0));
				ok &= rich != null;
				ok &= poor != null;
				if (!ok) throw new System.Exception("BasicGame initialization failed");
			}
		}
	}

	public class TestSilo
	{
		[Theory]
		[InlineData(10000, 1000, true)]
		[InlineData(10000, 100000, false)]
		[InlineData(10000, 10000, true)]
		public void TestSiloAdd(double capacity, double resourceAmountToAdd, bool ansver)
		{
			Silo silo = new Silo(capacity);
			bool result = silo.TryIncreaseAmount(Resource.CreateMoney(resourceAmountToAdd));
			Assert.Equal(ansver, result);
			if (result) Assert.Equal(resourceAmountToAdd, silo.Amount);
		}

		[Theory]
		[InlineData(10000, 1000, true)]
		[InlineData(10000, 100000, false)]
		[InlineData(10000, 10000, true)]

		public void TestSiloBlock(double capacity, double capacityToBlock, bool ansver)
		{
			Silo silo = new Silo(capacity);
			bool result = silo.TryBlockCapacity(Resource.CreateMoney(capacityToBlock));
			Assert.Equal(ansver, result);
			if (result) Assert.Equal(capacityToBlock, silo.BlockedCapacity);
		}

		[Theory]
		[InlineData(10000, 1000, 10)]

		public void TestUseBlockedCapacity(double capacity, double capacityToBlock, double capacityToFill)
		{
			Silo silo = new Silo(capacity);
			silo.TryBlockCapacity(Resource.CreateMoney(capacityToBlock));
			silo.UseBlockedResourceCapacity(Resource.CreateMoney(capacityToFill));
			Assert.Equal(capacityToBlock - capacityToFill, silo.BlockedCapacity);
			Assert.Equal(capacityToFill, silo.Amount);
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
			Assert.Equal(true, gameControler.TryProspectNewOilField(basicGame.rich));

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