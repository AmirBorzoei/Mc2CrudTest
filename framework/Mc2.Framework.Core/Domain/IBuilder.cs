namespace Mc2.Framework.Core.Domain;

public interface IBuilder<out T>
{
    T Build();
}