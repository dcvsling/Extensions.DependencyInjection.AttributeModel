// See https://aka.ms/new-console-template for more information

using Extensions.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;

namespace App;

[Inject(Lifetime = ServiceLifetime.Singleton, MemberName = "Instance")]
public class Test
{
    public static Test Instance { get; } = new Test();
    public void Invoke() => Console.WriteLine("Ok");
}

