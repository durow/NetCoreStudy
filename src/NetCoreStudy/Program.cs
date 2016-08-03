using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace NetCoreStudy
{
    public class Program
    {
        //程序的入口，也就是从这里开始执行
        public static void Main(string[] args)
        {
            /*
             * 每一个Web服务就是一个WebHost实例，
             * WebHost实例通过WebHostBuilder实例的Build()方法创建
             * 在WebHostBuilder最终Build之前可以进行配置。
             * 一般都是用名字为UseXXX的方法。
             */
            var host = new WebHostBuilder()
                .UseKestrel() //使用Kestrel监听HTTP请求
                .UseContentRoot(Directory.GetCurrentDirectory()) //配置根路径为当前程序路径
                .UseIISIntegration() //使用IISIntergration
                .UseStartup<Startup>() //将Startup类作为启动的类。启动时会构造一个Startup的实例并通过反射调用其ConfigreServices方法和Configure方法
                .Build(); //构建WebHost

            host.Run(); //运行构建好的WebHost。
        }
    }
}
