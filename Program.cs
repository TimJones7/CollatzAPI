using CollatzAPI;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICollatzService, CollatzService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/about", () =>
{
    return "This is a minimal API I decided to make for Collatz Collapser";
})
.WithName("AboutInfo");


app.MapGet("/GetCommonAncestor/{x}/{y}", (int x, int y) =>
{
    int commonAncestor = 0;
    using (var scope = app.Services.CreateScope())
    {
        var _collatz = scope.ServiceProvider.GetRequiredService<ICollatzService>();

        commonAncestor = _collatz.Find_Least_Common_Ancestor(x, y).value;
    }
    return commonAncestor;
})
.WithName("GetCommonAncestor");


app.MapGet("/GetPathFromNumber/{x}", (int x) =>
{
    List<int> path;
    using (var scope = app.Services.CreateScope())
    {
        var _collatz = scope.ServiceProvider.GetRequiredService<ICollatzService>();

        path = _collatz.Get_Collatz_Chain_From_Number(x);
    }
    return path;
})
.WithName("GetCollatzPath");

app.MapGet("/GetLeadingDigitDistributionOfPathFrom/{x}", (int x) =>
{
    Dictionary<int, int> dist = new Dictionary<int, int>();
    using (var scope = app.Services.CreateScope())
    {
        var _collatz = scope.ServiceProvider.GetRequiredService<ICollatzService>();

        dist = _collatz.Get_Leading_Digit_Distribution_as_Dictionary(x);
    }
    return dist;
})
.WithName("GetLeadingDigitDistribution");

app.MapGet("/ListMethods", () =>
{
    return "METHODS AVAILABLE IN COLLATZ COLLAPSER MINIMAL API:" +
    " ....../GetPathFromNumber/{x} ..... (input x => int)(returns => List of int)" +
    " ....../GetCommonAncestor/{x}/{y} ..... (input x, y => int)(returns => int)" +
    " ....../GetLeadingDigitDistributionOfPathFrom/{x} ..... (input x => int)(returns => dict )";
})
.WithName("ListAPIMethods");

app.MapGet("/ListMethods2", () =>
{
    var html = Results.Extensions.HtmlResponse(@"
    <h2>METHODS AVAILABLE IN COLLATZ COLLAPSER MINIMAL API:</h2>
    <hr/>
    <dl>
    <dt>/GetPathFromNumber/{x} </dt>
        <dd>- (input x => int)</dd>
        <dd>- (returns => List of int)</dd>
    <hr/>
    <dt>/GetCommonAncestor/{x}/{y} </dt>
        <dd>- (input x,y => int)</dd>
        <dd>- (returns => int)</dd>
    <hr/>
    <dt>/GetLeadingDigitDistributionOfPathFrom/{x}</dt>
        <dd>- (input x => int)</dd>
        <dd>- (returns => dictionary)</dd>
    </dl>");
    return html;
})
.WithName("ListAPIMethods2");

app.Run();