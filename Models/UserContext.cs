using Microsoft.EntityFrameworkCore;

namespace donetmvc_db_demo.Models;

public class UserContext : DbContext
{
    private const int MinPoolSize = 5;
    private const int MaxPoolSize = 20;
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var rdsAddress = Environment.GetEnvironmentVariable("RDS_ADDRESS");
        var rdsUserName = Environment.GetEnvironmentVariable("RDS_USER_NAME");
        var rdsPassword = Environment.GetEnvironmentVariable("RDS_PASSWORD");
        var rdsDbName = Environment.GetEnvironmentVariable("RDS_DB_NAME");
        var rdsPort = Environment.GetEnvironmentVariable("RDS_PORT");
        rdsAddress = string.IsNullOrEmpty(rdsAddress) ? "localhost" : rdsAddress;
        rdsUserName = string.IsNullOrEmpty(rdsUserName) ? "root" : rdsUserName;
        rdsPassword = string.IsNullOrEmpty(rdsPassword) ? "Huawei@123" : rdsPassword;
        rdsPort = string.IsNullOrEmpty(rdsPort) ? "3306" : rdsPort;
        rdsDbName = string.IsNullOrEmpty(rdsDbName) ? "dotnet_web_db" : rdsDbName;
        if (string.IsNullOrEmpty(rdsAddress) || string.IsNullOrEmpty(rdsUserName) ||
            string.IsNullOrEmpty(rdsPassword) || string.IsNullOrEmpty(rdsDbName) || string.IsNullOrEmpty(rdsPort))
        {
            Console.WriteLine("env not set properly");
            throw new Exception("env not set properly");
        }

        var connectionString =
            "Server=" + rdsAddress + ";Port=" + rdsPort + ";Database=" + rdsDbName + ";Uid=" + rdsUserName +
            ";Pwd=" + rdsPassword + ";" + "MinPoolSize=" + MinPoolSize + ";" + "MaxPoolSize=" + MaxPoolSize;
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
}