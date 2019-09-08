# AppEngine
**AppEngine** a cross-platform library and supports a logging API that works with a variety of built-in and third-party logging providers. By using AppEngine to support your software's architecture, your code will become easier to write, reuse, test, and modify

**_Write your code so it's flexible...._**
```
static void Main(string[] args)
{
ILogEngine Logger = new BaseLogEngine(new FileLogger[]
{
    new FileLogger(FileName, FileFormat, FilePath),
});

Logger.Log(LogMessage, LogLevel.Information);
}
```
**_...and let AppEngine works._**

Log output is:
> Alert: [INFORMATION] :Message: [Say, Hello World!] - File Name: [Main] - Location: [D:\Git\AppEngine\AppEngine\AppEngine.ConsoleUI\Program.cs] - Line: [22]


### Features:

**1. Powerfull.** AppEngine includes many advanced features.<br />
**2. Fast.** AppEngine is fast, that is takes advantage of lambda expression build system. This can result in a dramatic (8-50x) improvement in performance in many situations.<br />
**3. Maintainable.** AppEngine is designed around a component-based architecture, with customization and evolution in mind. Many facets of the system can be augmented or modified to fit the requirements of each project.<br />
**4. Easy to Use.** AppEngine is easy to use with simple implementation but powerfull.<br />


### License:
Ninject is intended to be used in both open-source and commercial environments. To allow its use in as many situations as possible, Ninject is one-licensed. You may choose to use Ninject under either the MIT License.

Refer to [LICENSE.txt](https://github.com/itskhawarizmi/AppEngine/blob/master/LICENSE) for detailed information.

