namespace Kernel.Federation.RelyingParty
{
    public interface IRelyingPartyContextBuilder
    {
        RelyingPartyContext BuildRelyingPartyContext(string relyingPartyId);
    }
}
