namespace CampervibeBooking.Domain.InfrastructureContracts
{
    public class StubEmailer : IEmailer
    {
        public void Send(string to, string from, string subject, string body)
        {
        }
    }
}
