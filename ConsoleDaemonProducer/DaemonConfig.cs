namespace ConsoleDaemonProducer
{
    public class DaemonConfig
    {
        public string DaemonName { get; set; }

        public int TickInterval => 60000;
    }
}
