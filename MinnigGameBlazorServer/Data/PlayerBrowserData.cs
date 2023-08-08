namespace MinnigGameBlazorServer.Data
{
    public record class PlayerBrowserData
    {
        public string Username;
        public PlayerBrowserData(string username)
        {
            this.Username = username;
        }
    }
}
