using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using CJ.Domain.EntityFrameworkCore;

namespace CJ.Data.FirstModels
{
    /// <summary>
    /// 重写IConnectionStringResolver 根据字典Type来判断用那一个数据链接字符串
    /// </summary>
    public class MyConnectionStringResolver : DefaultConnectionStringResolver
    {
        private IConfiguration _configuration;
        //public MyConnectionStringResolver(IConfiguration configuration)
        //   : base(configuration)
        //{
        //    _configuration = configuration;
        //}
        //public override string GetNameOrConnectionString(Dictionary<string, object> args)
        //{
        //    var connectString = this.GetConnectionString(args);
        //    if (!string.IsNullOrEmpty(connectString))
        //    {
        //        return connectString;
        //    }
        //    return base.GetNameOrConnectionString(args);
        //}
        //private string GetConnectionString(Dictionary<string, object> args)
        //{
        //    var type = args["DbContextConcreteType"] as Type;
        //    if (type == typeof(FirstTestDBContext))
        //    {
        //        return _configuration.GetSection("ConnectionStrings:Default").ToString() ;
        //    }
        //    return null;
        //}
    }
}
