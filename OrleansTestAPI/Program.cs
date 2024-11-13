using Orleans.Hosting;
using OrleansTestAPI;
using SqlSugar;
using Weixsu.Orleans.Transactions.AdoNet;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
builder.Host.UseOrleans((context, silo) =>
{
       var config = context.Configuration.GetSection("GrainStorageConfig").Get<GrainStorageConfig>();
       silo.UseLocalhostClustering().AddAdoNetGrainStorage("OrleansStorage", options =>
          {
              options.Invariant = config.Invariant;
              options.ConnectionString = config.ConnectionString;
              options.UseJsonFormat = config.UseJsonFormat;
          })
         .AddAdoNetTransactionalStateStorage("TransactionStore", cfg =>
         {
            cfg.DbConnector = DbConnectors.MySql;
           cfg.ConnectionString = config.ConnectionString;
           })
          .UseTransactions();

});
var sqlConfig = builder.Configuration.GetSection("SqlsugarConfigList").Get<List<ConnectionConfig>>();
builder.Services.AddScoped<ISqlSugarClient>(_ => new SqlSugarClient(sqlConfig,
    db =>
    {
        db.Aop.OnLogExecuting = (sql, param) => { };
    }
));
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
