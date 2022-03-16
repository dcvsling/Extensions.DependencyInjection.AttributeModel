# Extensions.DependencyInjection.AttributeModel

## 簡介

這是以 ```Microsoft.Extensions.DependencyInjection``` 為主要 DI 容器
並且透過 Attribute 來設定注入方式

## 使用方式

### 安裝

### 使用

一般類別庫僅需要透過以 ```Lifetime``` 為名的屬性對類別裝飾即可
```csharp
[Singleton]
public class SingletonService {}

[Scoped(ServiceType = typeof(IScoped))]
public class ScopedService : IScoped {}

[Transient(ServiceType = typeof(ITransient<>))]
public class TransientService<T> : ITransient<T> { }
```

並且於最末端的專案 例如: Web專案 
於該專案中註冊 DI 容器的地方加上下面這段
此處以 AspNetCore Web 專案中的 Startup.ConfigureService 為例

```csharp
public void ConfigureService(IServiceCollection services)
{
	// 加上這行即可
	services.AddAttributeModelRegister();
}
```

## 運作方式

### 流程

簡單說明一下其內部運作方式 (it's not magic)

1. 運用 SourceGenerator 中的分析器分析 Attribute 並將其轉換成為相對應的容器註冊的程式碼
1. 將產生的程式碼寫在一個由當下 AssemblyId 的 Namespace 下名為 ServiceRegistryAttribute 的類別中
1. 將該類別繼承 Attribute 並且於 Assembly 級別上直接宣告該 Attribute
1. 最後 ```AddAttributeModelRegister``` 就是在所有載入的 Assembly
中找出所有的 ServiceRegistryAttribute 並且將 IServiceCollection 帶入並完成註冊

### 優點

- 一般 Attribute 類型的做法都必須透過於執行階段去掃描所有的類型,  
而透過 SourceGenerator 則是在設計階段就完成所需要的註冊程式碼,  
於執行階段僅搜尋到 Assembly Level 的 Attribute,  
啟動時的效能可以好很多
- 由於 dotnet 內建的 DI 容器幾乎是沒有開放擴展的,  
因此如果想要實現比較特殊的模式 (ex: 裝飾器模式),
會變得異常的困難或是複雜,
透過 設計階段的 Attribute 來實現註冊等同於額外提供了一個重新設計的註冊方式的空間,  
因此可以大幅度的加強其可擴展性  
~~(但我還沒實現這件事情 orz)~~

### 缺點

- 因為 Attribute 無法使用 非 const 的參數  
因此在必須透過方法的實現註冊的案例中  
那些方法該放在哪裡變得有點尷尬  
不過由於 dotnet 的 DI 註冊方式已經盡可能地避免這件事情發生
以及目前也已經實現一種做法來提供這部分的需求

### Hint

所有註冊容器所使用的各式自定義的方法  
都離不開最初提供的那幾個註冊方式
例如:
```csharp
services.Configure<MyOptions>(options => configuration.Bind(options));
```
實際上他背後程式碼請參考連結
[source](https://github.com/dotnet/runtime/blob/main/src/libraries/Microsoft.Extensions.Options.ConfigurationExtensions/src/OptionsConfigurationServiceCollectionExtensions.cs#L67)
透過原始碼發現他是用一個 class 裝載那段 lambda 並且註冊為 IConfigureOptions 以及 Singleton 
因此透過下面做法也可以達到一樣的效果
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