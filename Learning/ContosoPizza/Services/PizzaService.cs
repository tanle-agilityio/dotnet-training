using ContosoPizza.Models;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ContosoPizza.Services;

public static class PizzaService
{
    static List<Pizza> Pizzas { get; }
    static int nextId = 3;

    static PizzaService()
    {
        Pizzas = new List<Pizza>
        {
            new Pizza { Id = 1, Name = "Classic Italian", IsGlutenFree = false },
            new Pizza { Id = 2, Name = "Veggie", IsGlutenFree = true }
        };
    }

    public static List<Pizza> GetAll() => Pizzas;

    public static Pizza? Get(int id) => Pizzas.FirstOrDefault(x => x.Id == id);

    public static void Add(Pizza pizza)
    {
        pizza.Id = nextId++;
        Pizzas.Add(pizza);
    }

    public static void Delete(int id)
    {
        var pizza = Get(id);
        
        if (pizza != null)
        {
            Pizzas.Remove(pizza);
        } else
        {
            return;
        }
    }

    public static void Update(Pizza pizza)
    {
        int index = Pizzas.FindIndex(p => p.Id == pizza.Id);
        
        if (index == -1) 
        {
            return;
        }

        Pizzas[index] = pizza;
    }
	public static void MapPizzaEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Pizza").WithTags(nameof(Pizza));

        group.MapGet("/", () =>
        {
            return new [] { new Pizza() };
        })
        .WithName("GetAllPizzas")
        .WithOpenApi();

        group.MapGet("/{id}", (int id) =>
        {
            //return new Pizza { ID = id };
        })
        .WithName("GetPizzaById")
        .WithOpenApi();

        group.MapPut("/{id}", (int id, Pizza input) =>
        {
            return TypedResults.NoContent();
        })
        .WithName("UpdatePizza")
        .WithOpenApi();

        group.MapPost("/", (Pizza model) =>
        {
            //return TypedResults.Created($"/api/Pizzas/{model.ID}", model);
        })
        .WithName("CreatePizza")
        .WithOpenApi();

        group.MapDelete("/{id}", (int id) =>
        {
            //return TypedResults.Ok(new Pizza { ID = id });
        })
        .WithName("DeletePizza")
        .WithOpenApi();
    }
}
