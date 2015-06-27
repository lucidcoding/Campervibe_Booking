namespace CampervibeBooking.Domain.InfrastructureContracts
{
    public interface IEmailer
    {
        void Send(
            string to,
            string from,
            string subject,
            string body);
    }
}
