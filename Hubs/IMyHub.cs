namespace advanced_programming_2_server_side_exercise.Hubs
{
    public interface IMyHub
    {
        public Task NewMessage();
        public Task NewContact();
    }
}
