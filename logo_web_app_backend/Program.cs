using logo_web_app_backend;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
         

        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Authentication Parameters

            ValidateIssuer = true,  // validates the server, generates token
            ValidateAudience = true, // validate the recipient of the token is authorized to receive
            ValidateLifetime = true, // check if the token is exprired
            ValidIssuer = TokenService.ISSUER,
            ValidAudience = TokenService.AUDIENCE,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenService.KEY)) // validates the signute of the tokens
        };

    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//enable the cors

builder.Services.AddCors(p => p.AddPolicy("corspolicy", build => {
    build.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corspolicy");

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
