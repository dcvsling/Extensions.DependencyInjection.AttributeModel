# Extensions.DependencyInjection.AttributeModel

## ²��

�o�O�H ```Microsoft.Extensions.DependencyInjection``` ���D�n DI �e��
�åB�z�L Attribute �ӳ]�w�`�J�覡

## �ϥΤ覡

### �w��

### �ϥ�

�@�����O�w�Ȼݭn�z�L�H ```Lifetime``` ���W���ݩʹ����O�˹��Y�i
```csharp
[Singleton]
public class SingletonService {}

[Scoped(ServiceType = typeof(IScoped))]
public class ScopedService : IScoped {}

[Transient(ServiceType = typeof(ITransient<>))]
public class TransientService<T> : ITransient<T> { }
```

�åB��̥��ݪ��M�� �Ҧp: Web�M�� 
��ӱM�פ����U DI �e�����a��[�W�U���o�q
���B�H AspNetCore Web �M�פ��� Startup.ConfigureService ����

```csharp
public void ConfigureService(IServiceCollection services)
{
	// �[�W�o��Y�i
	services.AddAttributeModelRegister();
}
```

## �B�@�覡

### �y�{

²�满���@�U�䤺���B�@�覡 (it's not magic)

1. �B�� SourceGenerator �������R�����R Attribute �ñN���ഫ�����۹������e�����U���{���X
1. �N���ͪ��{���X�g�b�@�ӥѷ�U AssemblyId �� Namespace �U�W�� ServiceRegistryAttribute �����O��
1. �N�����O�~�� Attribute �åB�� Assembly �ŧO�W�����ŧi�� Attribute
1. �̫� ```AddAttributeModelRegister``` �N�O�b�Ҧ����J�� Assembly
����X�Ҧ��� ServiceRegistryAttribute �åB�N IServiceCollection �a�J�ç������U

### �u�I

- �@�� Attribute ���������k�������z�L����涥�q�h���y�Ҧ�������,  
�ӳz�L SourceGenerator �h�O�b�]�p���q�N�����һݭn�����U�{���X,  
����涥�q�ȷj�M�� Assembly Level �� Attribute,  
�Ұʮɪ��į�i�H�n�ܦh
- �ѩ� dotnet ���ت� DI �e���X�G�O�S���}���X�i��,  
�]���p�G�Q�n��{����S���Ҧ� (ex: �˹����Ҧ�),
�|�ܱo���`���x���άO����,
�z�L �]�p���q�� Attribute �ӹ�{���U���P���B�~���ѤF�@�ӭ��s�]�p�����U�覡���Ŷ�,  
�]���i�H�j�T�ת��[�j��i�X�i��  
~~(�����٨S��{�o��Ʊ� orz)~~

### ���I

- �]�� Attribute �L�k�ϥ� �D const ���Ѽ�  
�]���b�����z�L��k����{���U���רҤ�  
���Ǥ�k�ө�b�����ܱo���I����  
���L�ѩ� dotnet �� DI ���U�覡�w�g�ɥi��a�קK�o��Ʊ��o��
�H�Υثe�]�w�g��{�@�ذ��k�Ӵ��ѳo�������ݨD

### Hint

�Ҧ����U�e���ҨϥΪ��U���۩w�q����k  
�������}�̪촣�Ѫ����X�ӵ��U�覡
�Ҧp:
```csharp
services.Configure<MyOptions>(options => configuration.Bind(options));
```
��ڤW�L�I��{���X�аѦҳs��
[source](https://github.com/dotnet/runtime/blob/main/src/libraries/Microsoft.Extensions.Options.ConfigurationExtensions/src/OptionsConfigurationServiceCollectionExtensions.cs#L67)
�z�L��l�X�o�{�L�O�Τ@�� class �˸����q lambda �åB���U�� IConfigureOptions �H�� Singleton 
�]���z�L�U�����k�]�i�H�F��@�˪��ĪG
```csharp
[Singleton(ServiceType = typeof(IConfigureOptions<MyOptions>))]
public class MyOptionsConfigureOptions : IConfigureOptions<MyOptions> 
{
	private readonly IConfiguration _configuration;
	public MyOptionsConfigureOptions(IConfiguration configuration) 
	{
		_configuration = configuration;
	}
	public void Configure(MyOptions options)
	{
		_configuration.Bind(options);
	}
}
```