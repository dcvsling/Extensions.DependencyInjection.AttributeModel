// See https://aka.ms/new-console-template for more information
using App;

using Extensions.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;

var srv = new ServiceCollection();
srv.AddAttributeModelRegister();
var provider = srv.BuildServiceProvider();

provider.GetRequiredService<Test>().Invoke();

