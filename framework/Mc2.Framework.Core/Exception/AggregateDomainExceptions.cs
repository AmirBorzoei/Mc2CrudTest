namespace Mc2.Framework.Core.Exception;

public class AggregateDomainExceptions : System.Exception
{
    public AggregateDomainExceptions()
    {
        DomainExceptions = new List<DomainException>();
    }

    public List<DomainException> DomainExceptions { get; set; }
}