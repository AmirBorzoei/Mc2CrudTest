namespace Mc2.Framework.Core.Domain;

public abstract class ValueObject<T> : IEquatable<T>
{
    public abstract bool Equals(T other);

    public abstract override int GetHashCode();

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((T)obj);
    }
}