using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using AutoMapper;
using ShelterBuddy.CodePuzzle.Api.Models;
using ShelterBuddy.CodePuzzle.Core.DataAccess;
using ShelterBuddy.CodePuzzle.Core.Entities;

namespace ShelterBuddy.CodePuzzle.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AnimalController : ControllerBase
{
    private readonly IRepository<Animal, Guid> repository;

    public AnimalController(IRepository<Animal, Guid> animalRepository)
    {
        repository = animalRepository;
    }

    [HttpGet]
    public AnimalModel[] Get() => repository.GetAll().Select(animal => new AnimalModel
    {
        Id = $"{animal.Id}",
        Name = animal.Name,
        Colour = animal.Colour,
        MicrochipNumber = animal.MicrochipNumber,
        Species = animal.Species,
        DateOfBirth = animal.DateOfBirth,
        DateInShelter = animal.DateInShelter,
        DateLost = animal.DateLost,
        DateFound = animal.DateFound,
        AgeYears = animal.AgeYears,
        AgeMonths = animal.AgeMonths,
        AgeWeeks = animal.AgeWeeks,
        AgeText = animal.AgeText
    }).ToArray();

    [HttpPost]
    public void Post(AnimalModel newAnimal)
    {
        try 
        {
            //Create a new Animal
            var animal = new Animal();

            repository.Add(animal);

            

        }
        catch (Exception ex) 
        {
            
        }
    }
}