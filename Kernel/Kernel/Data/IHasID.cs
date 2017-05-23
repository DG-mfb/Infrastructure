
namespace Kernel.Data
{
	public interface IHasID<TID>
	{
		TID ID { get; }
	}
}